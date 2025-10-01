using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.IHttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices
{
    public class AzureHttpService: IAzureHttpService
    {
        private readonly IConfiguration _config;
        private readonly ServiceBusClient _client;
        public AzureHttpService(IConfiguration config, ServiceBusClient client)
        {
            _config = config;
            _client = client;
        }
        public async Task PublishMessageOnAzureServiceBus(RefreshPayload payload, string type)
        {
            var topic = _config.GetSection("AzureSBConfig").GetSection("AzureTopicInit").Value;
            await this.PublishMessageOnAzureServiceBus(payload, type, topic);
        }
        public async Task PublishMessageOnAzureServiceBus(RefreshPayload payload, string type, string topic)
        {
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
                    //logger.LogInformation("--PublishMessage----ServiceBus--PublishMessageOnAzureServiceBus-Success");
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
                //logger.LogError(ex, "--PublishMessage----ServiceBus--PublishMessageOnAzureServiceBus-Exception: " + ex.Message);
                //logger.LogError(ex, "--PublishMessage--Service bus calls exception: " + ex.Message);
                throw;
            }
            //logger.LogInformation("--PublishMessage----ServiceBus--PublishMessageOnAzureServiceBus-Ends");
        }
    }
}
