using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServiceLayer.DTOs;
using ServiceLayer.Services.ProjectService;
using ServiceLayer.Services.WorkflowService;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class WorkflowSubscription
    {
        private readonly ILogger<WorkflowSubscription> _logger;
        private readonly IWorkflowService _workflowService;

        public WorkflowSubscription(ILogger<WorkflowSubscription> log, IWorkflowService workflowService)
        {
            _workflowService = workflowService;
            _logger = log;
        }

        //todo uncomment later
        [FunctionName("WorkflowSubscription")]
        public async Task Run([ServiceBusTrigger("%AzureTopicInit%", "workflow-subscription", Connection = "AzureServiceBus")] string mySbMsg)
        {
            var serviceBusClient = Helper.CreateServiceBusClient(_logger);
            var receiver = serviceBusClient.CreateReceiver(Environment.GetEnvironmentVariable("AzureTopicInit"), Environment.GetEnvironmentVariable("WorkflowSubscriptionName"));
            _logger.LogInformation("--ServiceBus---Workflow--- Created service bus client");

            _logger.LogInformation($"--ServiceBus---Workflow--- Started C# ServiceBus topic trigger function processed message: {mySbMsg}");
            var message = JsonSerializer.Deserialize<NotificationPayloadDTO>(mySbMsg);
            var errors = new List<Exception>();
            try
            {
                Task workflow = _workflowService.initWorkflow(message);
                if (workflow != null)
                {
                    var tasks = new List<Task>();
                    tasks.Add(workflow);
                    await Task.WhenAll(tasks);
                }
            }
            catch (Exception ex)
            {
                //errors.Add(ex);
                _logger.LogError(ex, "--ServiceBus---Workflow--- WorkflowSubscription Exception, Message:" + ex.Message + "StackTrace:" + ex.StackTrace);
                _logger.LogInformation("--ServiceBus---Workflow--- WorkflowSubscription Exception, InnerMessage: " + ex.InnerException?.Message + "InnerStackTrace: " + ex.InnerException?.StackTrace);
                throw;
            }
            finally
            {
                await receiver.CloseAsync();
                await serviceBusClient.DisposeAsync();
            }
            _logger.LogInformation($"--ServiceBus---Workflow--- Completed C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }


    }
}