using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs
{
    public class WCGTTimesheetGroupDTO
    {
        public string key { get; set; }
        public string designation { get; set; }
        public DateTime monthname { get; set; }
        public Double totaltime { get; set; }
        public Double timesheetcost { get; set;}
    }
}
