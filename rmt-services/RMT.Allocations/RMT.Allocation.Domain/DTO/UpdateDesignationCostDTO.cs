using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class UpdateDesignationCostDTO
    {
        public string EmpEmail { get; set; }
        public string? Designation { get; set; }
        public string? Grade { get; set; }
        public string? RatePerHour { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
