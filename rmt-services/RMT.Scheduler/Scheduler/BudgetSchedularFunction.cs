using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Npgsql;
using RMT.Scheduler.Constants;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.Helper;
using RMT.Scheduler.service;
using RMT.Scheduler.service.AzureServices;
using RMT.Scheduler.service.Configurations;
using RMT.Scheduler.service.Configurations.DTO;
using RMT.Scheduler.service.Project;
using RMT.Scheduler.service.WCGT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static RMT.Scheduler.Constants.Constant;

namespace RMT.Scheduler.Scheduler
{
    /// <summary>
    /// BudgetSchedular > to get the project budget details for all projects / project whose budget is modified
    /// and get the budget details for the project and update the budget indicatore accordingly 
    /// and send the notification for same
    /// </summary>
    public class BudgetSchedularFunction
    {
        private bool doDetailedLogging = true;
        //private readonly ILogger _logger;
        private readonly ITokenService _tokenService;
        private readonly IConfigurationService _configurationService;
        private readonly IAzureServiceBusService _azureServiceBusService;
        private readonly IWCGTHttpService _wcgtService;

        public BudgetSchedularFunction(ITokenService tokenService, IConfigurationService configurationService, IWCGTHttpService wcgtService, IAzureServiceBusService azureServiceBusService)
        {
            //_logger = loggerFactory.CreateLogger<BudgetSchedularFunction>();
            _tokenService = tokenService;
            _configurationService = configurationService;
            _wcgtService = wcgtService;
            _azureServiceBusService = azureServiceBusService;
        }

        [FunctionName("BudgetSchedular")]
        public async Task RunAsync([TimerTrigger("%BudgetSchedularTriggerTime%")] TimerInfo myTimer, ILogger _logger)
        {
            // bool execute = false;
            // if (!execute)
            // {
            //     return;
            // }
            var environmentVaribles = Environment.GetEnvironmentVariables();
            int updateBudgetBatchSize = Convert.ToInt32(environmentVaribles[Constant.EnvAppSettingConstants.UPDATE_BUDGET_BATCHSIZE]);
            updateBudgetBatchSize = updateBudgetBatchSize > 0 ? updateBudgetBatchSize : 500;
            string currentToken = await _tokenService.GetToken();

            _logger.LogInformation("Azure Function BudgetSchedular Started");

            string schedularType = SchedularLogsType.BudgetStatusScheduler;

            string ConfigDbConnectionString = Convert.ToString(environmentVaribles[EnvAppSettingConstants.ConfigDbConnectionString]);
            SchedularLogs logsInstance = new()
            {
                Connection = ConfigDbConnectionString
            };

            Guid loggedNewGuid = Guid.NewGuid();

            try
            {
                DataTable syncLogsTable = FetchSchedularLogsByStatus(logsInstance, SchedularLogsSyncStatus.SYNCED, _logger, schedularType);
                Object syncStartTime = null;
                if (syncLogsTable.Rows.Count > 0)
                {
                    syncStartTime = syncLogsTable.Rows[0]["SchedularStartTime"];
                }

                _logger.LogInformation($"BudgetSchedular--Adding schedular logs");
                AddSchedularLogs(logsInstance, loggedNewGuid, _logger, schedularType);
                _logger.LogInformation($"BudgetSchedular--Adding schedular logs completed");

                _logger.LogInformation($"BudgetSchedular--syncStartTime is {Convert.ToString(syncStartTime)}");

                ProjectBudgetHelper pbHelper = new ProjectBudgetHelper(_tokenService, _configurationService, _wcgtService, _azureServiceBusService, _logger);

                var resp = await pbHelper.GetProjectActualBudgetOverShoot(currentToken, _logger);

                _logger.LogInformation($"BudgetSchedular--GetProjectActualBudgetOverShoot--Completed");

                bool runForAllProjects = Convert.ToBoolean(environmentVaribles[EnvAppSettingConstants.BudhgetSchedulerForAllProject]);

                List<ProjectDTO> projectToProcess = new List<ProjectDTO>();
                if (runForAllProjects)
                {
                    projectToProcess = await pbHelper.GetAllProjectList(currentToken, _logger);
                    _logger.LogInformation($"BudgetSchedular--GetAllProjectList--Completed");
                }
                else
                {
                    List<string> modifiedJobCodes = new List<string>();

                    DateTime startDate;

                    if (DateTime.TryParseExact(Convert.ToString(syncStartTime),
                        Constant.BudgetSchedulerDateFormat,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out startDate))
                    {
                        Console.WriteLine("Budget Modified StartDate-", startDate);
                    }
                    else
                    {
                        startDate = DateTime.MinValue.Date;
                    }
                    DateTime endDate = DateTime.MaxValue.Date;
                    _logger.LogInformation($"BudgetSchedular--startDate is {Convert.ToString(startDate)}--endDate is {Convert.ToString(endDate)}");

                    modifiedJobCodes = await _wcgtService.GetProjectBudgetByModifiedDateRange(currentToken, startDate, endDate, _logger);
                    _logger.LogInformation($"BudgetSchedular--GetProjectBudgetByModifiedDateRange--Completed");

                    List<ProjectDTO> modifiedProjects = await pbHelper.GetAllProjectsForBudgetByJobCodes(modifiedJobCodes, currentToken, _logger);
                    _logger.LogInformation($"BudgetSchedular--GetAllProjectsForBudgetByJobCodes--Completed");

                    projectToProcess = modifiedProjects;

                }

                await pbHelper.ProcessProjectForBudget(_logger, updateBudgetBatchSize, currentToken, projectToProcess, true);
                _logger.LogInformation($"BudgetSchedular--ProcessProjectForBudget--Completed");

                UpdateSchedularLogs(logsInstance, loggedNewGuid, SchedularLogsSyncStatus.SYNCED, String.Empty, _logger);
                _logger.LogInformation($"BudgetSchedular--UpdateSchedularLogs--Completed");

                _logger.LogInformation($"BudgetSchedular Timer trigger function completed successfullt at: {DateTime.Now}");

            }
            catch (Exception ex)
            {
                UpdateSchedularLogs(logsInstance, loggedNewGuid, SchedularLogsSyncStatus.FAILED, ex.Message, _logger);
                _logger.LogInformation($"BudgetSchedular Timer trigger function failed with exception: {ex.Message}");

                _logger.LogError(ex, ex.Message);
                _logger.LogInformation("Azure Function BudgetSchedular Failed");
                throw;
            }
            _logger.LogInformation("Azure Function BudgetSchedular Completed");
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

                    if (doDetailedLogging) log.LogInformation($"--BudgetSchedular--FetchSchedularLogsByStatus--{JsonConvert.SerializeObject(dt)}");

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

        private int AddSchedularLogs(SchedularLogs schedularLogs, Guid guid, ILogger log, string type)
        {
            string sqlInsertQuery = $"INSERT INTO \"SchedularLog\"( \"Id\", \"Name\", \"Status\", \"SchedularStartTime\", \"SchedularEndTime\", \"Comments\", \"CreatedAt\", \"ModifiedAt\", \"IsActive\" , \"CreatedBy\" , \"ModifiedBy\" , \"Type\") VALUES ( '{guid}' , '{type}', '{SchedularLogsSyncStatus.INPROGRESS}', NOW(), null, null, NOW(),null , '1','{Constant.GT_HANDLER_ID}' , null , '{type}');";
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
    }
}
