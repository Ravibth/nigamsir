using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.Responses
{
    public class NotificationResponse
    {
        public Int64 Notification_Id { get; set; }
        public string Sender_EmailId { get; set; }
        public string Sender_EmpCode { get; set; }
        public string Receiver_Id { get; set; }
        public string Receiver_EmailId { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Is_Read { get; set; }
        public Int64 Notification_Type_Id { get; set; }
    }
}
