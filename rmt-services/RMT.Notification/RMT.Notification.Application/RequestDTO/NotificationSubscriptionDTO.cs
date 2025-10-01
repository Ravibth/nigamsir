using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.RequestDTO
{
    public class NotificationSubscriptionDTO
    {
        public Int64 id { get; set; }
        public string? module { get; set; }
        public string? subscription_role { get; set; }
        public string? user_emailid { get; set; }
        public string? user_name { get; set; }
        public bool? is_active { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public string? createdBy { get; set; }
        public string? modifiedBy { get; set; }
    }
}
