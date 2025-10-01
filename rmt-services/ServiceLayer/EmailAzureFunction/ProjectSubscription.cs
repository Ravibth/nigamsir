using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.DTOs;
using ServiceLayer.Services.NotificationService;
using ServiceLayer.Services.ProjectService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ProjectSubscription
    {
        private readonly ILogger<ProjectSubscription> logger;

        private readonly IProjectService projectService;

        public ProjectSubscription(ILogger<ProjectSubscription> logger, IProjectService projectService)
        {
            this.logger = logger;
            this.projectService = projectService;
        }

        //A new topic can also be created with new subscription for project relayted updates if needed
        [FunctionName("ProjectSubscription")]
        public async Task Run([ServiceBusTrigger("%AzureTopicInit%", "project-subscription", Connection = "AzureServiceBus")] string serviceBusMessage)
        {
            logger.LogInformation($"C# ServiceBus topic trigger ProjectSubscription started, processed message: {serviceBusMessage}");

            var serviceBusClient = Helper.CreateServiceBusClient(logger);
            var receiver = serviceBusClient.CreateReceiver(Environment.GetEnvironmentVariable("AzureTopicInit"), Environment.GetEnvironmentVariable("ProjectSubscriptionName"));
            logger.LogInformation("--ServiceBus---project--- Created service bus client");

            logger.LogInformation($"--ServiceBus---project--- Started C# ServiceBus topic trigger function processed message");
            NotificationPayloadDTO topicPayload = JsonConvert.DeserializeObject<NotificationPayloadDTO>(serviceBusMessage);
            var errors = new List<Exception>();
            try
            {
                Task projectPayloadTask = projectService.ProcessTopicPayload(topicPayload);
                if (projectPayloadTask != null)
                {
                    var tasks = new List<Task>();
                    tasks.Add(projectPayloadTask);
                    await Task.WhenAll(tasks);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"C# ServiceBus topic trigger ProjectSubscription Exception, : {ex.Message} {ex.StackTrace}");
                logger.LogError(ex, $"C# ServiceBus topic trigger ProjectSubscription InnerException,: {ex.InnerException?.Message} {ex.InnerException?.StackTrace}");
                throw;
            }
            finally
            {
                await receiver.CloseAsync();
                await serviceBusClient.DisposeAsync();
            }

            logger.LogInformation($"C# ServiceBus topic trigger ProjectSubscription started, processed message: {serviceBusMessage}");
        }
    }
}
