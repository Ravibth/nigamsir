using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs.RequisitionDTOs
{
    public class RequisitionDurationDTO
    {
       
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Int64? PerDayEffort { get; set; }
    }
}
