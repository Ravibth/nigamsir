
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.Handlers.CommandHandlers;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.IHttpServices
{
    public interface IProjectServiceHttpApi
    {
        Task<List<ProjectDTO>> GetProjectDetailsByProjectCode(List<KeyValuePair<string, string>> projectCode, string token);
        Task<List<AddProjectUserRole>> AddProjectUserWithRole(AddProjectUserCommand projectCode);
        Task<List<AddProjectUserRole>> RemoveProjectUserWithRole(AddProjectUserCommand projectCode);
        Task<ProjectDTO> GetProjectDetailsByCode(string pipelineCode, string? jobCode);
        Task<List<ResourceRequestorsWithPipelineCodesDTO>> GetResourceRequestorsByPipelineCodes(List<KeyValuePair<string, string?>> pipelineCodes, string token);
        Task<List<ProjectBudgetDto>> GetProjectBudget(string pipelineCode, string jobCode);
        Task<List<RoleEmailsByPipelineCodeResponse>> GetEmployeeRoleByByPipelineJobCodes(List<PipelineCodeAndRolesDto> pipelineCodeAndRolesDto);

        Task<List<ProjectFullDetailsResponse>> GetProjectFullDetailsByUniqueCodes(List<KeyValuePair<string, string?>> projectUniqueCodes);
        Task<ProjectFullDetailsResponse> UpdateProjectRollOverStatus(UpdateProjectRolledOverDto updateProjectRollForwardDto);
        Task<bool> AddSuperCoachProjectRole(AddSuperCoachProjectRoleRequest req);
        Task<List<ProjectFullDetailsResponse>> GetProjectListDataByUser(ProjectListRequestDTO requestDto);
        Task<bool> AddUpdateProjectRequisitionAllocation(string pipelineCode, string? jobCode, int requisitionCountAdded, int allocationCountAdded);
    }
}
