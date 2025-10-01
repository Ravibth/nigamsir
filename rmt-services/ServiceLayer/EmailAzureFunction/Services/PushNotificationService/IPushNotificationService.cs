using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.PushNotificationService
{
    public interface IPushNotificationService
    {
        Task PostNewPushNotification(List<PostNewPushNotificationDTO> payload, string token);
    }
}
