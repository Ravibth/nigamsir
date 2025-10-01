using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.Entities
{
    public class AllocationDayGroup
    {
        public DateTime? monthname { get; set; }
        public Double? totaltime { get; set; }
        public string? designation { get;set; }
        public string? grade { get;set; }
        public Double? cost { get; set; }
    }
}
