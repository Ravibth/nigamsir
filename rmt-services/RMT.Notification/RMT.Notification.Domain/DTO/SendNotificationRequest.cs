using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RMT.Notification.Domain.DTO
{
    public class SendNotificationRequest
    {
      //  public string NotificationType { get; set; }
        public string NotificationKey { get; set; }
        public string meta { get; set; }
    }
}
