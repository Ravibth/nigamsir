using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
// using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using Newtonsoft.Json;
using Npgsql;
using RMT.Scheduler.Constants;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.Helper;
using RMT.Scheduler.service;
using RMT.Scheduler.service.AzureServices;
using RMT.Scheduler.service.WCGT;
using static RMT.Scheduler.Constants.Constant;

namespace RMT.Scheduler.Scheduler
{
    /// <summary>
    /// WCGTDataMigrationScheduler > To migrate all the data from WCGT db to diff RMS database and send respective notification emails also.
    /// </summary>
    public class WCGTDataMigrationScheduler
    {
        private readonly IWCGTHttpService _wcgtHttpService;
        private readonly IAzureServiceBusService _azureServiceBusService;
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly ITokenService _tokenService;

        private bool doDetailedLogging = true;

        public WCGTDataMigrationScheduler(IWCGTHttpService wcgtHttpService, IAzureServiceBusService azureServiceBusService, IAzureBlobStorageService azureBlobStorageService, ITokenService tokenService)
        {
            _wcgtHttpService = wcgtHttpService;
            _azureServiceBusService = azureServiceBusService;
            _azureBlobStorageService = azureBlobStorageService;
            _tokenService = tokenService;
        }

        [FunctionName("WCGTDataMigrationScheduler")]
        public async Task RunAsync([TimerTrigger("%WCGTDataMigrationSchedulerTriggerTime%")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation("Azure Function WCGTDataMigrationScheduler Started");

            await SyncFunctionHandlerAsync(log);

            log.LogInformation("Azure Function WCGTDataMigrationScheduler Completed");
        }

        public async Task SyncFunctionHandlerAsync(ILogger log)
        {
            string schedularType = SchedularLogsType.SyncFunction;

            try
            {
                var environmentVariables = Environment.GetEnvironmentVariables();
                doDetailedLogging = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.DO_DETAILED_LOGGING]) == "true";

                //var filePath = "C:/GT-RMT/gt-rmt-repos/rmt-services/RMT.Scheduler/CompareMapping.json";
                var fileRelativePath = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.SCHEDULER_MAPPING_FILEPATH]);

                var dllPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var dllParentPath = Path.GetDirectoryName(dllPath);
                var directoryPath = Path.GetDirectoryName(dllParentPath);

                log.LogInformation($"--SCHEDULAR--CurrentDirectory--{directoryPath}");

                string filePath = Path.Combine(directoryPath + fileRelativePath);

                log.LogInformation($"--SCHEDULAR--fileFullPath--{filePath}");


