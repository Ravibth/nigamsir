using Microsoft.Extensions.Logging;
using ServiceLayer.DTOs.Budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.ServiceBus
{
    public interface IAzureServiceBusService
    {
        //Task PublishMessageOnAzureServiceBus(SyncEventPayload payload, string type, ILogger log);
        Task PublishNotificationOnAzureServiceBus(NotificationPayload payload, string type, ILogger logger);
    }
}
