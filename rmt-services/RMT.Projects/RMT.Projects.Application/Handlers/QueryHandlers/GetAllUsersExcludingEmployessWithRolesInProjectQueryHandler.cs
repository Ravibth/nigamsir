using RMT.Projects.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class FrontEndRoles
    {
        public string AdditionalElName { get; set; }
        public string AdditionalDelegateName { get; set; }
        public string AdditionalElEmail { get; set; }
        public string AdditionalDelegateEmail { get; set; }
    }
    public class GetAllUsersExcludingEmployessWithRolesInProjectQuery
    {
        public string InputEmail { get; set; }
        public string PipelineCode { get; set; }
        public string JobCode { get; set; }
        public List<FrontEndRoles>? CurrentRoles { get; set; }
    }
    public class GetAllUsersExcludingEmployessWithRolesInProjectQueryHandler
    {
        public GetAllUsersExcludingEmployessWithRolesInProjectQueryHandler()
        {
            
        }
    }
}
