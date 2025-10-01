using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.HttpServices.DTO
{
    public class CommonSenderDTO
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
