using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class NotificationPayloadDTO
    {
        public string token { get; set; }
        public string action { get; set; }
        public string payload { get; set; }
    }
}
