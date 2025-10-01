using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Domain.DTOs.Response
{
    public class GetProjectRolesByEmailsResponse
    {
        public string User { get; set; }
        public List<string> Role { get; set; }
    }
}
