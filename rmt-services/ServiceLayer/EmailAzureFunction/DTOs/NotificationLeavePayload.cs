using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class NotificationLeavePayload
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public string EmployeeEmail { get; set; }

    }
}
