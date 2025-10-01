using RMT.Allocation.Application.Handlers.CommandHandlers;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Utils
{
    public static class Helper
    {
        public static async Task AddProjectRoles(List<AddProjectUserRole> projectRoles, long projectId, string pipelineCode, string? jobCode, IProjectServiceHttpApi _projectServiceHttpApi)
        {
            /************** Add Role in Project Role table **********************/
            AddProjectUserCommand obj = new AddProjectUserCommand();
            if (projectId == 0 && pipelineCode != null)
            {
                ProjectDTO projectDetail = await _projectServiceHttpApi.GetProjectDetailsByCode(pipelineCode, jobCode);
                projectId = (long)projectDetail.Id;
            }
            if (projectId != 0)
            {
                foreach (var role in projectRoles)
                {
                    role.ProjectId = projectId;
                    role.IsActive = true;
                }
                obj.AddProjectUserRoles = projectRoles.ToArray();
                var projectRolesResponse = await _projectServiceHttpApi.AddProjectUserWithRole(obj);
            }
        }

        public static async Task RemoveProjectRoles(List<AddProjectUserRole> projectRoles, long projectId, string pipelineCode, string? jobCode, IProjectServiceHttpApi _projectServiceHttpApi)
        {
            /************** Add Role in Project Role table **********************/
            AddProjectUserCommand obj = new AddProjectUserCommand();
            if (projectId == 0 && pipelineCode != null)
            {
                ProjectDTO projectDetail = await _projectServiceHttpApi.GetProjectDetailsByCode(pipelineCode, jobCode);
                projectId = (long)projectDetail.Id;
            }
            if (projectId != 0)
            {
                foreach (var role in projectRoles)
                {
                    role.ProjectId = projectId;
                    role.IsActive = false;
                }
                obj.AddProjectUserRoles = projectRoles.ToArray();
                var projectRolesResponse = await _projectServiceHttpApi.RemoveProjectUserWithRole(obj);
            }
        }

        
    }
}
