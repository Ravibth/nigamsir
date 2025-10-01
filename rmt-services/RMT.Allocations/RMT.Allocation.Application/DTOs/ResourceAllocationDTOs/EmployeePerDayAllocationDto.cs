using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs.ResourceAllocationDTOs
{
    public class EmployeePerDayAllocationDto
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string PipelineName { get; set; }
        public string? JobName { get; set; }
        public string EmpEmail { get; set; }
        public string EmpMID { get; set; }

        public DateTime AllocationDate { get; set; }
        public double AllocatedPerDayHour { get; set; }

        public string? ClientId { get; set; }

    }
}
