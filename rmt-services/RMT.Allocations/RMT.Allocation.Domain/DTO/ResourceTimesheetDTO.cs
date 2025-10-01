using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class ResourceTimesheetDTO
    {
        public string email_id { get; set; }
        public Int64 totaltime { get; set; }
        public Int64 timesheetcost { get; set; }
    }
}
