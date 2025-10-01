using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Amqp.Framing;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RMT.Scheduler.Constants;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.service;
using RMT.Scheduler.service.Allocation.DTOs;
using RMT.Scheduler.service.AzureServices;
using static RMT.Scheduler.Constants.Constant;

namespace RMT.Scheduler.Scheduler
{
    /// <summary>
    /// SummaryNotificationFunction > to send all the schdeuled notification emails by consolidating the data and send single email overnight
    /// </summary>
    public class SummaryNotificationFunction
    {
        private readonly ITokenService _tokenService;
        private readonly IAzureServiceBusService _azureServiceBusService;

        public SummaryNotificationFunction(ITokenService tokenService, IAzureServiceBusService azureServiceBusService)
        {
            //_logger = loggerFactory.CreateLogger<SuspendedProjectFunciton>();
            _tokenService = tokenService;
            _azureServiceBusService = azureServiceBusService;
        }

        [FunctionName("SummaryNotificationFunction")]
        public void Run([TimerTrigger("%SummaryNotificationSchedulerTriggerTime%")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"SummaryNotificationFunction executed at: {DateTime.Now}");
            SendAllocationSummaryMail(log);
            log.LogInformation($"SummaryNotificationFunction Execution Completed at: {DateTime.Now}");
        }

        public async Task SendAllocationSummaryMail(ILogger log)
        {
            try
            {
                List<NotificationPayload> notificaionItems = new List<NotificationPayload>();
                log.LogInformation($"--SendAllocationSummaryMail function executed at: {DateTime.Now}");

                string currentToken = await _tokenService.GetToken();
                List<string> actions = new List<string>()
                {
                    NotificationTemplateType.ALLOCATION_SUMMARY_NOTIFICATION ,
                    NotificationTemplateType.CONSOLIDATED_MAIL_TO_EMPLOYEE_FOR_PROJECT_LISTING_IN_MARKETPLACE,
                    NotificationTemplateType.ADDITION_OF_NEW_PROJECT_NOTIFICATION_TO_RESOURCE_REQUESTOR,
                    NotificationTemplateType.RMS_ALLOCATION_AND_SKILL_TASKS_PENDING_FOR_ACTION_SUMMARY_MAIL
                };
                //Here we made what to send to the service layer
                foreach (var action in actions)
                {
                    notificaionItems.Add(new NotificationPayload
                    {
                        action = action,
                        payload = "",
                        token = currentToken
                    });
                }
                try
                {
                    log.LogInformation($"--SendAllocationSummaryMail--notificaionItems-{JsonConvert.SerializeObject(notificaionItems)}");
                }
                catch
                {
                }
                if (notificaionItems != null && notificaionItems.Count > 0)
                {
                    log.LogInformation("--SendAllocationSummaryMail--notificaionItems--notificaionItems-Count-{0}", notificaionItems.Count);
                    publishNotification(notificaionItems, log);
                    log.LogInformation("--SendAllocationSummaryMail--notificaionItems--publishNotification-Completed--");
                }
                else
                {
                    log.LogInformation("--SendAllocationSummaryMail--notificaionItems--Count is 0");
                }
            }
            catch (Exception ex)
            {
                log.LogInformation($"--SendAllocationSummaryMail function Exception : {ex}");
                throw;
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
    }
}
