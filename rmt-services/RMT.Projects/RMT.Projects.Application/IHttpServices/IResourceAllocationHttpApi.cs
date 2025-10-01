using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.HttpServices.DTOs;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.IHttpServices
{
    public interface IResourceAllocationHttpApi
    {
        Task<List<SuspendAllocationResponse>> SuspendAllocationHttpApiQuery(SuspendAllocationCommand request);

        Task<List<ProjectAllocatedHoursRatioDto>> GetAllocatedHoursRatioByPipelineCode(List<KeyValuePair<string, string>> pipelineCodes);
        Task<List<GetAllActiveRequisitionDTO>> GetAllActiveRequisitionsbyPipelineCodeHttpApiQuery(List<KeyValuePair<string, string>> request);
        Task<List<GetAllActiveRequisitionDTO>> GetAllRequisitionByProjectCodeForProjectDetailsQuery(List<KeyValuePair<string, string>> request);
        Task<List<ResourceAllocationDetailsResponse>> GetActivePublishedAllocationByPipeLineCode(List<KeyValuePair<string, string>> request);
        Task<Boolean> UpdateAllocationbyPiplelineCodeHttpApiQuery(string pipelineCode, string jobCode, string new_pipelineCode, string new_JobCode, string new_JobName, string updatedBy);
        Task<List<AllocationDayResourceGroup>> PublishResouceAllocationDayGroupedHttpApiQuery();

    }
}