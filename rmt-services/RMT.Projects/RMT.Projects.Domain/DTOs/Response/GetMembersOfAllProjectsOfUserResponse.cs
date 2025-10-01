using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Domain.DTOs.Response
{
    public class AllProjectsOfUserWithMembers
    {
        //public string ProjectName { get; set; }//feb
        public string PipelineName { get; set; }//feb
        public string JobName { get; set; }//feb
        public Dictionary<string, List<string>> RoleEmails { get; set; }

    }
    public class GetMembersOfAllProjectsOfUserResponse
    {
        public string userEmail { get; set; }
        public List<AllProjectsOfUserWithMembers> ProjectMembers { get; set; }


    }
}
