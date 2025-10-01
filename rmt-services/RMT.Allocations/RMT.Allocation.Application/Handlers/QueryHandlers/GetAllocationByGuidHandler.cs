using MediatR;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetAllocationByGuidQuery : IRequest<ResourceAllocationDetailsResponse>
    {
        public Guid guid { get; set; }
    }

    public class GetAllocationByGuidHandler : IRequestHandler<GetAllocationByGuidQuery, ResourceAllocationDetailsResponse>
    {

        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public GetAllocationByGuidHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }

        public async Task<ResourceAllocationDetailsResponse> Handle(GetAllocationByGuidQuery request, CancellationToken cancellationToken)
        {
            ResourceAllocationDetailsResponse allocations = await _resourceAllocationRepository.GetAllocationByGuidHandler(request.guid);
           var allocationResult =  AllocationMapper.Mapper.Map<ResourceAllocationDetailsResponse>(allocations);

            return allocationResult;
        }
    }
}
