using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using ServiceLayer.DTOs;
using ServiceLayer.Services.NotificationService;

namespace ServiceLayer
{
    public class NotificationSubscription
    {
        private readonly ILogger<NotificationSubscription> _logger;
        private readonly INotificationService _notificationServices;

        public NotificationSubscription(ILogger<NotificationSubscription> log, INotificationService notificationServices)
        {
            _logger = log;
            _notificationServices = notificationServices;
        }

        [FunctionName("NotificationSubscription")]
        public async Task Run([ServiceBusTrigger("%AzureTopicInit%", "notification-subscription", Connection = "AzureServiceBus")] string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger NotificationSubscription started, processed message: {mySbMsg}");
            try
            {
                var tasks = new List<Task>();
                var message = JsonSerializer.Deserialize<NotificationPayloadDTO>(mySbMsg);
                Task sendNotifications = _notificationServices.InitialiseNotificationTemplates(message);
                tasks.Add(sendNotifications);
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"C# ServiceBus topic trigger NotificationSubscription Exception, : {ex.Message} {ex.StackTrace}");
                _logger.LogError(ex, $"C# ServiceBus topic trigger NotificationSubscription InnerException,: {ex.InnerException?.Message} {ex.InnerException?.StackTrace}");
                throw;
            }
            _logger.LogInformation($"C# ServiceBus topic trigger NotificationSubscription completed, processed message: {mySbMsg}");

        }
    }
}
