using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using RMT.Scheduler.Constants;
using RMT.Scheduler.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RMT.Scheduler.service.AzureServices
{
    public class AzureServiceBusService : IAzureServiceBusService
    {
        private readonly ServiceBusClient _client;
        public AzureServiceBusService(ServiceBusClient client)
        {
            _client = client;

        }

        public async Task PublishMessageOnAzureServiceBus(SyncEventPayload payload, string type, ILogger log)
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            var topic = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.AZURE_TOPIC_INIT]);
            await this.PublishMessageOnAzureServiceBus(payload, type, topic, log);
        }

        public async Task PublishMessageOnAzureServiceBus(SyncEventPayload payload, string type, string topic, ILogger log)
        {
            log.LogInformation("PublishMessage----ServiceBus--PublishMessageOnAzureServiceBus Start topic-" + topic + ", type-" + type + ", action-" + payload.action);

            try
            {
                var sender = _client.CreateSender(topic);
                var body = JsonSerializer.Serialize(payload);
                var massage = new ServiceBusMessage(body);
                massage.ApplicationProperties.Add("type", type);
                await sender.SendMessageAsync(massage);
                log.LogInformation("PublishMessage----ServiceBus--PublishMessageOnAzureServiceBus Message Send");
            }
            catch (Exception ex)
            {
                log.LogInformation(ex, "PublishMessage----ServiceBus--PublishMessageOnAzureServiceBus" + ex.Message);
                throw;
            }
        }
        
        public async Task PublishNotificationOnAzureServiceBus(NotificationPayload payload, string type, ILogger logger)
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            var topic = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.AZURE_TOPIC_INIT]);
            await this.PublishNotificationOnAzureServiceBus(payload, type, logger, topic);
        }

        public async Task PublishNotificationOnAzureServiceBus(NotificationPayload payload, string type, ILogger logger, string topic)
        {
            logger.LogInformation("--PublishMessage----ServiceBus--PublishMessageOnAzureServiceBus-Started");
            try
            {
                await using (var sender = _client.CreateSender(topic))
                {
                    var body = JsonSerializer.Serialize(payload);
                    var message = new ServiceBusMessage(body);
                    message.ApplicationProperties.Add("type", type);
                    //try
                    //{
                    await sender.SendMessageAsync(message);
                    logger.LogInformation("--PublishMessage----ServiceBus--PublishMessageOnAzureServiceBus-Success");
                    //}
                    //catch (Exception ex)
                    //{
                    //    throw;
                    //}
                    //finally
                    //{
                    //    sender.CloseAsync();
                    //}
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "--PublishMessage----ServiceBus--PublishMessageOnAzureServiceBus-Exception: " + ex.Message);
                //logger.LogError(ex, "--PublishMessage--Service bus calls exception: " + ex.Message);
                throw;
            }
            logger.LogInformation("--PublishMessage----ServiceBus--PublishMessageOnAzureServiceBus-Ends");
        }
    }
}
