using Gateway.API.Dtos;
using Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper.DTOs;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Gateway.API.Helpers.IHttpServices
{
    public interface IAllocationHttpService
    {
        Task<HttpStatusCode> UpdateAllocationStatus(UpdateAllocationRequestDTO allocationStatus);
        Task<UpdateListOfAllocationStatusInResourceAllocationDetailsResponse> UpdateListOfAllocationStatus(List<UpdateAllocationRequestDTO> allocationStatus);
        Task<bool> RemoveAllDraftAllocationsAfterUserIsDeactivated(List<string> emails);
    }
}
