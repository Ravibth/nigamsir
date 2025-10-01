using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Domain.Entities
{
    public class NotificationTemplateLinks
    {
        public Int64 Id { get; set; }
        public string Module { get; set; }
        public string SubModule { get; set; }
        public string Link { get; set; }
    }
}
