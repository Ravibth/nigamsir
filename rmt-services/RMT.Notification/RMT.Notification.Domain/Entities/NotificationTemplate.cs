using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Domain.Entities
{
    public class NotificationTemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        public string? module { get; set; }
        public string? sub_module { get; set; }
        public string subject { get; set; }
        public string type { get; set; }
        public bool is_active { get; set; }
        public string notification_type { get; set; }
        public string to { get; set; }
        public string? cc { get; set; }
        public string template { get; set; }

        public Int64? link_id { get; set; }

        [ForeignKey("link_id")]
        public virtual NotificationTemplateLinks link { get; set; }

        [ForeignKey("notification_template_id")]
        public virtual List<NotificationPlaceHolder> payload { get; set; }

        public string? subscription_role { get; set; }

    }
}
