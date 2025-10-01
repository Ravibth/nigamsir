using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RMT.Notification.Application.Responses
{
    public class UserNotificationsResponse
    {
        public Guid id {  get; set; }
        public Guid notification_id { get; set; }
        public string email { get; set; }
        public string? employee_id { get; set; }
        public bool isRead { get; set; }
        public DateTime createdDate { get; set; }
        public string createdBy { get; set; }
        public string type { get; set; }
        public string message { get; set; }
        public string link { get; set; }
        public JsonDocument? meta { get; set; }

    }
}
