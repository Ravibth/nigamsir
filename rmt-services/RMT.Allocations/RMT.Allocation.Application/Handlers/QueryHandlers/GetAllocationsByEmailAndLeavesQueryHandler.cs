using MediatR;
using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;
using RMT.Allocation.Application.IHttpServices;
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
    public class GetAllocationsByEmailAndLeavesQuery : IRequest<List<AllocationWithLeavesAndResourceRequestorsResponse>>
    {
        public string? token { get; set; }
        public List<GetAllocationByEmployeeEmailAndLeaveDTO> EmployeeLeaves { get; set; }
    }
    public class GetAllocationsByEmailAndLeavesQueryHandler : IRequestHandler<GetAllocationsByEmailAndLeavesQuery, List<AllocationWithLeavesAndResourceRequestorsResponse>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
        public GetAllocationsByEmailAndLeavesQueryHandler(IResourceAllocationRepository resourceAllocationRepository, IProjectServiceHttpApi projectServiceHttpApi)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _projectServiceHttpApi = projectServiceHttpApi;
        }
        public async Task<List<AllocationWithLeavesAndResourceRequestorsResponse>> Handle(GetAllocationsByEmailAndLeavesQuery request, CancellationToken cancellationToken)
        {
            List<ResourceAllocationResponse> allocations = new();
            List<AllocationWithLeavesAndResourceRequestorsResponse> allocationsWithLeaves = new();
            foreach (var item in request.EmployeeLeaves)
            {
                List<ResourceAllocationResponse> allocationWithLeaves = await _resourceAllocationRepository.GetAllocationByEmailAndLeaveStartDateAndEndDate(item.EmployeeEmail, item.LeaveStartDate, item.LeaveEndDate);
                if (allocationWithLeaves.Count > 0)
                {
                    allocations.AddRange(allocationWithLeaves);
                }
                foreach (var allocation in allocationWithLeaves)
                {
                    AllocationWithLeavesAndResourceRequestorsResponse alloc = new AllocationWithLeavesAndResourceRequestorsResponse()
                    {
                        EmployeeEmail = item.EmployeeEmail,
                        LeaveStartDate = item.LeaveStartDate,
                        LeaveEndDate = item.LeaveEndDate,
                        JobCode = allocation.JobCode,
                        PipelineCode = allocation.PipelineCode
                    };
                    allocationsWithLeaves.Add(alloc);
                }
            };
            List<AllocationWithLeavesAndResourceRequestorsResponse> uniqueAllocationWithLeave = allocationsWithLeaves.GroupBy(d => new { d.JobCode, d.PipelineCode, d.EmployeeEmail, d.LeaveStartDate, d.LeaveEndDate }).Select(e => e.First()).ToList();
            List<KeyValuePair<string, string?>> PipelineCodes = uniqueAllocationWithLeave.Select(e => new KeyValuePair<string, string?>(e.PipelineCode, e.JobCode)).ToList();
            List<AllocationWithLeavesAndResourceRequestorsResponse> result = new List<AllocationWithLeavesAndResourceRequestorsResponse>();
            if(PipelineCodes != null && PipelineCodes.Count > 0)
            {
                var resourceRequestors = await _projectServiceHttpApi.GetResourceRequestorsByPipelineCodes(PipelineCodes, request.token);
                foreach (var uniqueLeaves in uniqueAllocationWithLeave)
                {
                    AllocationWithLeavesAndResourceRequestorsResponse leaveUniqueItem = new AllocationWithLeavesAndResourceRequestorsResponse()
                    {
                        EmployeeEmail = uniqueLeaves.EmployeeEmail,
                        JobCode = uniqueLeaves.JobCode,
                        LeaveEndDate = uniqueLeaves.LeaveEndDate,
                        LeaveStartDate = uniqueLeaves.LeaveStartDate,
                        PipelineCode = uniqueLeaves.PipelineCode,
                        ResourceRequestors = resourceRequestors.Where(x => x.PipelineCode.Trim().ToLower() == uniqueLeaves.PipelineCode.Trim().ToLower()).FirstOrDefault().ResourceRequestors
                    };
                    result.Add(leaveUniqueItem);
                }
            }
            return result;
        }
    }
}
