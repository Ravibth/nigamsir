using Microsoft.Extensions.Logging;
using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.NotificationService
{
    public interface INotificationService
    {
        Task InitialiseNotificationTemplates(NotificationPayloadDTO notificationPayloadDTO);
        //Task InitNotification(NotificationPayloadDTO notificationPayloadDTO);
    }
}
