using RMT.Allocation.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.IHttpServices
{
    public interface IAzureHttpService
    {
        Task PublishMessageOnAzureServiceBus(RefreshPayload payload, string type);
    }
}
