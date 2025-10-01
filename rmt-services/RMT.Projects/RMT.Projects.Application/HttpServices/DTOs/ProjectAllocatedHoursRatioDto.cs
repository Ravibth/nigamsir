using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.HttpServices.DTOs
{
    public class ProjectAllocatedHoursRatioDto
    {
        public string pipelineCode { get; set; }
        public string? jobCode { get; set; }
        public double requistionTotalHours { get; set; }
        public double allocatedTotalHours { get; set; }

    }
}
