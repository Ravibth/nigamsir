using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Domain.DTOs.Request
{
    public class ProjectRequisitionAllocationRequestDTO
    {
        public string pipelineCode { get; set; }
        public string? jobCode { get; set; }
        public int requisitionCountAdded { get; set; }
        public int allocationCountAdded { get; set; }
    }
}
