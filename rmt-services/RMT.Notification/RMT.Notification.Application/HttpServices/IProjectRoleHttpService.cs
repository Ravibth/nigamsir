using RMT.Notification.Application.HttpServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.HttpServices
{
    public interface IProjectRoleHttpService
    {
        public Task<List<RoleEmailsByPipelineCodeResponse>> GetEmailOfProjectRoles(string[] inputEmail, string pipelineCode, string jobCode);
    }
}
