using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Domain.DTO;
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
    public class GetActiveAllocationQuery : IRequest<List<GetResourceAllocationDetailsListByCurrentUserRoleResponse>>
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? userEmail { get; set; }
        public bool? isAllocationDetailsFilterByUserRoles { get; set; } = false;
        public string? status { get; set; } = null;
        public string[]? userAppRoles { get; set; } = null;
    }
    public class GetActiveAllocationQureyHandler : IRequestHandler<GetActiveAllocationQuery, List<GetResourceAllocationDetailsListByCurrentUserRoleResponse>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
        private readonly IConfigurationHttpService _configurationHttpService;
        private readonly IIdentityUserDetailsHttpApi _identityUserDetailsHttpApi;
        public GetActiveAllocationQureyHandler(IResourceAllocationRepository resourceAllocationRepository, 
                                            IProjectServiceHttpApi projectServiceHttpApi, 
                                            IConfigurationHttpService configurationHttpService,
                                            IIdentityUserDetailsHttpApi identityUserDetailsHttpApi)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _projectServiceHttpApi = projectServiceHttpApi;
            _configurationHttpService = configurationHttpService;
            _identityUserDetailsHttpApi = identityUserDetailsHttpApi;
        }
        public async Task<List<GetResourceAllocationDetailsListByCurrentUserRoleResponse>> Handle(GetActiveAllocationQuery request, CancellationToken cancellationToken)
        {
            List<ResourceAllocationDetailsResponse> resourceAllocationDetails;
            if (!string.IsNullOrEmpty(request.status) && request.status.ToLower() == "Approved".ToLower())
            {
                resourceAllocationDetails = await _resourceAllocationRepository.GetApprovedAllocationByPipeLineCode(request.PipelineCode, request.JobCode);
            }
            else
                resourceAllocationDetails = await _resourceAllocationRepository.GetActiveAllocationByPipeLineCode(request.PipelineCode, request.JobCode);
            if (!string.IsNullOrEmpty(request.userEmail) && request.isAllocationDetailsFilterByUserRoles != null && (bool)request.isAllocationDetailsFilterByUserRoles)
            {
                return await Helper.GetResourceAllocationDetailsListByCurrentUserRole(resourceAllocationDetails, request.PipelineCode, request.JobCode, request.userEmail, _projectServiceHttpApi, _configurationHttpService, _identityUserDetailsHttpApi ,request.userAppRoles);
            }
            return resourceAllocationDetails.Cast<GetResourceAllocationDetailsListByCurrentUserRoleResponse>().ToList();
        }
    }
}
