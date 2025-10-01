using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.DTOs;
using RMT.Allocation.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetResourceAllocationByProjectCodeQuery : IRequest<List<ResourceAllocationResponse>>
    {
        public string PipelineCode { get; set; }
        public string JobCode { get; set; }

    }

    //To be moved to Allocation Service

    public class GetResourceAllocationByProjectCodeQueryHandler : IRequestHandler<GetResourceAllocationByProjectCodeQuery, List<ResourceAllocationResponse>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public GetResourceAllocationByProjectCodeQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }

        public async Task<List<ResourceAllocationResponse>> Handle(GetResourceAllocationByProjectCodeQuery request, CancellationToken cancellationToken)
        {

            //List<ResourceAllocation> result = await _resourceAllocationRepository.GetProjectsByEmployeeEmail(request.ProjectCode);
            List<ResourceAllocationResponse> result = await _resourceAllocationRepository.GetProjectsByEmployeeEmailAndPipelineCode(null, request.PipelineCode, request.JobCode, null);

            List<ResourceAllocationResponse> entitiy = AllocationMapper.Mapper.Map<List<ResourceAllocationResponse>>(result);
            if (entitiy is null)
            {
                throw new ApplicationException("Issue with mapper");
            }

            return await Task.FromResult(entitiy);

            //return await _allocationRepo.GetResourceAllocationByProjectCode(request.ProjectCode);
            //return await Task.FromResult(new List<Project>());
        }
    }
}
