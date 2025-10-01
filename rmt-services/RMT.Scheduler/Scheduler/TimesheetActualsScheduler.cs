using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Enumeration;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Npgsql;
using RMT.Scheduler.Constants;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.DTOs.Allocation;
using RMT.Scheduler.service;
using RMT.Scheduler.service.AzureServices;
using RMT.Scheduler.service.Employee;
using RMT.Scheduler.service.Oracle;
using RMT.Scheduler.service.Project;
using RMT.Scheduler.service.WCGT;
using static RMT.Scheduler.Constants.Constant;

namespace RMT.Scheduler.Scheduler
{
    /// <summary>
    /// To Get the timeshtee actula data fromo WCGt exposet View from Oracle instance using direct connection string 
    /// And update the same data to RMS system
    /// </summary>
    public class TimesheetActualsScheduler
    {
        private bool doDetailedLogging = true;
        private readonly IOracleService _oracleService;
        private readonly IAllocationHttpService _allocationHttpService;
        private readonly IProjectHttpService _projectHttpService;
        private readonly IEmployeeHttpService _employeeHttpService;


        public TimesheetActualsScheduler(IOracleService oracleService, IAllocationHttpService allocationHttpService, IProjectHttpService projectHttpService, IEmployeeHttpService employeeHttpService)
        {
            _oracleService = oracleService;
            _allocationHttpService = allocationHttpService;
            _projectHttpService = projectHttpService;
            _employeeHttpService = employeeHttpService;
        }

