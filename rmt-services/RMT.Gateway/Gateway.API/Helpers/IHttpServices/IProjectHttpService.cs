using Gateway.API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.API.Helpers.IHttpServices
{
    public interface IProjectHttpService
    {
        Task<ProjectInfoDTO> GetProjectDetailsByPipelineCode(string pipelineCode, string jobCode);

        Task<bool> AddUpdateProjectRequisitionAllocation(string pipelineCode, string? jobCode, int requisitionCountAdded, int allocationCountAdded);

        Task<List<RoleEmailsByPipelineCodeResponse>> GetRolesEmailByPipelineCodesAndRoles(List<PipelineCodeAndRolesDto> pipelineCodeAndRolesDto);

        Task<List<ProjectRolesResponseDTO>> GetAllProjectRolesByCodes(PipelineCodeAndRolesDto pipelineCodeAndRolesDto);

    }
}
