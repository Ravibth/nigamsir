using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.HttpServices.DTOs
{
    public class TerminateWorkflowByPipelineCodeAndJobCodeRequestDTO
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
    }
}
