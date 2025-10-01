using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class TimesheetResponseDTO
    {
        //public string Designation { get; set; }
        public string Gradename { get; set; }
        public Double TimesheetHrs { get; set; }
        public Double TimesheetCost { get; set; }
    }
}
