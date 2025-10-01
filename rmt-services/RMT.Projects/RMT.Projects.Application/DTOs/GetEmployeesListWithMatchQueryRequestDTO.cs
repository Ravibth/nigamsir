using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.DTOs
{
    public class GetEmployeesListWithMatchQueryRequestDTO
    {
        public string InputEmail { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public List<string>? UsersNotToInclude { get; set; }
    }
}
