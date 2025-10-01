using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class PipelineCodeAndRolesDto
    {
        public string pipelineCode { get; set; }
        public string? jobCode { get; set; }
        public List<string>? roles { get; set; }
    }
    public class RoleEmailsByPipelineCodeResponse
    {
        public string PipelineCode { get; set; }
        public string? JobCode{ get; set; }

        public Dictionary<string, List<string>> RolesEmails { get; set; }
    }
}
