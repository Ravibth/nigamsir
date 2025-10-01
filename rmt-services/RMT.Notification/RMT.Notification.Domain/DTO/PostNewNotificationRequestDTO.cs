using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RMT.Notification.Domain.DTO
{
    public class PostNewNotificationRequestDTO
    {
        public Int64 notification_template_id {  get; set; }
        public string type { get; set; }
        public string message { get; set; }
        public string link { get; set; }
        public JsonDocument meta { get; set; }
        public string createdBy { get; set; }
        public DateTime createdDate { get; set; }
    }
}
