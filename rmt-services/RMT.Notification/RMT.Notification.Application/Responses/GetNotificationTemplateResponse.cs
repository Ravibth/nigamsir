using RMT.Notification.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.Responses
{
    public class GetNotificationTemplateResponse
    {
        public Int64 Id { get; set; }
        public string module { get; set; }
        public string sub_module { get; set; }
        public string subject { get; set; }
        public string type { get; set; }
        public bool is_active { get; set; }
        public string notification_type { get; set; }
        public string to { get; set; }
        public string? cc{ get; set; }
        public string template { get; set; }
        public string link {  get; set; }   
        public virtual List<NotificationPlaceHolder> payload { get; set; }
    }
}
