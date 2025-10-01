using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.DTOs
{
    public class InitNotificationDTO
    {
        public string path { get; set; }
        public string response_payload { get; set; }
        public string? token { get; set; }
        public string? request_payload { get; set; }
        public string? userinfo { get; set; }


    }
    public class NotificationPayload
    {
        public string? token { get; set; }
        public string action { get; set; }
        public string payload { get; set; }
    }
}
