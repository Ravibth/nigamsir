using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.DTOs.Allocation
{
    public class GetConfirmedPerDayHoursByDateDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool GetClientIds { get; set; }
        public bool GetEmployeeIds { get; set; }
    }
}
