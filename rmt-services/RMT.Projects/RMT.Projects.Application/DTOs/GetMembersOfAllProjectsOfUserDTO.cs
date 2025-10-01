using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.DTOs
{
    public class GetMembersOfAllProjectsOfUserDTO
    {
        public List<string> users { get; set; }
        public List<string>? roles { get; set; }
    }
}
