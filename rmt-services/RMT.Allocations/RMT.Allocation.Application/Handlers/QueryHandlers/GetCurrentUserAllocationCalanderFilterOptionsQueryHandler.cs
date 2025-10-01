using AutoMapper.Internal;
using MediatR;
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
    public class GetCurrentUserAllocationCalanderFilterOptionsQuery : IRequest<GetCurrentUserAllocationCalanderFilterOptionsResponse>
    {
        public string UserEmail { get; set; }
    }
    public class GetCurrentUserAllocationCalanderFilterOptionsQueryHandler : IRequestHandler<GetCurrentUserAllocationCalanderFilterOptionsQuery, GetCurrentUserAllocationCalanderFilterOptionsResponse>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public GetCurrentUserAllocationCalanderFilterOptionsQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }
        public async Task<GetCurrentUserAllocationCalanderFilterOptionsResponse> Handle(GetCurrentUserAllocationCalanderFilterOptionsQuery request, CancellationToken cancellationToken)
        {
            List<ResourceAllocationDetailsResponse> currentResourceAllocationDetails = await _resourceAllocationRepository.GetUserAllocationDetailsByEmailAndDates(new List<string> { request.UserEmail }, null, null, null, false);
            GetCurrentUserAllocationCalanderFilterOptionsResponse response = new();
            response.PipelineCodes = currentResourceAllocationDetails.Where(e => string.IsNullOrEmpty(e.JobCode)).DistinctBy(e => e.PipelineCode).Select(p => new PipelineCodes
            {
                PipelineCode = p.PipelineCode,
                PipelineName = p.PipelineName,
            }).ToList();
            response.JobCodes = currentResourceAllocationDetails.Where(e => !string.IsNullOrEmpty(e.JobCode)).DistinctBy(e => e.JobCode).Select(p => new JobCodes
            {
                JobCode = p.JobCode,
                JobName = p.JobName
            }).ToList();
            return response;
        }
    }
}
