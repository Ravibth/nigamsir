using System.Collections.Generic;

namespace Gateway.API.Dtos
{

    public class RoleEmailsByPipelineCodeResponse
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public Dictionary<string, List<string>> RolesEmails { get; set; }
    }

    public class PipelineCodeAndRolesDto
    {
        public string pipelineCode { get; set; }
        public string jobCode { get; set; }
        public List<string>? roles { get; set; }
    }

}
