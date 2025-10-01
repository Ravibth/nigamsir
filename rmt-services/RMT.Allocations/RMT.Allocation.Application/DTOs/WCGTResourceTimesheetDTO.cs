using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs
{
    public class WCGTResourceTimesheetDTO
    {
        public string empcode { get; set; }
        public string empname { get; set; }
        public Double totaltime { get; set; }
        public Double? timesheetcost { get; set; }
    }
}
