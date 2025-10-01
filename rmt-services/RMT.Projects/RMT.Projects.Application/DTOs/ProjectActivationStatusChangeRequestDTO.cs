using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.DTOs
{
    public class ProjectActivationStatusChangeRequestDTO
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public bool IsJobClosed { get; set; }

    }
}
