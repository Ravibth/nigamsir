using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.ProjectService.DTOs
{
    public class AllProjectsOfUserWithMembers
    {
        public string ProjectName { get; set; }
        public Dictionary<string, List<string>> RoleEmails { get; set; }
    }
    public class GetMembersOfAllProjectsOfUserResponse
    {
        public string userEmail { get; set; }
        public List<AllProjectsOfUserWithMembers> ProjectMembers { get; set; }

    }
}
