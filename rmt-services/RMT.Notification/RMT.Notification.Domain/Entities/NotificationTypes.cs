using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Domain.Entities
{
    public class NotificationTypes
    {
        public Int64  Id { get; set; }
        public string Name { get; set; }    
        public bool IsActive { get; set; }       
    }
}
