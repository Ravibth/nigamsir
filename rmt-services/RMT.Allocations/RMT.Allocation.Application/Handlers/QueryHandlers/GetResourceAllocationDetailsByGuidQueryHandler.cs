using MediatR;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.DTOs;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetResourceAllocationDetailsByGuidQuery : IRequest<ResourceAllocationDetailsResponse>
    {
        public Guid guid { get; set; }
    }
    public class GetResourceAllocationDetailsByGuidQueryHandler : IRequestHandler<GetResourceAllocationDetailsByGuidQuery, ResourceAllocationDetailsResponse>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public GetResourceAllocationDetailsByGuidQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }
        public async Task<ResourceAllocationDetailsResponse> Handle(GetResourceAllocationDetailsByGuidQuery request, CancellationToken cancellationToken)
        {
            var resourceAllocationDetails = await _resourceAllocationRepository.GetAllocationByGuidHandler(request.guid);
            var resourceAllocationDetailsResp = AllocationMapper.Mapper.Map<ResourceAllocationDetailsResponse>(resourceAllocationDetails);
            if (resourceAllocationDetailsResp is null)
            {
                throw new ApplicationException("Issue with the mapper");
            }
            if (resourceAllocationDetails != null && resourceAllocationDetails.Requisition != null && resourceAllocationDetailsResp != null)
            {
                resourceAllocationDetailsResp.Designation = resourceAllocationDetails.Requisition.Designation;
                //resourceAllocationDetailsResp.Competency = resourceAllocationDetails.Requisition.Competency;
                resourceAllocationDetailsResp.Grade = resourceAllocationDetails.Requisition.Grade;
            }

            return resourceAllocationDetailsResp;
        }
    }
}
