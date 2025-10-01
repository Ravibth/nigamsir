using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using RMT.Scheduler.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static RMT.Scheduler.Constants.Constant;

namespace RMT.Scheduler.service.Oracle
{
    public class OracleService : IOracleService
    {
        public OracleConnection _oracleConnection;

        public OracleService() { }

        public async Task<List<OracleTimesheetResponseDto>> GetTimesheetDataFromOracle(string startTime, ILogger _logger)
        {
            _logger.LogInformation("Entered GetTimesheetDataFromOracle method");
            var environmentVaribles = Environment.GetEnvironmentVariables();
            string dateTimePattern = Convert.ToString(environmentVaribles[EnvAppSettingConstants.dateTimePattern]);
            string dateTimePatternForOracle = Convert.ToString(environmentVaribles[EnvAppSettingConstants.dateTimePatternForOracle]);
            string executeAdditionalOracleCommands = Convert.ToString(environmentVaribles[EnvAppSettingConstants.executeAdditionalOracleCommands]);

            _logger.LogInformation("Connecting to oracle");
            ConnectOracleConnection();
            _logger.LogInformation("Connected to oracle");


            if (String.IsNullOrEmpty(executeAdditionalOracleCommands) || executeAdditionalOracleCommands == "true")
            {
                OracleCommand oraCmdPre1 = new()
                {
                    CommandType = CommandType.Text,
                    CommandText = "alter session set current_schema =UAT_RMS",
                    Connection = _oracleConnection
                };
                int recordsPre1 = oraCmdPre1.ExecuteNonQuery();

                OracleCommand oraCmdPre2 = new()
                {
                    CommandType = CommandType.Text,
                    CommandText = "alter session set NLS_DATE_FORMAT ='dd-mm-yyyy hh:mi:ss'",
                    Connection = _oracleConnection
                };

                int recordsPre2 = oraCmdPre2.ExecuteNonQuery();
            }

            string queryText = String.IsNullOrEmpty(startTime) ? $"select * from VW_RMS_TIMESHEET" : $"select * from VW_RMS_TIMESHEET where \"Modified Date\" >= TO_DATE(\'{startTime}\',\'{dateTimePatternForOracle}\') and \"Designation\" is not null";

            _logger.LogInformation($"SQL Query formed is --> {queryText}");
            OracleCommand oraCmd = new()
            {
                CommandType = CommandType.Text,
                CommandText = queryText,
                Connection = _oracleConnection
            };

            _logger.LogInformation("Reading data from oracle");
            OracleDataReader reader = oraCmd.ExecuteReader();
            DataTable dataTable = new(); // Create DataTable once before looping
            try
            {
                _logger.LogInformation("Reading fields from oracle data");
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var columnName = reader.GetName(i);
                    var finalColumnName = String.Join("", columnName.Trim().Split(' '));
                    dataTable.Columns.Add(finalColumnName);
                }
                _logger.LogInformation("Reading fields from oracle data -- completed");
                _logger.LogInformation("Reading rows from oracle data");
                while (reader.Read())
                {
                    DataRow dt = dataTable.NewRow();
                    for (int column = 0; column < reader.FieldCount; column++)
                    {
                        var columnValue = reader.GetValue(column);
                            if (reader.GetFieldType(column).Name == "DateTime")
                        {
                                DateTime parsedDate;

                                DateTime.TryParseExact(Convert.ToString(columnValue), dateTimePattern, null,
                                  DateTimeStyles.None, out parsedDate);

                                dt[column] = parsedDate;
                        }
                        else
                        {
                            dt[column] = columnValue;
                        }
                    }
                    dataTable.Rows.Add(dt);
                }
                _logger.LogInformation("Reading rows from oracle data -- completed");
                string tableString = JsonConvert.SerializeObject(dataTable);
                List<OracleTimesheetResponseDto> result = JsonConvert.DeserializeObject<List<OracleTimesheetResponseDto>>(tableString);
                _logger.LogInformation("Returning data from oracle method");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured in fetching data from oracle: - {ex.Message}");
            }
            finally
            {

                _logger.LogInformation($"Closing oracle connection");
                reader.Close();
                //CloseOracleConnection();
                _logger.LogInformation($"Closing oracle connection -- completed");
            }

            return new List<OracleTimesheetResponseDto>();
        }

        public bool ConnectOracleConnection()
        {
            var environmentVaribles = Environment.GetEnvironmentVariables();
            string oraclePassword = Convert.ToString(environmentVaribles[EnvAppSettingConstants.oraclePassword]);
            string oracleUserID = Convert.ToString(environmentVaribles[EnvAppSettingConstants.oracleUserID]);
            string oracleDataSource = Convert.ToString(environmentVaribles[EnvAppSettingConstants.oracleDataSource]);

            OracleConnectionStringBuilder ocsb = new()
            {
                Password = oraclePassword,
                UserID = oracleUserID,
                DataSource = oracleDataSource
            };

            _oracleConnection = new OracleConnection();
            _oracleConnection.ConnectionString = ocsb.ConnectionString;
            _oracleConnection.Open();
            Console.WriteLine("Connection established (" + _oracleConnection.ServerVersion + ")");
            return true;
        }

        public bool CloseOracleConnection()
        {
            _oracleConnection.Close();
            _oracleConnection.Dispose();
            Console.WriteLine("Connection closed (" + _oracleConnection.ServerVersion + ")");
            return true;
        }
    }
}
