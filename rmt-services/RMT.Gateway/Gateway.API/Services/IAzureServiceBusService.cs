using Gateway.API.Dtos;
using Gateway.API.Middlewares;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.API.Services
{
    public interface IAzureServiceBusService
    {
        Task PublishMessageOnAzureServiceBus(NotificationPayload payload, string type, ILogger<NotificatiionMiddleware> logger);
        Task PublishMessageOnAzureServiceBus(NotificationPayload payload, string type, ILogger<NotificatiionMiddleware> logger, string topic);
    }
}
