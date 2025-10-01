using Azure.Core;
using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Gateway.API.Dtos;
using Gateway.API.Middlewares;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gateway.API.Services
{
    public class AzureServiceBusService : IAzureServiceBusService
    {
        private readonly IConfiguration _config;
        private readonly ServiceBusClient _client;
        public AzureServiceBusService(IConfiguration config, ServiceBusClient client)
        {
            _client = client;
            _config = config;
        }

        public async Task PublishMessageOnAzureServiceBus(NotificationPayload payload, string type, ILogger<NotificatiionMiddleware> logger)
        {
            var topic = _config.GetSection("AzureSBConfig").GetSection("AzureTopicInit").Value;
            this.PublishMessageOnAzureServiceBus(payload, type, logger, topic);
        }

        public async Task PublishMessageOnAzureServiceBus(NotificationPayload payload, string type, ILogger<NotificatiionMiddleware> logger, string topic)
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
