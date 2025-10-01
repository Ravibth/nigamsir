using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Domain.Entities
{
    public class UserNotification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }
        public Guid notification_id { get; set; }
        public string email { get; set; }
        public string? employee_id { get; set; }
        public bool isRead { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime modifiedDate { get; set; }
        public string createdBy { get; set; }
        public string modifiedBy { get; set; }
        [ForeignKey("notification_id")]
        public virtual NotificationEntity notification { get; set; }
    }
}
