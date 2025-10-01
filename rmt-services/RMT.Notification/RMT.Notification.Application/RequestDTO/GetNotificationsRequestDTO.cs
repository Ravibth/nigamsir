using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.RequestDTO
{
    public class GetNotificationsRequestDTO
    {
        public string email { get; set; }
        public Int64 limit { get; set; }
        public Int64 pagination { get; set; }
    }
}
