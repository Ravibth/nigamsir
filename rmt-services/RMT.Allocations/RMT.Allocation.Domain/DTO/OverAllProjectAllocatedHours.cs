using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class OverAllProjectAllocatedHours
    {
        public string PipelineCode { get; set; }
        public string JobCode { get; set; }
        public int TotalAllocatedHours { get; set; }
    }
}