                if (File.Exists(filePath))
                {
                    log.LogInformation($"--SCHEDULAR--filepath--{filePath}-- file found");
                    //using (StreamReader reader = new StreamReader(filePath))
                    {
                        string jsonFileContent = string.Empty;

                        bool readJsonFromFileSystem = Convert.ToString(environmentVariables["BlobWCGTReadFromFileSystem"]) == "true";

                        if (readJsonFromFileSystem)
                        {
                            StreamReader reader = new StreamReader(filePath);
                            jsonFileContent = reader.ReadToEnd();
                        }
                        else
                        {
                            jsonFileContent = ReadJsonFileContent(environmentVariables, log);
                        }

                        if (string.IsNullOrEmpty(jsonFileContent))
                        {
                            throw new Exception("WCGT Sync Mapping Json file content is empty. Please update the Json file Content or check Blob Storage Setting! Is reading from FileSystem-" + readJsonFromFileSystem);
                        }

                        CompareDTO listSyncMapping = JsonConvert.DeserializeObject<CompareDTO>(jsonFileContent.ToString());
                        DataTable eventsDataTable = new DataTable();
                        eventsDataTable.Columns.Add("EventType", typeof(string));
                        eventsDataTable.Columns.Add("SourceDataTableName", typeof(string));
                        eventsDataTable.Columns.Add("SourceData", typeof(string));
                        eventsDataTable.Columns.Add("DestinationDataTableName", typeof(string));
                        eventsDataTable.Columns.Add("DestinationData", typeof(string));
                        //eventsDataTable.Columns.Add("EventCategory", typeof(string));
                        DataTable syncLogsTable = FetchSchedularLogsByStatus(listSyncMapping.SchedularLogs, SchedularLogsSyncStatus.SYNCED, log, schedularType);
                        Object syncStartTime = null;
                        if (syncLogsTable.Rows.Count > 0)
                        {
                            syncStartTime = syncLogsTable.Rows[0]["SchedularStartTime"];
                        }
                        //add to schedular logs
                        Guid newGuid = Guid.NewGuid();
                        log.LogInformation($"--SCHEDULAR--LOGS--LAST_SUCCESSFULL_SYNC_DATE_{syncStartTime}");
                        log.LogInformation("--SCHEDULAR--LOGS--FOUND");

                        int insertedRowsCount = AddSchedularLogs(listSyncMapping.SchedularLogs, newGuid, log, schedularType);
                        log.LogInformation("--SCHEDULAR--LOGS--INSERTED");

                        try
                        {
                            bool IsAllNodesSuccess = true;
                            string exceptionMsg = string.Empty;

                            log.LogInformation("--SCHEDULAR--SYNC-STARTED");

                            foreach (var syncMapping in listSyncMapping.Mappings)
                            {
                                try
                                {
                                    if (syncMapping.IsExecutable == true)
                                    {
                                        log.LogInformation("--SCHEDULAR--syncMapping- Src-" + syncMapping.SourceTableName + " Dest-" + syncMapping.DestinationTableName + ", isExecutable -{0}", syncMapping.IsExecutable);

                                        log.LogInformation("--SCHEDULAR--syncMapping-STARTED NodeTitle " + syncMapping.NodeTitle + " Src-" + syncMapping.SourceTableName + " Dest-" + syncMapping.DestinationTableName);
                                        DataTable sourceTableItems = GetSourceTableItems(syncMapping, Convert.ToString(syncStartTime), log);
                                        DataTable destinationTableItems = new DataTable();
                                        if (sourceTableItems.Rows.Count > 0)
                                        {
                                            if (syncMapping.IsSyncToBeMade != null && syncMapping.IsSyncToBeMade == false)
                                            {
                                                log.LogInformation("--SCHEDULAR--SyncFunctionHandlerAsync--EventCreation-STARTED Src-");
                                                CreateEvent(sourceTableItems, destinationTableItems, syncMapping, eventsDataTable, log);
                                                log.LogInformation("--SCHEDULAR--SyncFunctionHandlerAsync--EventCreation-END Src-");

                                            }
                                            else
                                            {
                                                if (syncMapping.IsInsertCheck.IsNullOrEmpty())
                                                {
                                                    log.LogInformation("--SCHEDULAR--GetDestinationTableItems-STARTED Src-" + syncMapping.SourceTableName + "-Count-" + sourceTableItems?.Rows?.Count + "- Dest-" + syncMapping.DestinationTableName);

                                                    destinationTableItems = GetDestinationTableItems(syncMapping, sourceTableItems, log);

                                                    log.LogInformation("--SCHEDULAR--GetDestinationTableItems-COMPLETED Src-" + syncMapping.SourceTableName + " Dest-" + syncMapping.DestinationTableName);
                                                }

                                                log.LogInformation("--SCHEDULAR--SyncFunctionHandlerAsync--GetInsertOrUpdateItems-STARTED Src-" + syncMapping.SourceTableName + " Dest-" + syncMapping.DestinationTableName);

                                                GetInsertOrUpdateItems(sourceTableItems, destinationTableItems, syncMapping, eventsDataTable, log);

                                                log.LogInformation("--SCHEDULAR--SyncFunctionHandlerAsync--GetInsertOrUpdateItems-COMPLETED Src-" + syncMapping.SourceTableName + " Dest-" + syncMapping.DestinationTableName);
                                            }

                                        }
                                        else
                                        {
                                            log.LogInformation("--SCHEDULAR--sourceTableItems--No rows found");
                                        }
                                        log.LogInformation("--SCHEDULAR--syncMapping-COMPLETED NodeTitle " + syncMapping.NodeTitle + " Src-" + syncMapping.SourceTableName + " Dest-" + syncMapping.DestinationTableName);
                                    }
                                    else
                                    {
                                        log.LogInformation("--SCHEDULAR--syncMapping- Src-" + syncMapping.SourceTableName + " Dest-" + syncMapping.DestinationTableName + ", isExecutable -{0}", syncMapping.IsExecutable);
                                    }
                                }
                                catch (Exception ex11)
                                {
                                    log.LogInformation("--SCHEDULAR--syncMapping-Failed NodeTitle " + syncMapping.NodeTitle + " Src-" + syncMapping.SourceTableName + " Dest-" + syncMapping.DestinationTableName);
                                    log.LogInformation("--SCHEDULAR--syncMapping--{3}--Exception, Message-{0}, StackTrace-{1},InnerException-{2}", ex11.Message, ex11.StackTrace, ex11.InnerException, syncMapping.NodeTitle);
                                    exceptionMsg += "--SyncMapping--" + string.Format("Message-{0},StackTrace-{1},InnerException-{2}", ex11.Message, ex11.StackTrace, ex11.InnerException);
                                    IsAllNodesSuccess = false;
                                }
                            }

                            if (IsAllNodesSuccess)
                            {
                                log.LogInformation("--SCHEDULAR--SYNC-END");

                                int updatedRowCounts = UpdateSchedularLogs(listSyncMapping.SchedularLogs, newGuid, SchedularLogsSyncStatus.SYNCED, string.Empty, log);

                                log.LogInformation("--SCHEDULAR--SYNC-END, RowCount-" + updatedRowCounts);
                            }
                            else
                            {
                                log.LogInformation("--SCHEDULAR--SYNC-FAILED");

                                int updatedRowCounts = UpdateSchedularLogs(listSyncMapping.SchedularLogs, newGuid, SchedularLogsSyncStatus.FAILED, exceptionMsg, log);

                                log.LogInformation("--SCHEDULAR--SYNC-FAILED, RowCount-" + updatedRowCounts);
                            }

                            // //wip
                            // DataTable dbEventTable = eventsDataTable.AsEnumerable().Where(a => Convert.ToString(a["EventCategory"]) == EventCategoryNames.DATABASE_EVENT).CopyToDataTable();
                            // DataTable publishEventTable = eventsDataTable.AsEnumerable().Where(a => Convert.ToString(a["EventCategory"]) == EventCategoryNames.PUBLISH_EVENT).CopyToDataTable();

                            // if (dbEventTable != null && dbEventTable.Rows.Count > 0)
                            // {
                            //     log.LogInformation("--SCHEDULAR--DatabaseEvent-STARTED ");

                            //     //Process database events
                            //     //List<SyncEventPayload> syncEventPayload = await CreateAndPublishSyncEvent(eventsDataTable, log);

                            //     log.LogInformation("--SCHEDULAR--DatabaseEvent-COMPLETED ");
                            // }

                            // else
                            // {
                            //     log.LogInformation("--SCHEDULAR--DatabaseEvent-No Event Found ");
                            // }

                            // if (publishEventTable != null && publishEventTable.Rows.Count > 0)
                            // {
                            log.LogInformation("--SCHEDULAR--CreateAndPublishSyncEvent-STARTED ");

                            List<SyncEventPayload> syncEventPayload = await CreateAndPublishSyncEvent(eventsDataTable, log);

                            log.LogInformation("--SCHEDULAR--CreateAndPublishSyncEvent-COMPLETED ");
                            // }
                            // else
                            // {
                            //     log.LogInformation("--SCHEDULAR--CreateAndPublishSyncEvent-No Event Found ");
                            // }

                            log.LogInformation("--SCHEDULAR--SYNC-FINISHED");

                        }
                        catch (Exception ex)
                        {
                            log.LogInformation("--SCHEDULAR--Exception, Message-{0}, StackTrace-{1},InnerException-{2}", ex.Message, ex.StackTrace, ex.InnerException);
                            UpdateSchedularLogs(listSyncMapping.SchedularLogs, newGuid, SchedularLogsSyncStatus.FAILED, string.Format("Message-{0},StackTrace-{1},InnerException-{2}", ex.Message, ex.StackTrace, ex.InnerException), log);
                            throw;
                        }

                        //update Schedular logs


                    }
                }
                else
                {
                    log.LogInformation($"--SCHEDULAR--filepath--{filePath}-- file not found");

                }
            }
            catch (Exception ex)
            {
                log.LogInformation($"--Exception--SyncFunctionHandlerAsync--{ex.Message}");
                //_logger.LogError(ex, ex.Message);
                //UpdateSchedularLogs(listSyncMapping.)
                throw;
            }
        }

        #region WCGTData Migration Helper Methods

        private void CreateEvent(DataTable sourceTableItems, DataTable destinationTableItems, Mapping syncMapping, DataTable eventDataTable, ILogger log)
        {
            string[] events = syncMapping.EventsOnInsert.Split(SeparatorPipe);
            foreach (DataRow sourceRow in sourceTableItems.Rows)
            {

                CreateEventAfterSyncResult(events.ToList(), syncMapping.SourceTableName, syncMapping.DestinationTableName, sourceRow, null, eventDataTable, log);
            }
        }

        private string ReadJsonFileContent(IDictionary envVariables, ILogger log)
        {
            string jsonContent = string.Empty;

            try
            {
                string blobContainerName = Convert.ToString(envVariables[Constant.EnvAppSettingConstants.BLOB_ContainerName]);
                string blobFileName = Convert.ToString(envVariables[Constant.EnvAppSettingConstants.BLOB_WCGT_Sync_FileName]);

                jsonContent = _azureBlobStorageService.ReadBlobStorageFileContent(blobContainerName, blobFileName, log);
                log.LogInformation($"--SCHEDULAR--ReadJsonFileContent--Successfully");

            }
            catch (Exception ex)
            {
                log.LogInformation($"--Exception--ReadJsonFileContent--{ex.Message}");
            }

            return jsonContent;
        }

        private int AddSchedularLogs(SchedularLogs schedularLogs, Guid guid, ILogger log, string type)
        {
            //Guid newGuid = Guid.NewGuid();
            string sqlInsertQuery = $"INSERT INTO \"SchedularLog\"( \"Id\", \"Name\", \"Status\", \"SchedularStartTime\", \"SchedularEndTime\", \"Comments\", \"CreatedAt\", \"ModifiedAt\", \"IsActive\" , \"CreatedBy\" , \"ModifiedBy\" , \"Type\") VALUES ( '{guid}' , 'SyncWCGTWithRMS', '{SchedularLogsSyncStatus.INPROGRESS}', NOW(), null, null, NOW(),null , '1','{Constant.GT_HANDLER_ID}' , null , '{type}');";
            int rowsAffected = ExecuteQueryInPostgresSQL(schedularLogs.Connection, sqlInsertQuery, log);
            return rowsAffected;
        }

        private int UpdateSchedularLogs(SchedularLogs schedularLogs, Guid guid, string status, string comments, ILogger log)
        {
            comments = Helper.Helper.ReplaceInvalidChar(comments);

            string sqlUpdateQueryWithComments = $"UPDATE \"SchedularLog\" SET   \"Status\"='{status}', \"SchedularEndTime\"=NOW(), \"Comments\" = '{comments}',  \"ModifiedAt\"=NOW(), \"ModifiedBy\"='GT' WHERE \"Id\" = '{guid}';";
            string sqlUpdateQueryWithoutComments = $"UPDATE \"SchedularLog\" SET   \"Status\"='{status}', \"SchedularEndTime\"=NOW(), \"Comments\" = null,  \"ModifiedAt\"=NOW(), \"ModifiedBy\"='{Constant.GT_HANDLER_ID}' WHERE \"Id\" = '{guid}';";
            string sqlUpdateQuery = comments.IsNullOrEmpty() ? sqlUpdateQueryWithoutComments : sqlUpdateQueryWithComments;
            int rowsAffected = ExecuteQueryInPostgresSQL(schedularLogs.Connection, sqlUpdateQuery, log);
            return rowsAffected;
        }

        private DataTable FetchSchedularLogsByStatus(SchedularLogs schedularLogs, string status, ILogger log, string type)
        {
            string sqlQuery = $"select * from \"SchedularLog\" where \"Status\" = '{status}' AND \"Type\" = '{type}' order by \"SchedularStartTime\" desc LIMIT 1;";
            using (NpgsqlConnection connection = new NpgsqlConnection(schedularLogs.Connection))
            {
                DataTable table = new DataTable();
                try
                {
                    DataTable dt = new DataTable();
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
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

        private void GetInsertOrUpdateItems(DataTable sourceTableItems, DataTable destinationTableItems, Mapping syncMapping, DataTable eventDataTable, ILogger log)
        {
            DataTable dtInsertItems = new DataTable();
            dtInsertItems = sourceTableItems.Clone();

            DataTable dtUpdateItems = new DataTable();
            dtUpdateItems = sourceTableItems.Clone();

            List<string> UpdateQueries = new List<string>();

            log.LogInformation($"--SCHEDULAR--GetInsertOrUpdateItems-Foreach start");

            foreach (DataRow sourceTableRow in sourceTableItems.Rows)
            {
                try
                {
                    //Dictionary<string, List<string>> dicConditions = new Dictionary<string, List<string>>();
                    Dictionary<string, string> dicConditions = new Dictionary<string, string>();
                    string sourceColumnValue = string.Empty;
                    if (syncMapping.IsInsertCheck.IsNullOrEmpty())
                    {
                        foreach (var columnsToCompare in syncMapping.ColumnsToCompare.Split(Constant.SeparatorPipe))
                        {
                            string[] keyArray = columnsToCompare.Split(Constant.SeparatorColon);
                            string[] sourceColumns = keyArray[0].Split(Constant.DoubleAtTheRate);
                            string sourceColumnName = sourceColumns[0];
                            if (sourceColumns.Length == 2)
                            {
                                switch (sourceColumns[1])
                                {
                                    case "null":
                                        dicConditions.Add(keyArray[1], null);

                                        break;
                                    default:
                                        dicConditions.Add(keyArray[1], Convert.ToString(sourceColumns[1]));

                                        break;
                                }
                            }
                            else
                            {
                                //dicConditions.Add(Convert.ToString(sourceTableRow[sourceColumnName]));
                                dicConditions.Add(keyArray[1], Convert.ToString(sourceTableRow[sourceColumnName]));
                            }
                        }
                        var dtCompareResult = destinationTableItems
                        .AsEnumerable().Where(c =>
                             dicConditions.All(kv => (c.Field<string>(kv.Key) == null && kv.Value == string.Empty) ? true : (c.Field<string>(kv.Key) == kv.Value))
                        );

                        if (doDetailedLogging) log.LogInformation($"--SCHEDULAR--GetInsertOrUpdateItems-InsideMethod-Foreach dtCompareResult-");

                        if (syncMapping.IsInsertToBeMade == false || dtCompareResult.Any())
                        {
                            // update collection
                            if (syncMapping.IsUpdateToBeMade == true)
                            {
                                if (doDetailedLogging) log.LogInformation($"--SCHEDULAR--GetInsertOrUpdateItems-Foreach dtUpdateItems IsUpdateToBeMade true");
                                dtUpdateItems.ImportRow(sourceTableRow);
                            }
                        }
                        else
                        {
                            // insert collection
                            if (doDetailedLogging) log.LogInformation($"--SCHEDULAR--GetInsertOrUpdateItems-Foreach dtInsertItems IsUpdateToBeMade false");
                            dtInsertItems.ImportRow(sourceTableRow);
                        }
                    }
                    else
                    {
                        DataTable destinationRow = GetDestinationRow(syncMapping, sourceTableRow, log);
                        if (destinationRow == null)
                        {
                            dtInsertItems.ImportRow(sourceTableRow);
                            if (doDetailedLogging) log.LogInformation($"--SCHEDULAR--GetInsertOrUpdateItems-Foreach else dtInsertItems is null");
                        }
                        else
                        {
                            destinationTableItems.Merge(destinationRow);
                            dtUpdateItems.ImportRow(sourceTableRow);
                            if (doDetailedLogging) log.LogInformation($"--SCHEDULAR--GetInsertOrUpdateItems-Foreach else dtUpdateItems is not null");
                        }
                    }

                }
                catch (Exception ex)
                {
                    log.LogInformation($"--Exception--GetInsertOrUpdateItems-Foreach Exception. Messasge-{ex.Message}, StackTrace-{ex.StackTrace}, InnerException-{ex.InnerException}");
                    throw;
                }
            }

            log.LogInformation($"--SCHEDULAR--GetInsertOrUpdateItems-Foreach end");

            //todo: - to be discussed
            if (dtInsertItems != null && dtInsertItems.Rows != null)
            {
                if (dtInsertItems.Rows.Count > 0)
                {
                    log.LogInformation($"--SCHEDULAR--GetInsertOrUpdateItems-InsertItemInDestination");
                    InsertItemInDestination(dtInsertItems, syncMapping, eventDataTable, log);
                }
                if (destinationTableItems.Rows.Count > 0 && dtUpdateItems.Rows.Count > 0)
                {
                    log.LogInformation($"--SCHEDULAR--GetInsertOrUpdateItems-UpdateItemInDestination");
                    UpdateItemInDestination(dtUpdateItems, syncMapping, destinationTableItems, eventDataTable, log);
                }
            }
            else
            {
                log.LogInformation($"--SCHEDULAR--GetInsertOrUpdateItems-dtInsertItems is null or empty");
            }

        }

        private DataTable GetDestinationRow(Mapping syncMapping, DataRow sourceDataRow, ILogger log)
        {
            string sqlQuery = syncMapping.IsInsertCheck;
            string finalSqlQuey = Helper.Helper.QueryStringBuilderByRegex(sqlQuery, sourceDataRow);
            using (NpgsqlConnection connection = new NpgsqlConnection(syncMapping.DestinationConnection))
            {
                DataTable dt = new DataTable();
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(finalSqlQuey, connection);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        return dt;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    log.LogInformation($"--Exception--GetDestinationRow Exception. Messasge-{ex.Message}, StackTrace-{ex.StackTrace}, InnerException-{ex.InnerException}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        //Method not in use
        //private string GeneratePostgressUpdateQuery(DataRow dtUpdateItem, DataRow destinationTableItem, Mapping syncMapping, ILogger log)
        //{
        //    string query = $"UPDATE \"{syncMapping.DestinationTableName}\"  SET ";
        //    string value = string.Empty;
        //    string whereClause = " WHERE ";
        //    string[] columnColl = syncMapping.ColumnMapping.Split(Constant.SeparatorPipe);
        //    for (int i = 0; i < columnColl.Length; i++)
        //    {
        //        string[] columns = columnColl[i].Split(Constant.SeparatorColon);
        //        string columnToUpdate = columns[1];

        //        string ignoreOnUpdate = syncMapping.IgnoreOnUpdate;
        //        if (!ignoreOnUpdate.IsNullOrEmpty())
        //        {
        //            string[] arrayIgnoreOnUpdate = ignoreOnUpdate.Split(Constant.SeparatorPipe);
        //            if (!arrayIgnoreOnUpdate.Contains(columnToUpdate))
        //            {
        //                value = value + "\"" + columnToUpdate + "\"" + " = " + '\'' + Helper.Helper.ReplaceInvalidChar(dtUpdateItem[columns[0]]) + '\'' + ",";
        //            }
        //        }
        //        else
        //        {
        //            if (columns.Length > 2)
        //            {
        //                switch (columns[2])
        //                {
        //                    case "bool":
        //                        value = value + "\"" + columnToUpdate + "\"" + " = " + Convert.ToByte(dtUpdateItem[columns[0]]) + ",";
        //                        break;
        //                    default:
        //                        value = value + '"' + columnToUpdate + '"' + " = " + '\'' + Helper.Helper.ReplaceInvalidChar(dtUpdateItem[columns[0]]) + '\'' + ",";
        //                        break;
        //                }
        //            }
        //            else
        //            {
        //                if (dtUpdateItem.IsNull(columns[0]))
        //                {
        //                    value = value + '"' + columnToUpdate + '"' + " = null ,";
        //                }
        //                else
        //                {
        //                    value = value + '"' + columnToUpdate + '"' + " = " + '\'' + Helper.Helper.ReplaceInvalidChar(dtUpdateItem[columns[0]]) + '\'' + ",";

        //                }
        //            }
        //        }
        //    }
        //    value = value.Remove(value.Length - 1, 1);
        //    string[] whereClauseArray = syncMapping.UpdateCondition.Split(Constant.SeparatorPipe);
        //    for (int i = 0; i < whereClauseArray.Length; i++)
        //    {
        //        string[] columns = whereClauseArray[i].Split(Constant.SeparatorColon);
        //        switch (columns[1])
        //        {
        //            case Constant.INTEGER:
        //                whereClause = whereClause + '"' + columns[0] + '"' + " = " + Helper.Helper.ReplaceInvalidChar(destinationTableItem[columns[0]]) + " AND ";
        //                break;
        //            default:
        //                whereClause = whereClause + '"' + columns[0] + '"' + " = " + '\'' + Helper.Helper.ReplaceInvalidChar(destinationTableItem[columns[0]]) + '\'' + " AND ";
        //                break;
        //        }
        //    }
        //    whereClause = whereClause.Remove(whereClause.Length - 5, 5) + ';';
        //    query = query + value + whereClause;
        //    return query;
        //}

        private void UpdateItemInDestination(DataTable dtUpdateItems, Mapping syncMapping, DataTable destinationDataTable, DataTable eventDataTable, ILogger log)
        {
            log.LogInformation($"--SCHEDULAR--UpdateItemInDestination--Start");
            int batchCount = 1;
            string batchQuery = string.Empty;
            DataTable destinationEventDataTable = new DataTable();
            destinationEventDataTable = destinationDataTable.Clone();
            destinationEventDataTable.Columns.Add("Events", typeof(string));
            foreach (DataRow updateRow in dtUpdateItems.Rows)
            {
                //try
                //{
                string query = $"UPDATE \"{syncMapping.DestinationTableName}\"  SET ";
                string value = string.Empty;
                string whereClause = " WHERE ";
                string[] columnColl = syncMapping.ColumnMapping.Split(Constant.SeparatorPipe);
                for (int i = 0; i < columnColl.Length; i++)
                {

                    string[] columns = columnColl[i].Split(Constant.SeparatorColon);
                    string[] columnToUpdateArray = columns.Length > 0 ? columns[1].Split(Constant.DoubleAtTheRate) : new string[] { string.Empty };
                    string columnToUpdate = columnToUpdateArray[0];//destination
                    string ignoreOnUpdate = syncMapping.IgnoreOnUpdate;
                    if (!ignoreOnUpdate.IsNullOrEmpty() && ignoreOnUpdate.Split(Constant.SeparatorPipe).Contains(columnToUpdate))
                    {
                        continue;

                    }
                    else
                    {
                        if (columns.Length > 2)
                        {
                            string destinationColumnValue = GetColumnValueByParams(syncMapping.SourceTableName, columns[0], syncMapping.DestinationTableName, columnToUpdate, Constant.UPDATE_OPRATION, syncMapping, updateRow, log);
                            if (!destinationColumnValue.IsNullOrEmpty())
                            {
                                if (destinationColumnValue == "null")
                                {
                                    value = value + " null , ";
                                }
                                else if (destinationColumnValue.StartsWith(TypeConstant.SQL_QUERY))
                                {
                                    string destinationColumnExactValue = destinationColumnValue.Substring(TypeConstant.SQL_QUERY.Length);
                                    value = value + '"' + columnToUpdate + '"' + " = " + destinationColumnExactValue + ",";
                                }
                                else
                                {
                                    value = value + '"' + columnToUpdate + '"' + " = " + '\'' + Helper.Helper.ReplaceInvalidChar(destinationColumnValue) + '\'' + ",";
                                }
                            }
                            else
                            {
                                switch (columns[2])
                                {
                                    case "bool":
                                        value = value + "\"" + columnToUpdate + "\"" + " = " + Convert.ToBoolean(updateRow[columns[0]]) + ",";
                                        break;
                                    default:
                                        value = value + '"' + columnToUpdate + '"' + " = " + '\'' + Helper.Helper.ReplaceInvalidChar(Convert.ToString(updateRow[columns[0]])) + '\'' + ",";
                                        break;
                                }
                            }

                        }
                        else
                        {
                            string destinationColumnValue = GetColumnValueByParams(syncMapping.SourceTableName, columns[0], syncMapping.DestinationTableName, columnToUpdate, Constant.UPDATE_OPRATION, syncMapping, updateRow, log);
                            if (!destinationColumnValue.IsNullOrEmpty())
                            {
                                if (destinationColumnValue == "null")
                                {
                                    value = value + " null , ";
                                }
                                else if (destinationColumnValue.StartsWith(TypeConstant.SQL_QUERY))
                                {
                                    string destinationColumnExactValue = destinationColumnValue.Substring(TypeConstant.SQL_QUERY.Length);
                                    value = value + '"' + columnToUpdate + '"' + " = " + destinationColumnExactValue + ",";
                                }
                                else
                                {
                                    value = value + '"' + columnToUpdate + '"' + " = " + '\'' + Helper.Helper.ReplaceInvalidChar(destinationColumnValue) + '\'' + ",";
                                }
                            }
                            else if (updateRow.IsNull(columns[0]))
                            {
                                value = value + '"' + columnToUpdate + '"' + " = null ,";

                            }
                            else
                            {
                                value = value + '"' + columnToUpdate + '"' + " = " + '\'' + Convert.ToString(Helper.Helper.ReplaceInvalidChar(updateRow[columns[0]])) + '\'' + ",";

                            }
                        }
                    }
                }

                value = value.Remove(value.Length - 1, 1);
                string[] whereClauseArray = syncMapping.PrimaryKey.Split(Constant.SeparatorPipe);

                Dictionary<string, string> dictConditions = new Dictionary<string, string>();
                //string[] whereClauseArray = syncMapping.ColumnsToCompare.Split(Constant.SeparatorPipe);
                for (int i = 0; i < whereClauseArray.Length; i++)
                {
                    string[] columns = whereClauseArray[i].Split(Constant.SeparatorColon);
                    if (columns.Length == 1)
                    {
                        string[] destinationColumns = columns[0].Split(Constant.DoubleAtTheRate);
                        string destColumnName = destinationColumns[0];
                        string destColumnValue = destinationColumns[1];
                        switch (destColumnValue)
                        {
                            case "null":
                                whereClause = whereClause + '"' + destColumnName + '"' + " is null and ";
                                dictConditions.Add(destColumnName, null);
                                break;
                            default:
                                whereClause = whereClause + '"' + destColumnName + '"' + " = " + '\'' + Helper.Helper.ReplaceInvalidChar(destColumnValue) + '\'' + " and ";
                                dictConditions.Add(destColumnName, Convert.ToString(updateRow[destColumnValue]));

                                break;
                        }
                        //whereClause = whereClause + '"' + columns[1] + '"' + " = " + '\'' + updateRow[columns[0]] + '\'' + " and ";
                        //dictConditions.Add(columns[1], Convert.ToString(updateRow[columns[0]]));
                    }
                    else if (columns.Length == 2)
                    {
                        bool isEmpty = true;
                        foreach (DataRow row in destinationDataTable.Rows)
                        {
                            if (!Convert.ToString(updateRow[columns[0]]).IsNullOrEmpty() && Convert.ToString(row[columns[1]]) == Convert.ToString(updateRow[columns[0]]))
                            {
                                whereClause = whereClause + '"' + columns[1] + '"' + " = " + '\'' + Helper.Helper.ReplaceInvalidChar(updateRow[columns[0]]) + '\'' + " and ";
                                isEmpty = false;
                                dictConditions.Add(columns[1], Convert.ToString(updateRow[columns[0]]));
                                break;
                            }
                        }
                        if (isEmpty == true)
                        {
                            //whereClause = whereClause + '"' + columns[1] + '"' + " = " + '\''+ DBNull.Value  + '\'' + " and ";
                            whereClause = whereClause + '"' + columns[1] + '"' + " is null and ";
                            dictConditions.Add(columns[1], null);

                        }
                    }
                    else if (columns.Length == 4)
                    {
                        switch (columns[3])
                        {
                            case Constant.SQL:
                                DataTable datatable = PostgresQueryFormation(columns[1], updateRow, syncMapping.DestinationConnection, log);
                                switch (columns[2])
                                {
                                    case Constant.INTEGER:
                                        whereClause = whereClause + '"' + datatable.Columns[0].ColumnName + '"' + " = " + datatable.Rows[0][datatable.Columns[0].ColumnName] + " and ";
                                        dictConditions.Add(datatable.Columns[0].ColumnName, Convert.ToString(datatable.Rows[0][datatable.Columns[0].ColumnName]));
                                        break;
                                    default:
                                        whereClause = whereClause + '"' + datatable.Columns[0].ColumnName + '"' + " = " + '\'' + Helper.Helper.ReplaceInvalidChar(datatable.Rows[0][datatable.Columns[0].ColumnName]) + '\'' + " and ";
                                        dictConditions.Add(datatable.Columns[0].ColumnName, Convert.ToString(datatable.Rows[0][datatable.Columns[0].ColumnName]));
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                whereClause = whereClause.Remove(whereClause.Length - 5, 5) + ';';
                //whereClause to include IsActive = true;
                //Delete the current one and Insert the current one

                if (!string.IsNullOrEmpty(syncMapping.ExtraValueCondtion) && !string.IsNullOrEmpty(syncMapping.ExtraWhereCondtion))
                {
                    value = value + syncMapping.ExtraValueCondtion;
                    if (!string.IsNullOrEmpty(whereClause))
                    {
                        whereClause = whereClause.Replace(";", "");
                    }
                    whereClause = whereClause + syncMapping.ExtraWhereCondtion;
                }

                query = query + value + whereClause;
                int rowsAffected = ExecuteQueryInPostgresSQL(syncMapping.DestinationConnection, query, log);
                //MAKE A LOOP OVER DESIGNATION PUT A FLAG TO IT
                string[] comparingColls = { };
                if (!syncMapping.ComparingColumns.IsNullOrEmpty())
                {
                    comparingColls = syncMapping.ComparingColumns.Split(Constant.SeparatorPipe);
                }
                if (comparingColls.Length > 0 && rowsAffected > 0)
                {
                    //var dtCompareResult = destinationDataTable
                    //                    .AsEnumerable().Where(c =>
                    //                    dictConditions.All(kv => (c.Field<string>(kv.Key) == null && kv.Value == string.Empty) ? true : (c.Field<string>(Convert.ToString(kv.Key)) == Convert.ToString(kv.Value))));

                    var dtCompareResult = destinationDataTable
                                .AsEnumerable()
                                .Where(c =>
                                    dictConditions.All(kv =>
                                    {
                                        var columnValue = c.Field<object>(kv.Key);
                                        if (columnValue == null && kv.Value == string.Empty)
                                        {
                                            return true;
                                        }
                                        else if (columnValue is string && kv.Value is string)
                                        {
                                            return ((string)columnValue) == (string)kv.Value;
                                        }
                                        else if (columnValue is long && kv.Value is string)
                                        {
                                            long.TryParse((string)kv.Value, out long conditionValue);
                                            return ((long)columnValue) == conditionValue;
                                        }
                                        else if (columnValue is string && kv.Value is long)
                                        {
                                            long.TryParse((string)columnValue, out long columnValueAsLong);
                                            return Convert.ToString(columnValueAsLong) == Convert.ToString(kv.Value);
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }));

                    foreach (DataRow dtCompareResultRow in dtCompareResult)
                    {
                        List<string> rowEvents = new List<string>();
                        for (int i = 0; i < comparingColls.Length; i++)
                        {
                            string[] comparingColumn = comparingColls[i].Split(Constant.SeparatorColon);
                            string sourceColumn = comparingColumn[0];
                            string destinationColumn = comparingColumn[1];
                            string eventType = comparingColumn[2];
                            string dataType = string.Empty;
                            if (comparingColumn.Length == 3)
                            {
                                dataType = comparingColumn[2];

                            }
                            else if (comparingColumn.Length == 4)
                            {
                                dataType = comparingColumn[3];
                            }
                            string eventName = GetEventBySourceAndDestination(syncMapping.SourceTableName, syncMapping.DestinationTableName, updateRow, dtCompareResultRow, comparingColumn[0], comparingColumn[1], eventType, syncMapping, dataType, log);
                            if (!eventName.IsNullOrEmpty())
                            {
                                if (!eventName.IsNullOrEmpty() && !rowEvents.Contains(eventName))
                                {
                                    rowEvents.Add(eventName);
                                }
                            }
                        }
                        CreateEventAfterSyncResult(rowEvents, syncMapping.SourceTableName, syncMapping.DestinationTableName, updateRow, dtCompareResultRow, eventDataTable, log);
                        DataRow dr = destinationEventDataTable.NewRow();
                        dr.ItemArray = dtCompareResultRow.ItemArray;
                        //add the datatable to workwith

                        //check this column
                        dr["Events"] = string.Join(",", rowEvents);
                        destinationEventDataTable.Rows.Add(dr);
                    }
                }

            }

            log.LogInformation($"--SCHEDULAR--UpdateItemInDestination--End");
        }

        private void CreateEventAfterSyncResult(List<string> events, string sourceTableName, string destinationTableName, DataRow sourceDataRow, DataRow destinationDataRow, DataTable eventDataTable, ILogger log)
        {
            for (int i = 0; i < events.Count; i++)
            {
                string sourceDataRowSerialized = JsonConvert.SerializeObject(sourceDataRow.Table.Columns.Cast<DataColumn>().ToDictionary(c => c.ColumnName, c => sourceDataRow.IsNull(c) ? null : sourceDataRow[c]));
                string destinationDataRowSerialized = destinationDataRow == null ? string.Empty : JsonConvert.SerializeObject(destinationDataRow.Table.Columns.Cast<DataColumn>().ToDictionary(c => c.ColumnName, c => destinationDataRow.IsNull(c) ? null : destinationDataRow[c]));
                DataRow eventDataRow = eventDataTable.NewRow();
                eventDataRow["EventType"] = events[i];
                eventDataRow["SourceDataTableName"] = sourceTableName;
                eventDataRow["DestinationDataTableName"] = destinationTableName;
                eventDataRow["SourceData"] = sourceDataRowSerialized;
                eventDataRow["DestinationData"] = destinationDataRowSerialized;
                // if (DatabaseEventsNames.Any(a => a.ToLower() == events[i]?.ToLower()))
                // {
                //     eventDataRow["EventCategory"] = EventCategoryNames.DATABASE_EVENT;
                // }
                // else
                // {
                //     eventDataRow["EventCategory"] = EventCategoryNames.PUBLISH_EVENT;
                // }
                eventDataTable.Rows.Add(eventDataRow);
            }
        }

        private string GetEventBySourceAndDestination(string sourceTableName, string destinationTableName, DataRow sourceDataRow, DataRow destinationDataRow, string sourceColumnName, string destinationColumnName, string mappingEventName, Mapping syncMapping, string datatype, ILogger log)
        {
            string finalEvent = string.Empty;
            string datatypes = datatype == string.Empty ? "string" : datatype;

            string sCase = $"{sourceTableName}_{destinationTableName}_{mappingEventName.ToUpper().Trim()}";

            switch (sCase)
            {
                //case $"{pipelinesTableName}_{projectsTableName}_{EventConstants.PROJECT_STATUS_CHANGE_EVENT}":
                case $"{TableNameConstants.PIPELINES_TABLE_NAME}_{TableNameConstants.PROJECT_TABLE_NAME}_{EventConstants.PROJECT_STATUS_CHANGE_EVENT}":
                    //TODO: - TAKE PIPELINE EVENTS
                    if (destinationDataRow != null && Convert.ToString(sourceDataRow[sourceColumnName]).ToLower().Trim() != Convert.ToString(destinationDataRow[destinationColumnName]).ToLower().Trim())
                    {
                        finalEvent = EventConstants.PROJECT_STATUS_CHANGE_EVENT;
                    }
                    break;
                case $"{TableNameConstants.PIPELINES_TABLE_NAME}_{TableNameConstants.PROJECT_TABLE_NAME}_{EventConstants.NEW_PIPELINE_INSERTION_EVENT}":
                    if (destinationDataRow == null && Convert.ToString(sourceDataRow[sourceColumnName]) == "APPROVED")
                    {
                        //INSERTION OF A NEW PIPELINE_PROJECT FROM PIPELINE
                        finalEvent = EventConstants.NEW_PIPELINE_INSERTION_EVENT;
                    }
                    break;
                case $"{TableNameConstants.PIPELINES_TABLE_NAME}_{TableNameConstants.PROJECT_TABLE_NAME}_{EventConstants.JOB_INSERT_EVENT}":
                    if (destinationDataRow == null && Convert.ToString(sourceDataRow[sourceColumnName]) == "WON")
                    {
                        //INSERTION OF A JOBCODE FROM PIPELINE 
                        finalEvent = EventConstants.JOB_INSERT_EVENT;
                    }
                    //PIPELINE_CONVERTED_TO_JOB
                    break;
                case $"{TableNameConstants.PIPELINES_TABLE_NAME}_{TableNameConstants.PROJECT_TABLE_NAME}_{EventConstants.PIPELINE_CONVERTED_TO_JOB_EVENT}":
                    if (destinationDataRow != null && string.IsNullOrEmpty(Convert.ToString(destinationDataRow[destinationColumnName])) && !string.IsNullOrEmpty(Convert.ToString(sourceDataRow[sourceColumnName])))
                    {
                        finalEvent = EventConstants.PIPELINE_CONVERTED_TO_JOB_EVENT;
                    }
                    break;
                case $"{TableNameConstants.PIPELINES_TABLE_NAME}_{TableNameConstants.PROJECT_TABLE_NAME}_{EventConstants.ROLL_FORWARD_EVENT}":
                    //todo
                    //null check over here
                    if (sourceDataRow != null && destinationColumnName != null && Convert.ToString(sourceDataRow[TableColumnNames.JOB_CODE]).Equals(Convert.ToString(destinationDataRow[TableColumnNames.PROJECT_JOB_CODE]), StringComparison.OrdinalIgnoreCase))
                    {
                        DateTime dt;
                        //if jobcode is same as before
                        //case-1 : jobcode is null -> pipelineStartDateComparision startDate of pipeline and startDate of project
                        //case-2 : jobcode is not null -> jobStartDateComparision startDate of pipeline_job and startdate of project
                        if ((sourceDataRow.IsNull(TableColumnNames.JOB_CODE)
                            || string.IsNullOrEmpty(Convert.ToString(sourceDataRow[TableColumnNames.JOB_CODE])))
                            && DateTime.TryParse(Convert.ToString(sourceDataRow[TableColumnNames.START_DATE]), out dt)
                            && DateTime.TryParse(Convert.ToString(destinationDataRow[TableColumnNames.PROJECT_START_DATE]), out dt)
                            && Convert.ToDateTime(sourceDataRow[TableColumnNames.START_DATE]).ToUniversalTime().Date > Convert.ToDateTime(destinationDataRow[TableColumnNames.PROJECT_START_DATE]).ToUniversalTime().Date)
                        {
                            finalEvent = EventConstants.ROLL_FORWARD_EVENT;
                        }
                        else if (!(string.IsNullOrEmpty(Convert.ToString(sourceDataRow[TableColumnNames.JOB_CODE])))
                            && DateTime.TryParse(Convert.ToString(sourceDataRow[TableColumnNames.PIPELINE_JOB_START_DATE]), out dt)
                            && DateTime.TryParse(Convert.ToString(destinationDataRow[TableColumnNames.PROJECT_START_DATE]), out dt)
                            && Convert.ToDateTime(sourceDataRow[TableColumnNames.PIPELINE_JOB_START_DATE]).ToUniversalTime().Date > Convert.ToDateTime(destinationDataRow[TableColumnNames.PROJECT_START_DATE]).ToUniversalTime().Date)
                        {
                            finalEvent = EventConstants.ROLL_FORWARD_EVENT;
                        }
                    }
                    break;
                case $"{TableNameConstants.JOBS_TABLE_NAME}_{TableNameConstants.PROJECT_TABLE_NAME}_{EventConstants.ROLL_FORWARD_EVENT}":
                    if (sourceDataRow != null && destinationDataRow != null && Convert.ToString(sourceDataRow[TableColumnNames.JOB_CODE]).Equals(Convert.ToString(destinationDataRow[TableColumnNames.PROJECT_JOB_CODE]), StringComparison.OrdinalIgnoreCase))
                    {
                        DateTime dt;
                        if (DateTime.TryParse(Convert.ToString(sourceDataRow[TableColumnNames.START_DATE]), out dt)
                            && DateTime.TryParse(Convert.ToString(destinationDataRow[TableColumnNames.PROJECT_START_DATE]), out dt)
                            && Convert.ToDateTime(sourceDataRow[TableColumnNames.START_DATE]).ToUniversalTime().Date > Convert.ToDateTime(destinationDataRow[TableColumnNames.PROJECT_START_DATE]).ToUniversalTime().Date)
                        {
                            finalEvent = EventConstants.ROLL_FORWARD_EVENT;
                        }
                    }
                    break;
                case $"{TableNameConstants.EMPLOYEES_TABLE_NAME}_{TableNameConstants.USERS_TABLE_NAME}_{EventConstants.DESIGNATION_CHANGE_EVENT}":
                    //CONDITION : - CHANGE IN DESIGNATION NAME && BOTH SOURCE AND DESTINATION MUST BE ACTIVE
                    if (destinationDataRow != null && Convert.ToBoolean(sourceDataRow[TableColumnNames.IS_ACTIVE]) == true && Convert.ToBoolean(destinationDataRow[TableColumnNames.IS_ACTIVE_USER_TABLE]) == true && Convert.ToString(sourceDataRow[sourceColumnName]) != Convert.ToString(destinationDataRow[destinationColumnName]))
                    {
                        finalEvent = EventConstants.DESIGNATION_CHANGE_EVENT;
                    }
                    break;
                case $"{TableNameConstants.EMPLOYEES_TABLE_NAME}_{TableNameConstants.USERS_TABLE_NAME}_{EventConstants.EMPLOYEE_STATUS_CHANGE_EVENT}":
                    //CONDITION : - CHANGE IN SPERCOACH_MID NAME && BOTH SOURCE AND DESTINATION MUST BE ACTIVE
                    if (destinationDataRow != null && Convert.ToBoolean(sourceDataRow[TableColumnNames.IS_ACTIVE]) == true && Convert.ToBoolean(destinationDataRow[TableColumnNames.IS_ACTIVE_USER_TABLE]) == true && Convert.ToString(sourceDataRow[sourceColumnName]) != Convert.ToString(destinationDataRow[destinationColumnName]))
                    {
                        finalEvent = EventConstants.EMPLOYEE_STATUS_CHANGE_EVENT;
                    }
                    break;
                case $"{TableNameConstants.EMPLOYEES_TABLE_NAME}_{TableNameConstants.USERS_TABLE_NAME}_{EventConstants.EMPLOYEE_CO_SUPERCOACH_CHANGE}":
                    //CONDITION : - CHANGE IN SPERCOACH_MID NAME && BOTH SOURCE AND DESTINATION MUST BE ACTIVE
                    if (destinationDataRow != null)
                    {
                        finalEvent = EventConstants.EMPLOYEE_CO_SUPERCOACH_CHANGE;
                    }
                    break;
                case $"{TableNameConstants.EMPLOYEES_TABLE_NAME}_{TableNameConstants.USERS_TABLE_NAME}_{EventConstants.EMPLOYEE_SUPERCOACH_CHANGE}":
                    //CONDITION : - CHANGE IN DESIGNATION NAME && BOTH SOURCE AND DESTINATION MUST BE ACTIVE
                    if (destinationDataRow != null)
                    {
                        finalEvent = EventConstants.EMPLOYEE_SUPERCOACH_CHANGE;
                    }
                    break;
                case $"{TableNameConstants.JOB_ROLES_TABLE_NAME}_{TableNameConstants.PROJECT_ROLES_TABLE_NAME}_{EventConstants.PROJECT_ROLE_UPDATE}":
                    finalEvent = EventConstants.PROJECT_ROLE_UPDATE;
                    break;
                case $"{TableNameConstants.PIPELINE_ROLES_TABLE_NAME}_{TableNameConstants.PROJECT_ROLES_TABLE_NAME}_{EventConstants.PROJECT_PIPELINE_ROLE_UPDATE}":
                    finalEvent = EventConstants.PROJECT_PIPELINE_ROLE_UPDATE;
                    break;
                case $"{TableNameConstants.JOBS_TABLE_NAME}_{TableNameConstants.PROJECT_TABLE_NAME}_{EventConstants.PROJECT_ACTIVATION_STATUS_EVENT}":
                    //jobs -> project -> if Previous status True shanged the status to False
                    bool jobActivationStatus = sourceDataRow.IsNull(TableColumnNames.IS_ACTIVE) ? false : Convert.ToBoolean(sourceDataRow[TableColumnNames.IS_ACTIVE]);
                    bool projectActivationStatus = destinationDataRow.IsNull(TableColumnNames.ProjectActivationStatus) ? false : Convert.ToBoolean(destinationDataRow[TableColumnNames.ProjectActivationStatus]);
                    if ((jobActivationStatus != projectActivationStatus) && (jobActivationStatus == false && projectActivationStatus == true))
                    {
                        finalEvent = EventConstants.PROJECT_ACTIVATION_STATUS_EVENT;
                    }
                    break;
                case $"{TableNameConstants.JOBS_TABLE_NAME}_{TableNameConstants.PROJECT_TABLE_NAME}_{EventConstants.PROJECT_CLOSURE_STATUS_EVENT}":
                    //jobs -> project -> if Previous status True shanged the status to False
                    //condition change 
                    bool jobClosureStatus = sourceDataRow.IsNull(TableColumnNames.closed_job) ? true : Convert.ToBoolean(sourceDataRow[TableColumnNames.closed_job]);
                    bool projectClosureStatus = destinationDataRow.IsNull(TableColumnNames.ProjectClosureState) ? true : Convert.ToBoolean(destinationDataRow[TableColumnNames.ProjectClosureState]);
                    if ((jobClosureStatus != projectClosureStatus) && (jobClosureStatus == true && projectClosureStatus == false))
                    {
                        finalEvent = EventConstants.PROJECT_CLOSURE_STATUS_EVENT;
                    }
                    break;
                default:
                    break;
            }
            return finalEvent;
        }

        private DataTable PostgresQueryFormation(string sqlString, DataRow dtInsertItemRow, string connectionString, ILogger log)
        {
            sqlString = Helper.Helper.QueryStringBuilderByRegex(sqlString, dtInsertItemRow);
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                DataTable table = new DataTable();
                try
                {
                    DataTable dt = new DataTable();
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    NpgsqlCommand command = new NpgsqlCommand(sqlString, connection);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    adapter.Fill(dt);

                    if (!(dt != null && dt.Rows.Count > 0))
                    {
                        log.LogInformation("Exception - No rows found. SQL-{0}, dtInsertItemRow-{1}", sqlString, dtInsertItemRow);
                    }

                    return dt;
                }
                catch (Exception ex)
                {
                    log.LogInformation($"--Exception--PostgresQueryFormation--{ex.Message}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void InsertItemInDestination(DataTable dtInsertItems, Mapping syncMapping, DataTable eventDataTable, ILogger log)
        {
            bool isInsertCommandCreated = false;
            int batchCount = 1;
            string query = $"INSERT INTO \"{syncMapping.DestinationTableName}\" (";
            string value = $" VALUES (";
            foreach (DataRow insertRow in dtInsertItems.Rows)
            {
                string[] columnColl = syncMapping.ColumnMapping.Split(Constant.SeparatorPipe);
                for (int i = 0; i < columnColl.Length; i++)
                {
                    string[] columns = columnColl[i].Split(Constant.SeparatorColon);
                    string[] columnsInfo = columns[1].Split(Constant.DoubleAtTheRate);
                    string columnName = columnsInfo[0];
                    if (!isInsertCommandCreated)
                    {
                        query = query + '"' + columnName + '"' + ",";
                    }

                    if (columns.Length == 3)
                    {
                        string destinationColumnValue = GetColumnValueByParams(syncMapping.SourceTableName, columns[0], syncMapping.DestinationTableName, columnName, Constant.INSERT_OPRATION, syncMapping, insertRow, log);
                        if (!destinationColumnValue.IsNullOrEmpty())
                        {
                            if (destinationColumnValue == "null")
                            {
                                value = value + " null , ";
                            }
                            else
                            {
                                value = value + '\'' + Helper.Helper.ReplaceInvalidChar(destinationColumnValue) + '\'' + " , ";
                            }
                        }
                        else if (insertRow.IsNull(columns[0]))
                        {
                            value = value + " null , ";
                        }
                        else
                        {
                            switch (columns[2])
                            {
                                case "bool":
                                    value = value + Convert.ToBoolean(insertRow[columns[0]]) + ",";
                                    break;
                                case Constant.DATETIME:
                                    if (insertRow[columns[0]].ToString().IsNullOrEmpty())
                                    {
                                        value = value + " null ,";
                                    }
                                    else
                                    {
                                        value = value + '\'' + Convert.ToDateTime(insertRow[columns[0]]) + '\'' + ",";
                                    }
                                    break;
                                default:
                                    value = value + '\'' + Convert.ToString(Helper.Helper.ReplaceInvalidChar(insertRow[columns[0]])) + '\'' + ",";
                                    break;
                            }
                        }
                    }
                    else if (columns.Length > 3)
                    {
                        if (columns[3] == Constant.SQL)
                        {
                            DataTable datatable = PostgresQueryFormation(columns[0], insertRow, syncMapping.DestinationConnection, log);
                            if (datatable.Rows != null && datatable.Rows.Count <= 0)
                            {
                                value = value + " null " + ",";
                            }
                            else
                            {
                                switch (columns[2])
                                {
                                    case Constant.INTEGER:
                                        value = value + datatable.Rows[0][columnName] + ",";
                                        break;
                                    default:
                                        value = value + '\'' + Convert.ToString(Helper.Helper.ReplaceInvalidChar(datatable.Rows[0][columnName])) + '\'' + ",";
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        string destinationColumnValue = GetColumnValueByParams(syncMapping.SourceTableName, columns[0], syncMapping.DestinationTableName, columnName, Constant.INSERT_OPRATION, syncMapping, insertRow, log);
                        if (!destinationColumnValue.IsNullOrEmpty())
                        {
                            if (destinationColumnValue == "null")
                            {
                                value = value + " null , ";
                            }
                            else
                            {
                                value = value + '\'' + Helper.Helper.ReplaceInvalidChar(destinationColumnValue) + '\'' + ',';
                            }
                        }
                        else if (insertRow.IsNull(columns[0]))
                        {
                            value = value + " null , ";

                        }
                        else
                        {
                            value = value + '\'' + Convert.ToString(Helper.Helper.ReplaceInvalidChar(insertRow[columns[0]])) + '\'' + ',';
                        }
                    }
                }

                if (!isInsertCommandCreated)
                {
                    int queryCommaLastIndex = query.LastIndexOf(',');
                    query = query.Substring(0, queryCommaLastIndex);
                    query = query + ')';
                }
                int valueCommaLastIndex = value.LastIndexOf(',');
                value = value.Substring(0, valueCommaLastIndex);
                value = value + ")";
                query = query + value;
                value = " , (";
                isInsertCommandCreated = true;

            }

            int RowsAffected = ExecuteQueryInPostgresSQL(syncMapping.DestinationConnection, query, log);
            if (!syncMapping.EventsOnInsert.IsNullOrEmpty() && RowsAffected > 0)
            {
                foreach (DataRow insertRow in dtInsertItems.Rows)
                {
                    string[] events = syncMapping.EventsOnInsert.Split(Constant.SeparatorPipe);
                    List<string> rowEvents = new List<string>();
                    for (int i = 0; i < events.Length; i++)
                    {
                        string[] comparingColumn = events[i].Split(Constant.SeparatorColon);
                        string sourceColumn = comparingColumn[0];
                        string eventType = comparingColumn[1];
                        string dataType = string.Empty;
                        if (comparingColumn.Length > 2)
                        {
                            dataType = comparingColumn[2];
                        }
                        string eventName = GetEventBySourceAndDestination(syncMapping.SourceTableName, syncMapping.DestinationTableName, insertRow, null, sourceColumn, string.Empty, eventType, syncMapping, dataType, log);
                        if (!eventName.IsNullOrEmpty() && !rowEvents.Contains(eventName))
                        {
                            rowEvents.Add(eventName);
                        }
                    }
                    CreateEventAfterSyncResult(events.ToList<string>(), syncMapping.SourceTableName, syncMapping.DestinationTableName, insertRow, null, eventDataTable, log);
                }
            }
        }

        private string GetColumnValueByParams(string sourceTableName, string sourceColumnName, string destinationTableName, string destinationColumnName, string oprationType, Mapping syncMapping, DataRow sourceDataRow, ILogger log)
        {
            string columnValue = string.Empty;
            if (oprationType == Constant.INSERT_OPRATION && (sourceColumnName.ToUpper().Trim() == TableColumnNames.CREATED_BY.ToUpper().Trim() || sourceColumnName.ToUpper().Trim() == TableColumnNames.CREATED_BY_USERS.ToUpper().Trim()))
            {
                columnValue = Constant.GT_HANDLER_ID;
            }
            else if (oprationType == Constant.INSERT_OPRATION && (sourceColumnName.ToUpper().Trim() == TableColumnNames.CREATED_AT.ToUpper().Trim() || sourceColumnName.ToUpper().Trim() == TableColumnNames.CREATED_AT_USERS.ToUpper().Trim()))
            {
                DateTime currentDate = DateTime.Now;
                DateTime minDateTime = Convert.ToDateTime(currentDate);
                columnValue = Convert.ToString(minDateTime);
            }
            else if (oprationType == Constant.INSERT_OPRATION && (sourceColumnName.ToUpper().Trim() == TableColumnNames.MODIFIED_AT.ToUpper().Trim() || sourceColumnName.ToUpper().Trim() == TableColumnNames.MODIFIED_AT_USER.ToUpper().Trim()))
            {
                DateTime currentDate = DateTime.Now;
                DateTime minDateTime = Convert.ToDateTime(currentDate);
                columnValue = Convert.ToString(minDateTime);
                //columnValue = "null";
            }
            else if (oprationType == Constant.INSERT_OPRATION && (sourceColumnName.ToUpper().Trim() == TableColumnNames.MODIFIED_BY.ToUpper().Trim() || sourceColumnName.ToUpper().Trim() == TableColumnNames.MODIFIED_BY_USER.ToUpper().Trim()))
            {
                columnValue = Constant.GT_HANDLER_ID;
            }
            else if (oprationType == Constant.UPDATE_OPRATION && (sourceColumnName.ToUpper().Trim() == TableColumnNames.MODIFIED_BY.ToUpper().Trim() || sourceColumnName.ToUpper().Trim() == TableColumnNames.MODIFIED_BY_USER.ToUpper().Trim()))
            {
                columnValue = Constant.GT_HANDLER_ID;
            }
            else if (oprationType == Constant.UPDATE_OPRATION && (sourceColumnName.ToUpper().Trim() == TableColumnNames.MODIFIED_AT.ToUpper().Trim() || sourceColumnName.ToUpper().Trim() == TableColumnNames.MODIFIED_AT_USER.ToUpper().Trim()))
            {
                DateTime currentDate = DateTime.Now;
                //DateTime minDateTime = Convert.ToDateTime(currentDate.Date);
                DateTime minDateTime = Convert.ToDateTime(currentDate);
                columnValue = Convert.ToString(minDateTime);
            }
            else if (oprationType == Constant.INSERT_OPRATION && sourceTableName.ToUpper().Trim() == TableNameConstants.ROLE_TABLE_NAME.ToUpper().Trim() && destinationTableName.ToUpper().Trim() == TableNameConstants.USER_ROLE_TABLE_NAME.ToUpper().Trim() && destinationColumnName.ToUpper().Trim() == TableColumnNames.ID.ToUpper().Trim())
            {
                Guid newGuid = Guid.NewGuid();
                columnValue = Convert.ToString(newGuid);
            }
            else if (oprationType == Constant.INSERT_OPRATION && sourceTableName.ToUpper().Trim() == TableNameConstants.USERS_TABLE_NAME.ToUpper().Trim() && destinationTableName.ToUpper().Trim() == TableNameConstants.USER_ROLE_TABLE_NAME.ToUpper().Trim() && destinationColumnName.ToUpper().Trim() == TableColumnNames.ID.ToUpper().Trim())
            {
                Guid newGuid = Guid.NewGuid();
                columnValue = Convert.ToString(newGuid);
            }
            else if (oprationType == Constant.INSERT_OPRATION && sourceTableName.ToUpper().Trim() == TableNameConstants.BUTREEMAPPING_TABLE_NAME.ToUpper().Trim() && destinationTableName.ToUpper().Trim() == TableNameConstants.USER_ROLE_TABLE_NAME.ToUpper().Trim() && destinationColumnName.ToUpper().Trim() == TableColumnNames.ID.ToUpper().Trim())
            {
                Guid newGuid = Guid.NewGuid();
                columnValue = Convert.ToString(newGuid);
            }
            else if ((oprationType == Constant.INSERT_OPRATION || oprationType == Constant.UPDATE_OPRATION) && sourceTableName.ToUpper().Trim() == TableNameConstants.PIPELINES_TABLE_NAME.ToUpper().Trim() && destinationTableName.ToUpper().Trim() == TableNameConstants.PROJECT_TABLE_NAME.ToUpper().Trim() && destinationColumnName.ToUpper().Trim() == TableColumnNames.CHARGABLE_TYPE.ToUpper().Trim())
            {
                columnValue = Constant.CHARGABLE;
            }
            else if ((oprationType == Constant.INSERT_OPRATION || oprationType == Constant.UPDATE_OPRATION) && sourceTableName.ToUpper().Trim() == TableNameConstants.JOBS_TABLE_NAME.ToUpper().Trim() && destinationTableName.ToUpper().Trim() == TableNameConstants.PROJECT_TABLE_NAME.ToUpper().Trim() && destinationColumnName.ToUpper().Trim() == TableColumnNames.CHARGABLE_TYPE.ToUpper().Trim())
            {
                if (sourceDataRow.IsNull(sourceColumnName))
                {
                    columnValue = Constant.NONCHARGABLE;
                }
                else if (Convert.ToBoolean(sourceDataRow[sourceColumnName]) == true)
                {
                    columnValue = Constant.CHARGABLE;
                }
                else
                {
                    columnValue = Constant.NONCHARGABLE;
                }
            }
            else if ((oprationType == Constant.INSERT_OPRATION || oprationType == Constant.UPDATE_OPRATION) && sourceTableName.ToUpper().Trim() == TableNameConstants.PIPELINES_TABLE_NAME.ToUpper().Trim() && destinationTableName.ToUpper().Trim() == TableNameConstants.PROJECT_TABLE_NAME.ToUpper().Trim() && destinationColumnName.ToUpper().Trim() == TableColumnNames.PROJECT_TYPE.ToUpper().Trim() && sourceColumnName.ToUpper().Trim() == TableColumnNames.IS_RECURRING.ToUpper().Trim())
            {
                if (sourceDataRow.IsNull(sourceColumnName))
                {
                    columnValue = Constant.NONRECURRING;
                }
                else if (Convert.ToString(sourceDataRow[sourceColumnName]).Equals("0", StringComparison.OrdinalIgnoreCase))
                {
                    columnValue = Constant.NONRECURRING;
                }
                else if (Convert.ToString(sourceDataRow[sourceColumnName]).Equals("1", StringComparison.OrdinalIgnoreCase))
                {
                    columnValue = Constant.RECURRING;
                }
            }
            else if ((oprationType == Constant.INSERT_OPRATION || oprationType == Constant.UPDATE_OPRATION)
                && sourceTableName.ToUpper().Trim() == TableNameConstants.JOBS_TABLE_NAME.ToUpper().Trim()
                && destinationTableName.ToUpper().Trim() == TableNameConstants.PROJECT_TABLE_NAME.ToUpper().Trim()
                && sourceColumnName.ToUpper().Trim() == TableColumnNames.RECURRING_TYPE.ToUpper().Trim()
                && destinationColumnName.ToUpper().Trim() == TableColumnNames.PROJECT_TYPE.ToUpper().Trim())
            {
                if (sourceDataRow.IsNull(sourceColumnName))
                {
                    columnValue = Constant.NONRECURRING;
                }
                else if (Convert.ToString(sourceDataRow[sourceColumnName]).Equals("R", StringComparison.OrdinalIgnoreCase))
                {
                    columnValue = Constant.RECURRING;
                }
                else if (Convert.ToString(sourceDataRow[sourceColumnName]).Equals("N", StringComparison.OrdinalIgnoreCase))
                {
                    columnValue = Constant.NONRECURRING;
                }
            }
            else if (oprationType.Equals(Constant.UPDATE_OPRATION, StringComparison.OrdinalIgnoreCase) 
                    && sourceTableName.Equals(TableNameConstants.PIPELINES_TABLE_NAME, StringComparison.OrdinalIgnoreCase) 
                    && destinationTableName.Equals(TableNameConstants.PROJECT_TABLE_NAME, StringComparison.OrdinalIgnoreCase) 
                    && sourceColumnName.Equals(TableColumnNames.START_DATE, StringComparison.OrdinalIgnoreCase) 
                    && destinationColumnName.Equals(TableColumnNames.PROJECT_START_DATE, StringComparison.OrdinalIgnoreCase))
            {
                if ((!(sourceDataRow.IsNull(TableColumnNames.JOB_CODE)) || sourceDataRow[TableColumnNames.JOB_CODE] != ""))
                {
                    columnValue = Convert.ToString(sourceDataRow[TableColumnNames.PIPELINE_JOB_START_DATE]);
                }
            }
            else if (oprationType.Equals(Constant.INSERT_OPRATION, StringComparison.OrdinalIgnoreCase) &&
                sourceTableName.Equals(TableNameConstants.PIPELINES_TABLE_NAME, StringComparison.OrdinalIgnoreCase) &&
                destinationTableName.Equals(TableNameConstants.PROJECT_TABLE_NAME, StringComparison.OrdinalIgnoreCase) &&
                sourceColumnName.Equals(TableColumnNames.START_DATE, StringComparison.OrdinalIgnoreCase) &&
                destinationColumnName.Equals(TableColumnNames.PROJECT_START_DATE, StringComparison.OrdinalIgnoreCase))
            {
                if ((!(sourceDataRow.IsNull(TableColumnNames.JOB_CODE)) || sourceDataRow[TableColumnNames.JOB_CODE] != ""))
                {
                    columnValue = GetValueAsString(sourceDataRow[TableColumnNames.PIPELINE_JOB_START_DATE]);
                }
            }
            else if (oprationType.Equals(Constant.INSERT_OPRATION, StringComparison.OrdinalIgnoreCase)
                && sourceTableName.Equals(TableNameConstants.PIPELINES_TABLE_NAME, StringComparison.OrdinalIgnoreCase)
                && destinationTableName.Equals(TableNameConstants.PROJECT_TABLE_NAME, StringComparison.OrdinalIgnoreCase)
                && sourceColumnName.Equals(TableColumnNames.END_DATE, StringComparison.OrdinalIgnoreCase)
                && destinationColumnName.Equals(TableColumnNames.PROJECT_END_DATE, StringComparison.OrdinalIgnoreCase))
            {
                if ((!(sourceDataRow.IsNull(TableColumnNames.JOB_CODE)) || sourceDataRow[TableColumnNames.JOB_CODE] != ""))
                {
                    columnValue = GetValueAsString(sourceDataRow[TableColumnNames.PIPELINE_JOB_END_DATE]);
                }
            }
            else if (oprationType.Equals(Constant.UPDATE_OPRATION, StringComparison.OrdinalIgnoreCase) && sourceTableName.Equals(TableNameConstants.PIPELINES_TABLE_NAME, StringComparison.OrdinalIgnoreCase)
                && destinationTableName.Equals(TableNameConstants.PROJECT_TABLE_NAME, StringComparison.OrdinalIgnoreCase)
                && sourceColumnName.Equals(TableColumnNames.END_DATE, StringComparison.OrdinalIgnoreCase)
                && destinationColumnName.Equals(TableColumnNames.PROJECT_END_DATE, StringComparison.OrdinalIgnoreCase))
            {
                if (doDetailedLogging) log.LogInformation($"--SCHEDULAR--TimestampCheck --{Convert.ToString(sourceDataRow[TableColumnNames.PIPELINE_JOB_END_DATE])}-----{Convert.ToString(sourceDataRow[TableColumnNames.END_DATE])}");

                if (!(sourceDataRow.IsNull(TableColumnNames.JOB_CODE)) && (sourceDataRow[TableColumnNames.JOB_CODE] != ""))
                {
                    bool isNullOrEmpty = string.IsNullOrEmpty(Convert.ToString(sourceDataRow[TableColumnNames.PIPELINE_JOB_END_DATE]));
                    columnValue = $"{TypeConstant.SQL_QUERY} CASE WHEN \"{destinationColumnName}\" IS NULL AND \"{TableColumnNames.PROJECT_JOB_CODE}\" IS NULL THEN " + (isNullOrEmpty ? " null " : $" '{Convert.ToString(sourceDataRow[TableColumnNames.PIPELINE_JOB_END_DATE])}' ") +
                                  $" WHEN \"{destinationColumnName}\" IS NOT NULL AND \"{TableColumnNames.PROJECT_JOB_CODE}\" IS NULL THEN " + (isNullOrEmpty ? " null " : $" '{Convert.ToString(sourceDataRow[TableColumnNames.PIPELINE_JOB_END_DATE])}' ") +
                                  $" WHEN \"{destinationColumnName}\" IS NULL AND \"{TableColumnNames.PROJECT_JOB_CODE}\" IS NOT NULL THEN " + (isNullOrEmpty ? " null " : $" '{Convert.ToString(sourceDataRow[TableColumnNames.PIPELINE_JOB_END_DATE])}' ") +
                                  $" ELSE \"{destinationColumnName}\" END ";
                }
                else if ((sourceDataRow.IsNull(TableColumnNames.JOB_CODE) || sourceDataRow[TableColumnNames.JOB_CODE] == ""))
                {
                    bool isNullOrEmpty = string.IsNullOrEmpty(Convert.ToString(sourceDataRow[TableColumnNames.END_DATE]));
                    columnValue = $"{TypeConstant.SQL_QUERY} CASE WHEN \"{destinationColumnName}\" IS NULL THEN " + (isNullOrEmpty ? " null " : $" '{Convert.ToString(sourceDataRow[TableColumnNames.END_DATE])}' ") +
                                  $" ELSE \"{destinationColumnName}\" END";
                }
                else
                {
                    columnValue = $"{TypeConstant.SQL_QUERY} CASE WHEN \"{destinationColumnName}\" IS NULL THEN null ELSE \"{destinationColumnName}\" END";
                }
            }
            else if (oprationType.Equals(Constant.UPDATE_OPRATION, StringComparison.OrdinalIgnoreCase) && sourceTableName.Equals(TableNameConstants.JOBS_TABLE_NAME, StringComparison.OrdinalIgnoreCase) && destinationTableName.Equals(TableNameConstants.PROJECT_TABLE_NAME, StringComparison.OrdinalIgnoreCase) && sourceColumnName.Equals(TableColumnNames.UPDATED_END_DATE, StringComparison.OrdinalIgnoreCase) && destinationColumnName.Equals(TableColumnNames.PROJECT_END_DATE, StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrEmpty(Convert.ToString(sourceDataRow[TableColumnNames.UPDATED_END_DATE])))
                {
                    //checked
                    columnValue = $"{TypeConstant.SQL_QUERY} CASE WHEN \"{destinationColumnName}\" IS NULL THEN '{Convert.ToString(sourceDataRow[TableColumnNames.UPDATED_END_DATE])}' ELSE \"{destinationColumnName}\" END";
                }
                else
                {
                    //checked
                    columnValue = $"{TypeConstant.SQL_QUERY} CASE WHEN \"{destinationColumnName}\" IS NULL THEN null ELSE \"{destinationColumnName}\" END";
                }
            }



            return columnValue;
        }

        private static string GetValueAsString(object dateValue)
        {
            string columnValue;
            DateTime date;
            bool isValid = DateTime.TryParse(dateValue.ToString(), out date);
            if (isValid && dateValue != DBNull.Value && date != DateTime.MinValue)
            {
                //Console.WriteLine("The DateTime is valid: " + endDate);
                columnValue = Convert.ToString(dateValue);
            }
            else
            {
                //Console.WriteLine("The DateTime is not valid or is empty.");
                columnValue = "null";
            }

            return columnValue;
        }

        private int ExecuteQueryInPostgresSQL(string destinationConnectionString, string query, ILogger log)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(destinationConnectionString))
            {
                try
                {
                    DataTable dt = new DataTable();
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    if (doDetailedLogging) log.LogInformation($"--SCHEDULAR--ExecuteQueryInPostgresSQL--{query}");

                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
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

        private DataTable GetSourceTableItems(Mapping mapping, string syncStartTime, ILogger log)
        {
            try
            {
                DataTable tableItems = new DataTable();
                bool isMoreRecords = true;
                int iteration = 0;
                using (NpgsqlConnection connection = new NpgsqlConnection(mapping.SourceConnection))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    DataTable dt = new DataTable();
                    string sqlString = string.IsNullOrEmpty(mapping.SourceSelectQuery) ? "SELECT * FROM " + mapping.SourceTableName +
                                        " ;" : mapping.SourceSelectQuery;
                    if (!syncStartTime.IsNullOrEmpty() && !mapping.SourceFilterQuery.IsNullOrEmpty())
                    {
                        string srcFilterQuery = mapping.SourceFilterQuery.Replace("{{syncStartTime}}", syncStartTime);
                        if (sqlString.Contains("WHERE", StringComparison.OrdinalIgnoreCase))
                        {
                            sqlString = sqlString + " AND " + srcFilterQuery;
                        }
                        else
                        {
                            sqlString = sqlString + " WHERE " + srcFilterQuery;
                        }
                    }
                    NpgsqlCommand command = new NpgsqlCommand(sqlString, connection);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    adapter.Fill(dt);
                    tableItems.Merge(dt);
                }
                return tableItems;
            }
            catch (Exception ex)
            {
                log.LogInformation($"--Exception--GetSourceTableItems--{ex.Message}");
                log.LogError(ex, ex.Message);
                throw;
            }
        }

        private DataTable GetDestinationTableItems(Mapping mapping, DataTable sourceTableItems, ILogger log)
        {
            try
            {
                DataTable tableItems = new DataTable();
                bool isMoreRecords = true;
                int iteration = 0;

                using (NpgsqlConnection connection = new NpgsqlConnection(mapping.DestinationConnection))
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    DataTable dt = new DataTable();
                    string sqlString = string.IsNullOrEmpty(mapping.DestinationSelectQuery) ? "SELECT * FROM " + mapping.DestinationTableName +
                                        " ;" : mapping.DestinationSelectQuery;

                    string whereClause = " WHERE ";
                    bool isWhereConditionApplied = false;
                    if (mapping.DestinationSelectQuery.Contains("WHERE", StringComparison.OrdinalIgnoreCase))
                    {
                        isWhereConditionApplied = true;
                        string value = mapping.DestinationSelectQuery.Split("WHERE")[1];
                        whereClause = whereClause + value;
                        sqlString = mapping.DestinationSelectQuery.Split("WHERE")[0];

                        string pattern = @"\{\{(.+?)\}\}";

                        MatchCollection matches = Regex.Matches(whereClause, pattern);
                        List<string> dataList = new List<string>();
                        foreach (Match match in matches)
                        {
                            string data = match.Groups[1].Value;
                            if (!dataList.Contains(data))
                            {
                                dataList.Add(data);
                            }
                        }
                        foreach (string data in dataList)
                        {
                            var distinctDataValuesList = sourceTableItems
                                                        .AsEnumerable()
                                                        .Where(row => row.IsNull(data) == false)
                                                        .Select(row => row.Field<string>(data))
                                                        .Distinct();
                            string distinctDataValues = string.Join(", ", distinctDataValuesList.Select(value => $"'{value}'"));
                            string replaceString = "{{" + data + "}}";
                            string replaceValue = distinctDataValues.IsNullOrEmpty() ? "null" : distinctDataValues;
                            whereClause = whereClause.Replace(replaceString, replaceValue);
                        }

                    }

                    if (isWhereConditionApplied == true)
                    {
                        sqlString = sqlString + whereClause;
                    }
                    if (sqlString.EndsWith("AND ", StringComparison.OrdinalIgnoreCase))
                    {
                        sqlString = sqlString.Remove(sqlString.Length - 4, 4);
                    }
                    sqlString = sqlString.Trim();
                    NpgsqlCommand command = new NpgsqlCommand(sqlString, connection);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    adapter.Fill(dt);
                    tableItems.Merge(dt);
                }
                return tableItems;

            }
            catch (Exception ex)
            {
                log.LogInformation($"--Exception--GetDestinationTableItems--{ex.Message}");
                log.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task publishNotification(List<SyncEventPayload> payload, ILogger log)
        {
            log.LogInformation("--SCHEDULAR--publishNotification-STARTED ,type-" + Constant.EventBusIdentificationType);
            string type = Constant.EventBusIdentificationType;
            foreach (var item in payload)
            {
                await _azureServiceBusService.PublishMessageOnAzureServiceBus(item, type, log);
            }
            log.LogInformation("--SCHEDULAR--publishNotification-End ");
        }

        public async Task<List<SyncEventPayload>> CreateAndPublishSyncEvent(DataTable eventsDataTable, ILogger log)
        {
            List<SyncEventPayload> syncEventPayloads = new List<SyncEventPayload>();

            var environmentVariables = Environment.GetEnvironmentVariables();
            bool disablePublishSyncEvent = Convert.ToBoolean(environmentVariables[Constant.EnvAppSettingConstants.DISABLE_PUBLISHSYNCEVENT]);

            if (!disablePublishSyncEvent)
            {
                string token = await _tokenService.GetToken();
                if (eventsDataTable != null && eventsDataTable.Rows.Count > 0)
                {
                    log.LogInformation("--SCHEDULAR--CreateAndPublishSyncEvent-RowCount " + eventsDataTable.Rows.Count);

                }
                else
                {
                    log.LogInformation("--SCHEDULAR--CreateAndPublishSyncEvent-RowCount 0 or null");

                }
                foreach (DataRow row in eventsDataTable.Rows)
                {
                    log.LogInformation($"--SCHEDULAR--CreateAndPublishSyncEvent-EventDate EventType:{row["EventType"]}-SourceDataTableName:{row["SourceDataTableName"]}-DestinationDataTableName:{row["DestinationDataTableName"]}");
                    log.LogInformation($"--SCHEDULAR--CreateAndPublishSyncEvent-EventDate EventType:{row["EventType"]}-SourceData:{row["SourceData"]}-DestinationData:{row["DestinationData"]}");

                    syncEventPayloads.Add(new SyncEventPayload
                    {
                        token = token,
                        action = Convert.ToString(row["EventType"]),
                        source_table_name = Convert.ToString(row["SourceDataTableName"]),
                        source_table_row = Convert.ToString(row["SourceData"]),
                        destination_table_name = Convert.ToString(row["DestinationDataTableName"]),
                        destination_table_row = Convert.ToString(row["DestinationData"])

                    });
                }
                if (syncEventPayloads.Count == 0)
                {
                    return null;
                }
                await publishNotification(syncEventPayloads, log);
            }
            else
            {
                log.LogInformation("--SCHEDULAR--CreateAndPublishSyncEvent-Disabled-" + disablePublishSyncEvent);
            }

            return syncEventPayloads;
        }

        #endregion

    }
}
