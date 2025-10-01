using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class InitNotificationDTO
    {
        public string path { get; set; }
        public string response_payload { get; set; }
        public string? token { get; set; }
        public string? request_payload { get; set; }
        public string? userinfo { get; set; }

    }
}
