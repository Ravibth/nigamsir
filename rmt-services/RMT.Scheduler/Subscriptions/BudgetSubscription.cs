using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using RMT.Scheduler.Helper;
using RMT.Scheduler.service.AzureServices;
using RMT.Scheduler.service.Configurations;
using RMT.Scheduler.service.WCGT;
using RMT.Scheduler.service;
using System.Threading.Tasks;
using RMT.Scheduler.DTOs;
using System.Collections.Generic;
using RMT.Scheduler.Constants;
using Newtonsoft.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Amqp.Framing;
using System.Linq;
using Microsoft.Azure.WebJobs.ServiceBus;
using static RMT.Scheduler.Constants.Constant;

namespace RMT.Scheduler.Subscriptions
{
    public class BudgetSubscription
    {
        //private readonly ILogger<BudgetSubscription> logger;
        private readonly ITokenService _tokenService;
        private readonly IConfigurationService _configurationService;
        private readonly IAzureServiceBusService _azureServiceBusService;
        private readonly IWCGTHttpService _wcgtService;

        public BudgetSubscription(ILogger<BudgetSubscription> logger, ITokenService tokenService, IConfigurationService configurationService, IWCGTHttpService wcgtService, IAzureServiceBusService azureServiceBusService)
        {
            //this.logger = logger;
            _tokenService = tokenService;
            _configurationService = configurationService;
            _wcgtService = wcgtService;
            _azureServiceBusService = azureServiceBusService;
        }

        [FunctionName("BudgetSubscription")]
        public async Task Run([ServiceBusTrigger("%AzureTopicInit%", "budget-subscription", Connection = "AzureServiceBus")] string serviceBusMessage, ILogger logger)
        {
            logger.LogInformation($"C# ServiceBus topic trigger BudgetSubscription Started, processed message: {serviceBusMessage}");

            try
            {
                var environmentVaribles = Environment.GetEnvironmentVariables();
                int updateBudgetBatchSize = Convert.ToInt32(environmentVaribles[Constant.EnvAppSettingConstants.UPDATE_BUDGET_BATCHSIZE]);
                updateBudgetBatchSize = updateBudgetBatchSize > 0 ? updateBudgetBatchSize : 500;
                string currentToken = await _tokenService.GetToken();

                ProjectBudgetHelper pbHelper = new ProjectBudgetHelper(_tokenService, _configurationService, _wcgtService, _azureServiceBusService, logger);

                NotificationPayloadDTO serviceBusPayload = JsonConvert.DeserializeObject<NotificationPayloadDTO>(serviceBusMessage);

                switch (serviceBusPayload.action)
                {
                    case ServiceBusActions.REFRESH_PROJECT_BUDGET_STATUS:
                        logger.LogInformation("BudgetSubscription---ProcessTopicPayload--Action-{0)--Data-{1} ", serviceBusPayload.action, serviceBusPayload);

                        List<RefreshProjectBudgetStatusPayload> projectBudgetPayload = JsonConvert.DeserializeObject<List<RefreshProjectBudgetStatusPayload>>(serviceBusPayload.payload);

                        List<string> modifiedJobCodes = projectBudgetPayload.Where(a => !string.IsNullOrEmpty(a.JobCode)).Select(a => a.JobCode).Distinct().ToList();

                        logger.LogInformation($"BudgetSubscription--modifiedJobCodes--", string.Join(";", modifiedJobCodes));

                        List<ProjectDTO> modifiedProjects = await pbHelper.GetAllProjectsForBudgetByJobCodes(modifiedJobCodes, currentToken, logger);

                        logger.LogInformation($"BudgetSubscription--GetAllProjectsForBudgetByJobCodes--Completed");

                        var resp = await pbHelper.GetProjectActualBudgetOverShoot(currentToken, logger);

                        logger.LogInformation($"BudgetSubscription--GetProjectActualBudgetOverShoot--Completed");

                        await pbHelper.ProcessProjectForBudget(logger, updateBudgetBatchSize, currentToken, modifiedProjects, false);

                        logger.LogInformation($"BudgetSubscription--ProcessProjectForBudget--Completed");
                        break;
                    default:
                        logger.LogInformation("BudgetSubscription---ProcessTopicPayload--serviceBusPayload.action ", serviceBusPayload.action);
                        break;
                }

                logger.LogInformation("BudgetSubscription---Switch--Completed", serviceBusPayload);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"C# ServiceBus topic trigger BudgetSubscription Exception, : {ex.Message} {ex.StackTrace}");
                logger.LogError(ex, $"C# ServiceBus topic trigger BudgetSubscription InnerException,: {ex.InnerException?.Message} {ex.InnerException?.StackTrace}");
                throw;
            }
            finally
            {
            }

            logger.LogInformation($"C# ServiceBus topic trigger BudgetSubscription Completed, processed message: {serviceBusMessage}");
        }
    }
}
