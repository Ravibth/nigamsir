using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.RequestDTO
{
    public class NotificationSeederDTO
    {
        public string module { get; set; }
        public string sub_module { get; set; }
        public string subject { get; set; }
        public string type { get; set; }
        public bool is_active { get; set; }
        public string notification_type { get; set; }
        public string to { get; set; }
        public string template { get; set; }

    }
}
