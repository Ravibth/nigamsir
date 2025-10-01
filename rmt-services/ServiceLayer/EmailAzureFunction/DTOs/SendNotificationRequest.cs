using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class SendNotificationRequest
    {
        public string NotificationKey { get; set; }
        public string meta { get; set; }
    }
}
