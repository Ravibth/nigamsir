using RMT.Projects.Application.Handlers.QueryHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class GetAllUsersListWithMatchRequestDTO
    {
        public string InputEmail { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public List<CurrentUserRoles>? CurrentUserRoles { get; set; }
        public List<string>? UsersNotToInclude { get; set; }
    }
}
