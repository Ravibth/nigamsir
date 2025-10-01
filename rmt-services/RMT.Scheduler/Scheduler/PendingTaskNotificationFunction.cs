using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Azure;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RMT.Scheduler.Constants;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.service;
using RMT.Scheduler.service.AzureServices;

namespace RMT.Scheduler.Scheduler
{
    /// <summary>
    /// PendingTaskNotificationFunction > Not in use anymore
    /// </summary>
    public class PendingTaskNotificationFunction
    {
        private readonly ITokenService _tokenService;
        private readonly IAzureServiceBusService _azureServiceBusService;

        public PendingTaskNotificationFunction(ITokenService tokenService, IAzureServiceBusService azureServiceBusService)
        {
            //_logger = loggerFactory.CreateLogger<SuspendedProjectFunciton>();
            _tokenService = tokenService;
            _azureServiceBusService = azureServiceBusService;
        }

        //Not in Use Anymore
        //Not Used 
        [FunctionName("PendingTaskNotificationFunction")]
        public async Task RunAsync([TimerTrigger("%PendingTaskNotificationSchedulerTriggerTime%")] TimerInfo myTimer, ILogger log)
        {
            //await Execute(log);
        }

        private async Task Execute(ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            string currentToken = await _tokenService.GetToken();
            List<WorkflowDTO> result = await GetWorkflow(log, currentToken);
            var groupedData = result.GroupBy(res => res.assigned_to.ToLower()).ToList();

            log.LogInformation("--PendingTaskNotificationFunction--groupedData--URL-{0}", groupedData);
            // List<WorkflowDTO> allocationWorkflow = result.Where(res => res.workflow.module == "Employee Allocation").ToList();

            for (int i = 0; i < groupedData.Count; i++)
            {
                Dictionary<string, List<WorkflowDTO>> data = new Dictionary<string, List<WorkflowDTO>>();
                List<WorkflowDTO> workflows = new List<WorkflowDTO>();
                foreach (var item in groupedData[i])
                {
                    workflows.Add(item);
                }
                data.Add(groupedData[i].Key, workflows);
                List<NotificationPayload> notificaionItems = new List<NotificationPayload>();

                //Here we made what to send to the service layer
                notificaionItems.Add(new NotificationPayload
                {
                    action = Constant.SUPERCOACH_NOTIFICATION_OF_PENDING_TASK,
                    payload = JsonConvert.SerializeObject(data).ToString(),
                    token = currentToken
                });
                log.LogInformation("--PendingTaskNotificationFunction--notificaionItems--URL-{0}", data);
                if (notificaionItems != null && notificaionItems.Count > 0)
                {
                    log.LogInformation("--initEmailNotificationProcess------Middleware--publishNotification-Count-{0}", notificaionItems.Count);
                    publishNotification(notificaionItems, log);
                }
            }
        }

        public async Task publishNotification(List<NotificationPayload> payload, ILogger logger)
        {
            string type = Constant.NotificationTypeNotification;
            foreach (var item in payload)
            {
                await _azureServiceBusService.PublishNotificationOnAzureServiceBus(item, type, logger);
            }
        }


        public async Task<List<WorkflowDTO>> GetWorkflow(ILogger _logger, string currentToken)
        {
            string taskstatus = "pending";
            string outcome = "inprogress";
            using (var client = AzureHttpClient.GetAzureHttpClient(true))
            {
                var environmentVaribles = Environment.GetEnvironmentVariables();
                var WorkflowTask = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.WORKFLOW_TASK]);
                var gateway = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
                NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
                queryString.Add("taskstatus", taskstatus);
                queryString.Add("outcome", outcome);
                string query = queryString.ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentToken);
                client.DefaultRequestHeaders.Add("Accept", "Application/JSON");

                _logger.LogInformation("--PendingTaskNotificationFunction--GetWorkflow--URL-{0}", gateway + WorkflowTask + query);
                //todo change path from appsettings
                var response = await client.GetAsync(gateway + WorkflowTask + query);
                _logger.LogInformation("--PendingTaskNotificationFunction--GetWorkflow--URL-{0}", response);

                var result = await response.Content.ReadAsStringAsync();
                List<WorkflowDTO> workFlowList = JsonConvert.DeserializeObject<List<WorkflowDTO>>(result);

                return workFlowList;
            }
        }



    }
}
