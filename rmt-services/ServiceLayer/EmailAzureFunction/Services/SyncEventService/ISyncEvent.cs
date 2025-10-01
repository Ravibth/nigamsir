using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.SyncEventService
{
    public interface ISyncEvent
    {
         Task initSyncEvent(SyncEventPayloadDTO payload);
    }
}
