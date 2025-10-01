using ServiceLayer.DTOs;
using ServiceLayer.Services.AllocationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.AllocationService
{
    public interface IAllocationService
    {
        Task<ResourceAllocationDetailsResponse> GetResourceAllocationDetailsByGuid(string guid, string token);
        Task<AllocationRolloverResponseDTO> RolloverAllocationByPipelineCode(WcgtJobDTO wcgtJob, Project rmsProjectJob, string token);
        Task UpdateDesignation(DesignationUpdateDTO request, string token);
        Task<List<AllocationWithLeavesAndResourceDTO>> GetAllocationInformationByLeaves(string empEmail, DateTime startDate, DateTime endDate, string token);
        Task<List<ResourceAllocationResponseDTO>> GetActiveAllocationByEmail(string empEmail, string token);
    }
}
