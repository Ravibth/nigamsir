using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class RefreshAssignedWorkflowTaskDTO
    {
        public string? previousAssignTo { get; set; }
        public string? currentAssignTo { get; set; }
        public string? employeeEmail { get; set; }
        public string? pipelineCode { get; set; }
        public string? jobCode { get; set; }
        public string? type { get; set; }
    }
}
