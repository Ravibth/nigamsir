using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Domain.DTO
{
    public class NotificationDTO
    {
        public string To { get; set; }  
        public string? CC { get; set; }
        public string Subject {  get; set; }
        public string Body { get; set; }
        public string? From { get; set; }

    }
}
