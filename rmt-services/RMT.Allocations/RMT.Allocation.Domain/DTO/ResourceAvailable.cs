using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class ResourceAvailable
    {
        public Guid? RequisitionId { get; set; }
        public string EmailId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Int64? TotalAvaibleHours { get; set; }
        public Int64 RequireWorkingHours { get; set; }
        public Boolean IsHoursAvialable { get; set; }
        public Boolean isPerDayHourAllocation { get; set; }
        public string? ErrorMsg { get; set; }
        public int? TotalWorkingHours { get; set; }
        public int? TotalWorkingDays { get; set; }
        public DateOnly? LastAvailableDay { get; set; }
    }
}
