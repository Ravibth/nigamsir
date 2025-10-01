using MediatR;
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
    public class GetAllocationByJobCodeQuery : IRequest<List<ResourceAllocationDetailsResponse>>
    {
        public List<string> JobCodes { get; set; }
    }

    public class GetAllocationByJobCodeHandler : IRequestHandler<GetAllocationByJobCodeQuery, List<ResourceAllocationDetailsResponse>>
    {

        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public GetAllocationByJobCodeHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }

        public async Task<List<ResourceAllocationDetailsResponse>> Handle(GetAllocationByJobCodeQuery request, CancellationToken cancellationToken)
        {
            List<ResourceAllocationDetailsResponse> allocations = await _resourceAllocationRepository.GetAllocationByJobCodeHandler(request.JobCodes);
            return allocations;
        }
    }
}
