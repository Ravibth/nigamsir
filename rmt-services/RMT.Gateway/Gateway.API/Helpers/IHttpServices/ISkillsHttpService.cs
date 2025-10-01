using Gateway.API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.API.Helpers.IHttpServices
{
    public interface ISkillsHttpService
    {
        Task<bool> UpdateUserSkillStatusAfterWorkflow(List<UpdateUserSkillStatusAfterWorkflowRequestDTO> requests);

    }
}
