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
    public class GetAllAllocationByPipelineOrJobCodeQuery : IRequest<List<ResourceAllocationDetailsResponse>>
    {
        public string PipelineCode { get; set; }

        public string? JobCode { get; set; }
    }

    public class GetAllAllocationByPipelineOrJobCodeHandler : IRequestHandler<GetAllAllocationByPipelineOrJobCodeQuery, List<ResourceAllocationDetailsResponse>>
    {

        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public GetAllAllocationByPipelineOrJobCodeHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }

        public async Task<List<ResourceAllocationDetailsResponse>> Handle(GetAllAllocationByPipelineOrJobCodeQuery request, CancellationToken cancellationToken)
        {
            List<ResourceAllocationDetailsResponse> allocations = await _resourceAllocationRepository.GetAllAllocationByPipelineOrJobCode(request.PipelineCode, request.JobCode);
            return allocations;
        }
    }
}
