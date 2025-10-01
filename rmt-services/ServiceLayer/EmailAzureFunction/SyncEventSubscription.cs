using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.DTOs;
using ServiceLayer.Services.SyncEventService;

namespace ServiceLayer
{
    public class SyncEventSubscription
    {
        private readonly ILogger<SyncEventSubscription> _logger;
        private readonly ISyncEvent _syncEvent;
        public SyncEventSubscription(ILogger<SyncEventSubscription> logger, ISyncEvent syncEvent)
        {
            _logger = logger;
            _syncEvent = syncEvent;
        }
        [FunctionName("SyncEventSubscription")]
        public async Task Run([ServiceBusTrigger("%AzureTopicInit%", "sync-event-subscription", Connection = "AzureServiceBus")] string mySbMsg, ILogger log)
        {
            try
            {
                log.LogInformation($"C# ServiceBus topic trigger function Start processed message: {mySbMsg}");

                SyncEventPayloadDTO syncEvent = JsonConvert.DeserializeObject<SyncEventPayloadDTO>(mySbMsg);
                await _syncEvent.initSyncEvent(syncEvent);

                log.LogInformation($"C# ServiceBus topic trigger function End processed message: {mySbMsg}");
            }
            catch (Exception ex)
            {
                log.LogInformation(ex, "--ServiceBus---SyncEvent---");
                throw;
            }
        }
    }
}
