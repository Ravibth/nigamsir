using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs.CommonResourceAllocationDTOs
{
    public class FormValuesForAllocationDTO
    {
        public DateTime? ConfirmedAllocationStartDate { get; set; }
        public DateTime? ConfirmedAllocationEndDate { get; set; }
        public Int64? ConfirmedPerDayHours { get; set; }
        public bool? PerDayAllocation { get; set; }
    }
}
