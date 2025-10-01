using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RMT.Notification.Domain.Entities
{
    public class NotificationEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }
        public string item_id { get; set; }
        public Int64 notification_template_id {  get; set; }
        public string type { get; set; }
        //To is kept interim to store user/group/role
        public string to { get; set; }
        public string message { get; set; }
        public JsonDocument? meta { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
        public string createdBy { get; set; }
        public string modifiedBy { get; set; }
        public string link { get; set; }
        [ForeignKey("notification_template_id")]
        public virtual NotificationTemplate notificationTemplateMaster { get; set; }
    }
}
