using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Responses
{
    public class RoleEmailsByPipelineCodeResponse
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public Dictionary<string, List<string>> RolesEmails { get; set; }
    }
}
