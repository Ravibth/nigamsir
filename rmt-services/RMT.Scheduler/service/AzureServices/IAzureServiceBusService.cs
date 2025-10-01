using Microsoft.Extensions.Logging;
using RMT.Scheduler.DTOs;
using System.Threading.Tasks;

namespace RMT.Scheduler.service.AzureServices
{
    public interface IAzureServiceBusService
    {
        Task PublishMessageOnAzureServiceBus(SyncEventPayload payload, string type, ILogger log);
        Task PublishNotificationOnAzureServiceBus(NotificationPayload payload, string type, ILogger logger);
    }
}