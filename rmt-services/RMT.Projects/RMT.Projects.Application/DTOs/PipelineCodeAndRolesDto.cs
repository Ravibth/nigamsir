using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.DTOs
{
    public class PipelineCodeAndRolesDto
    {
        public string pipelineCode { get; set; }
        public string? jobCode { get; set; }
        public List<string>? roles { get; set; }
    }
}
