using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class PostNewPushNotificationDTO
    {
        public Int64 notification_template_id { get; set; }
        public string[] users { get; set; }
        public string? type { get; set; }
        public string? message { get; set; }
        public string link { get; set; }
        public JsonDocument? meta { get; set; }
    }
}
