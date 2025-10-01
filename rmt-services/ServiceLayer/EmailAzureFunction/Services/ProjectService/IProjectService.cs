using Microsoft.Extensions.Logging;
using ServiceLayer.DTOs;
using ServiceLayer.Services.MarketPlaceService.DTOs;
using ServiceLayer.Services.ProjectService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.ProjectService
{
    public interface IProjectService
    {
        Task<List<RoleEmailsByPipelineCodeResponse>> GetRolesEmailByPipelineCodesAndRoles(List<PipelineCodeAndRolesDto> pipelineCodeAndRolesDto, string token);
        Task<List<GetMembersOfAllProjectsOfUserResponse>> GetListOfAllMembersOfAllProjectsOfUser(List<string> users, string token);
        Task<List<ProjectRolesResponseDTO>> GetResourceRequestorEmailsByPipelineCode(string pipelineCode, string jobCode,string token);
        Task<Project> GetProjectDetailByPipelineCode(string pipelineCode, string token);

        Task<MarketPlaceProjectDetailResponse> ProcessTopicPayload(NotificationPayloadDTO serviceBusPayload);
        Task<string> SuspendProjects(List<string> projectCodes, string currentToken);
        Task<string> ProjectActivationStatusChange(string PipelineCode, string? JobCode, string token, bool IsJobClosed);
        Task ReplaceProjectsSuperCoachRole(SuperCoachProjectRole request, string token);

    }
}
