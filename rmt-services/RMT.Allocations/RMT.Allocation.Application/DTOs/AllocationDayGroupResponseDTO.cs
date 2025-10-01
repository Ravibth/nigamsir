using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs
{
    public class AllocationDayGroupResponseDTO
    {
        public string key { get; set; }
        public DateTime monthname { get; set; }
        public Double totalAllocationTime { get; set; }
        public Double totalAllocationCost { get; set; }

        public Double totalTimesheetTime { get; set; }
        public Double totalTimesheetCost { get; set; }
        public string designation { get; set; }
        public string grade { get; set; }
    }
}