        [FunctionName("TimesheetActualsScheduler")]
        public async Task RunAsync([TimerTrigger("%TimesheetActualsSchedulerTriggerTime%")] TimerInfo myTimer, ILogger _logger)
        {
            _logger.LogInformation($"TimesheetActualsSchedulerTriggerTime Timer trigger function execution started at: {DateTime.Now}");
            string schedularType = SchedularLogsType.Timesheet;
            var environmentVaribles = Environment.GetEnvironmentVariables();
            string ConfigDbConnectionString = Convert.ToString(environmentVaribles[EnvAppSettingConstants.ConfigDbConnectionString]);
            string numberOfDaysToConsiderToFetchInitially = Convert.ToString(environmentVaribles[EnvAppSettingConstants.numberOfDaysToConsiderToFetchInitially]);
            SchedularLogs logsInstance = new()
            {
                Connection = ConfigDbConnectionString
            };
            Guid loggedNewGuid = Guid.NewGuid();

            try
            {
                DataTable syncLogsTable = FetchSchedularLogsByStatus(logsInstance, SchedularLogsSyncStatus.SYNCED, _logger, schedularType);
                Object syncStartTime = DateTime.UtcNow.AddDays(string.IsNullOrEmpty(numberOfDaysToConsiderToFetchInitially) ? -7 : Convert.ToInt16(numberOfDaysToConsiderToFetchInitially));
                if (syncLogsTable.Rows.Count > 0)
                {
                    syncStartTime = syncLogsTable.Rows[0]["SchedularStartTime"];
                    //syncStartTime = DateTime.Parse("2025-06-19 10:00:00.529763"); ;
                }
                Int16 batchConfig = Convert.ToInt16(environmentVaribles[EnvAppSettingConstants.TimesheetActualsBatchSize]);

                var batchSize = batchConfig > 0 ? batchConfig : 100;

                _logger.LogInformation($"Adding schedular logs");
                AddSchedularLogs(logsInstance, loggedNewGuid, _logger, schedularType);
                _logger.LogInformation($"Adding schedular logs completed");

                _logger.LogInformation($"syncStartTime is {Convert.ToString(syncStartTime)}");

                List<OracleTimesheetResponseDto> timesheetDataFromOracle = await _oracleService.GetTimesheetDataFromOracle(Convert.ToString(syncStartTime), _logger);

                if (timesheetDataFromOracle != null && timesheetDataFromOracle.Count > 0)
                {
                    _logger.LogInformation($"Timesheet Data From Oracle fetched successfully.");
                    var validRecords = timesheetDataFromOracle
                        .Where(m =>
                            !String.IsNullOrEmpty(m.EmployeeMID)
                            && !String.IsNullOrEmpty(m.EmpEmail)
                            && !String.IsNullOrEmpty(m.JobCode)
                            && m.DateLog != null
                            && m.TotalTime != null
                        )
                        .ToList();


                    SyncingCountDTO syncingCount = new()
                    {
                        TotalRecords = timesheetDataFromOracle.Count,
                        ValidRecords = validRecords.Count,
                        InValidRecords = timesheetDataFromOracle.Count - validRecords.Count
                    };

                    _logger.LogInformation($"A total of {syncingCount.TotalRecords} records found. and {syncingCount.ValidRecords} valid records found.");

                    var batchItemNumber = 1;

                    DataTable destinationTimesheetTableItems = GetTimeSheetTable(_logger);

                    for (int i = 0; i < validRecords.Count; i += batchSize)
                    {
                        _logger.LogInformation($"Batch {batchItemNumber} execution started.");

                        var batchTimesheetDataFromOracle = validRecords.Skip(i).Take(batchSize).ToList();

                        Task<bool> updateActualAllocation = _allocationHttpService.UpdateActualAllocationTime(batchTimesheetDataFromOracle, _logger);

                        Task<List<AddEmployeeProjectMappingRequestDto>> GetRequestPayloadForUpdatingEmployeeProjectMapping = this.GetRequestPayloadForUpdatingEmployeeProjectMapping(batchTimesheetDataFromOracle, _logger);

                        await Task.WhenAll(updateActualAllocation, GetRequestPayloadForUpdatingEmployeeProjectMapping);

                        await _employeeHttpService.AddEmployeeProjectMapping(GetRequestPayloadForUpdatingEmployeeProjectMapping.Result, _logger);

                        _logger.LogInformation($"Batch {batchItemNumber} execution completed.");
                        await InsertOrUpdateTimesheet(batchTimesheetDataFromOracle, destinationTimesheetTableItems, _logger);
                        batchItemNumber++;
                    }
                }
                else
                {
                    _logger.LogInformation($"No records found");
                }

                UpdateSchedularLogs(logsInstance, loggedNewGuid, SchedularLogsSyncStatus.SYNCED, String.Empty, _logger);

                _logger.LogInformation($"TimesheetActualsSchedulerTriggerTime Timer trigger function completed successfullt at: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                UpdateSchedularLogs(logsInstance, loggedNewGuid, SchedularLogsSyncStatus.FAILED, ex.Message, _logger);
                _logger.LogInformation($"TimesheetActualsSchedulerTriggerTime Timer trigger function failed with exception: {ex.Message}");
                throw;
            }
        }

        private async Task<bool> InsertOrUpdateTimesheet(List<OracleTimesheetResponseDto> timesheetDataFromOracle, DataTable destinationDt, ILogger _logger)
        {
            _logger.LogInformation("Start -> Oracle Timesheet data insert into Timesheet Table");

            try
            {
                var dtInsertItems = destinationDt.Clone();
                var dtUpdateItems = destinationDt.Clone();

                _logger.LogInformation("--SCHEDULER--Timesheet--GetInsertOrUpdateItems LINQ processing start");

                foreach (var item in timesheetDataFromOracle)
                {
                    var match = destinationDt.AsEnumerable().FirstOrDefault(row =>
                        string.Equals(row["employeecode"]?.ToString(), item.EmployeeMID, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(row["jobcode"]?.ToString(), item.JobCode, StringComparison.OrdinalIgnoreCase) &&
                        DateOnly.FromDateTime(DateTime.Parse(row["datelog"]?.ToString())) ==
                        DateOnly.FromDateTime(DateTime.Parse(item.DateLog?.ToString()))
                    );

                    var targetTable = match == null ? dtInsertItems : dtUpdateItems;
                    var newRow = targetTable.NewRow();

                    MapTimesheetRow(newRow, item);
                    targetTable.Rows.Add(newRow);
                }

                _logger.LogInformation($"Insert count: {dtInsertItems.Rows.Count}, Update count: {dtUpdateItems.Rows.Count}");

                if (dtInsertItems.Rows.Count > 0)
                    InsertItemInDestination(dtInsertItems, _logger);

                if (dtUpdateItems.Rows.Count > 0)
                    UpdateItemInDestination(dtUpdateItems, _logger);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TimesheetActualsSchedulerTriggerTime -- InsertOrUpdateTimesheet failed.");
                throw; // rethrow original stack trace
            }

            return true;
        }

        private void MapTimesheetRow(DataRow row, OracleTimesheetResponseDto item)
        {
            row["employeecode"] = item.EmployeeMID;
            row["employeename"] = item.EmpEmail;
            row["datelog"] = item.DateLog;
            row["totaltime"] = item.TotalTime;
            row["client"] = item.JobName;
            row["jobcode"] = item.JobCode;
            row["status"] = item.STATUS;
            row["designation_id"] = item.Designation;
            row["gradename"] = item.GradeName;
            row["chargeableflag"] = item.ChargeableFlag;
            row["rate"] = item.RATE;
            row["createdat"] = item.CreatedDate;
            row["modifiedat"] = item.ModifiedDate;
            row["createdby"] = "System";
            row["modifiedby"] = "System";
        }

        private void InsertItemInDestination(DataTable dtInsertItems, ILogger log)
        {
            var env = Environment.GetEnvironmentVariables();
            string connectionString = Convert.ToString(env[EnvAppSettingConstants.WCGTDbConnectionString]);
            string tableName = Convert.ToString(env[EnvAppSettingConstants.TimesheetTableName]);
            string columnsStr = Convert.ToString(env[EnvAppSettingConstants.TimesheetColumnName]);

            string[] columns = columnsStr.Split(Constant.SeparatorComma);

            // Build column list: "col1", "col2", ...
            string columnClause = string.Join(", ", columns.Select(col => $"\"{col}\""));

            var valuesList = new List<string>();

            foreach (DataRow row in dtInsertItems.Rows)
            {
                var formattedValues = columns.Select(col => FormatSqlValue(row[col])).ToArray();
                valuesList.Add($"({string.Join(", ", formattedValues)})");
            }

            string valuesClause = string.Join(", ", valuesList);

            string query = $"INSERT INTO \"{tableName}\" ({columnClause}) VALUES {valuesClause};";

            int rowsAffected = ExecuteQueryInPostgresSQL(connectionString, query, log);
        }


        private void UpdateItemInDestination(DataTable dtUpdateItems, ILogger log)
        {
            var env = Environment.GetEnvironmentVariables();
            string WCGTDbConnectionString = Convert.ToString(env[EnvAppSettingConstants.WCGTDbConnectionString]);
            string tableName = Convert.ToString(env[EnvAppSettingConstants.TimesheetTableName]);
            string updateColumns = Convert.ToString(env[EnvAppSettingConstants.TimesheetColumnName]);
            string whereColumns = Convert.ToString(env[EnvAppSettingConstants.TimesheetWhereColumnName]);

            string[] updateColumnList = updateColumns.Split(Constant.SeparatorComma);
            string[] whereColumnList = whereColumns.Split(Constant.SeparatorComma);

            foreach (DataRow row in dtUpdateItems.Rows)
            {
                // Build SET clause
                var setClauses = updateColumnList
                    .Select(col => $"\"{col}\" = {FormatSqlValue(row[col])}")
                    .ToArray();
                string setClause = string.Join(", ", setClauses);

                // Build WHERE clause
                var whereClauses = whereColumnList
                    .Select(col => $"\"{col}\" = {FormatSqlValue(row[col])}")
                    .ToArray();
                string whereClause = string.Join(" AND ", whereClauses);

                // Final Query
                string query = $"UPDATE \"{tableName}\" SET {setClause} WHERE {whereClause};";

                // Execute
                int rowsAffected = ExecuteQueryInPostgresSQL(WCGTDbConnectionString, query, log);
            }
        }
        private string FormatSqlValue(object value)
        {
            if (value == null || value == DBNull.Value)
                return "NULL";

            string strValue = value.ToString()?.Replace("'", "''"); // Escape single quotes

            // Add single quotes for strings/dates
            if (value is string)
                return $"'{strValue}'";
            if (value is DateTime)
            {
                return $"'{DateOnly.FromDateTime(DateTime.Parse(strValue))}'";
            }

            return strValue;
        }


        public async Task<List<AddEmployeeProjectMappingRequestDto>> GetRequestPayloadForUpdatingEmployeeProjectMapping(List<OracleTimesheetResponseDto> timesheetDataFromOracle, ILogger _logger)
        {
            List<string> jobCodes = timesheetDataFromOracle
                                    .DistinctBy(m => m.JobCode)
                                    .Select(m => m.JobCode)
                                    .ToList();

            List<GetOfferingSolutionsByJobCodeResponseDTO> offeringSolutionsData = await _projectHttpService.GetOfferingSolutionsByJobCode(jobCodes, _logger);

            List<AddEmployeeProjectMappingRequestDto> addEmployeeProjectMappingRequest = new();

            if (offeringSolutionsData != null && offeringSolutionsData.Count > 0)
            {
                foreach (var item in timesheetDataFromOracle)
                {
                    var offeringSolutionInstance = offeringSolutionsData
                        .Where(m =>
                            m.JobCode != null
                            && item.JobCode != null
                            && m.JobCode.ToLower() == item.JobCode.ToLower()
                        )
                        .FirstOrDefault();

                    if (offeringSolutionInstance != null)
                    {
                        addEmployeeProjectMappingRequest.Add(new()
                        {
                            Solution = offeringSolutionInstance.Solution,
                            SolutionId = offeringSolutionInstance.SolutionId,
                            Offering = offeringSolutionInstance.Offering,
                            OfferingId = offeringSolutionInstance.OfferingId,
                            EmpMID = item.EmployeeMID
                        });
                    }
                }
            }

            return addEmployeeProjectMappingRequest;
        }

        private int AddSchedularLogs(SchedularLogs schedularLogs, Guid guid, ILogger log, string type)
        {
            string sqlInsertQuery = $"INSERT INTO \"SchedularLog\"( \"Id\", \"Name\", \"Status\", \"SchedularStartTime\", \"SchedularEndTime\", \"Comments\", \"CreatedAt\", \"ModifiedAt\", \"IsActive\" , \"CreatedBy\" , \"ModifiedBy\" , \"Type\") VALUES ( '{guid}' , 'SyncTimesheet', '{SchedularLogsSyncStatus.INPROGRESS}', NOW(), null, null, NOW(),null , '1','{Constant.GT_HANDLER_ID}' , null , '{type}');";
            int rowsAffected = ExecuteQueryInPostgresSQL(schedularLogs.Connection, sqlInsertQuery, log);
            return rowsAffected;
        }

        private int UpdateSchedularLogs(SchedularLogs schedularLogs, Guid guid, string status, string comments, ILogger log)
        {
            comments = Helper.Helper.ReplaceInvalidChar(comments);

            string sqlUpdateQueryWithComments = $"UPDATE \"SchedularLog\" SET   \"Status\"='{status}', \"SchedularEndTime\"=NOW(), \"Comments\" = '{comments}',  \"ModifiedAt\"=NOW(), \"ModifiedBy\"='GT' WHERE \"Id\" = '{guid}';";
            string sqlUpdateQueryWithoutComments = $"UPDATE \"SchedularLog\" SET   \"Status\"='{status}', \"SchedularEndTime\"=NOW(), \"Comments\" = null,  \"ModifiedAt\"=NOW(), \"ModifiedBy\"='{GT_HANDLER_ID}' WHERE \"Id\" = '{guid}';";
            string sqlUpdateQuery = comments.IsNullOrEmpty() ? sqlUpdateQueryWithoutComments : sqlUpdateQueryWithComments;
            int rowsAffected = ExecuteQueryInPostgresSQL(schedularLogs.Connection, sqlUpdateQuery, log);
            return rowsAffected;
        }

        private int ExecuteQueryInPostgresSQL(string destinationConnectionString, string query, ILogger log)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(destinationConnectionString))
            {
                try
                {
                    DataTable dt = new();
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    if (doDetailedLogging) log.LogInformation($"--SCHEDULAR--ExecuteQueryInPostgresSQL--{query}");

                    NpgsqlCommand command = new(query, connection);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    log.LogInformation($"--Exception--ExecuteQueryInPostgresSQL--{ex.Message}");
                    log.LogInformation($"--Exception--ExecuteQueryInPostgresSQL--ErrorQuery-{query}");
                    log.LogError(ex, ex.Message);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private DataTable GetTimeSheetTable(ILogger log)
        {
            var environmentVaribles = Environment.GetEnvironmentVariables();
            string WCGTDbConnectionString = Convert.ToString(environmentVaribles[EnvAppSettingConstants.WCGTDbConnectionString]);
            string TimesheetTableName = Convert.ToString(environmentVaribles[EnvAppSettingConstants.TimesheetTableName]);

            string sqlQuery = $"SELECT * FROM \"{TimesheetTableName}\"";
            using (NpgsqlConnection connection = new NpgsqlConnection(WCGTDbConnectionString))
            {
                DataTable table = new();
                try
                {
                    DataTable dt = new();
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    NpgsqlCommand command = new(sqlQuery, connection);
                    NpgsqlDataAdapter adapter = new(command);
                    adapter.Fill(dt);

                    if (doDetailedLogging) log.LogInformation($"--SCHEDULAR--GetTimesheetItems--{JsonConvert.SerializeObject(dt)}");

                    return dt;
                }
                catch (Exception ex)
                {
                    log.LogInformation($"--Exception--GetTimeSheetTable--GetTimesheetItems--{ex.Message}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }

        }
        private DataTable FetchSchedularLogsByStatus(SchedularLogs schedularLogs, string status, ILogger log, string type)
        {
            string sqlQuery = $"select * from \"SchedularLog\" where \"Status\" = '{status}' AND \"Type\" = '{type}' order by \"SchedularStartTime\" desc LIMIT 1;";
            using (NpgsqlConnection connection = new NpgsqlConnection(schedularLogs.Connection))
            {
                DataTable table = new();
                try
                {
                    DataTable dt = new();
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    NpgsqlCommand command = new(sqlQuery, connection);
                    NpgsqlDataAdapter adapter = new(command);
                    adapter.Fill(dt);

                    if (doDetailedLogging) log.LogInformation($"--SCHEDULAR--FetchSchedularLogsByStatus--{JsonConvert.SerializeObject(dt)}");

                    return dt;
                }
                catch (Exception ex)
                {
                    log.LogInformation($"--Exception--FetchSchedularLogsByStatus--{ex.Message}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
