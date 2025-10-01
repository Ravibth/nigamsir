using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class ResourceAllocatedHourRequest
    {
        public List<string> EmpEmail { get; set; }
        public string PipelineCode { get; set; }
        public string JobCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
    public class ResourceAllocatedHour
    {
        public string EmpEmail { get; set; }
        public int  AllocatedHours { get; set; }
    }
}
