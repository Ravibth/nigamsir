using MediatR;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.DTOs;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class UpdateResourceAllocationDetailsAllocationStatusCommand : IRequest<ResourceAllocationDetailsResponse>
    {
        public Guid guid { get; set; }
        public string AllocationStatus { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

    }
    public class UpdateResourceAllocationDetailsAllocationStatusCommandHandler : IRequestHandler<UpdateResourceAllocationDetailsAllocationStatusCommand, ResourceAllocationDetailsResponse>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public UpdateResourceAllocationDetailsAllocationStatusCommandHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }
        public async Task<ResourceAllocationDetailsResponse> Handle(UpdateResourceAllocationDetailsAllocationStatusCommand request, CancellationToken cancellationToken)
        {
            var allocationDetail = await _resourceAllocationRepository.GetAllocationByGuidHandler(request.guid);
            if (allocationDetail is null)
            {
                throw new Exception($"No Resource Allocation Details Found with '{allocationDetail?.Id}' guid");
            }
            allocationDetail.AllocationStatus = request.AllocationStatus;
            allocationDetail.ModifiedAt = request.ModifiedDate;

            UnPublishedResAllocDetails unPublishedResAllocDetails = AllocationMapper.Mapper.Map<UnPublishedResAllocDetails>(allocationDetail);

            return await _resourceAllocationRepository.UpdateAllocationStatus(unPublishedResAllocDetails);

            //Old
            //var allocationDetail = AllocationMapper.Mapper.Map<ResourceAllocationDetails>(request);
            //if (allocationDetail == null)
            //{
            //    throw new ApplicationException("Issue With The Mapper");
            //}
            //var updatedResourceAllocationDetails = await _resourceAllocationRepository.UpdateAllocationStatus(allocationDetail);
            //var result = AllocationMapper.Mapper.Map<ResourceAllocationDetailsResponse>(updatedResourceAllocationDetails);
            //return result;
        }
    }
}
