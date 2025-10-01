using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class TimesheetDaysReponseDTO
    {
        public string key { get; set; }
        public string designation { get; set; }
        public DateTime monthname { get; set; }
        public Int64 totaltime { get; set; }
        public Int64 timesheetcost { get; set; }
    }
}
