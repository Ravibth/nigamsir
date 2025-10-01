using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Domain;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure;
using RMT.Allocation.Infrastructure.Data;
using RMT.Allocation.Infrastructure.DTOs;
using RMT.Allocation.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static RMT.Allocation.Domain.ConstantsDomain;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class ReleaseResourceCommand : IRequest<ResourceAllocationDetailsResponse>
    {
        public Guid guid { get; set; }
        public string? ModifiedBy { get; set; }
        public string? token { get; set; }
    }
    public class ReleaseResourceCommandHandler : IRequestHandler<ReleaseResourceCommand, ResourceAllocationDetailsResponse>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
        private readonly IAzureHttpService _azureHttpService;

        public ReleaseResourceCommandHandler(IResourceAllocationRepository resourceAllocationRepository, IProjectServiceHttpApi projectServiceHttpApi, IAzureHttpService azureHttpService)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _projectServiceHttpApi = projectServiceHttpApi;
            _azureHttpService = azureHttpService;
        }

        public async Task<ResourceAllocationDetailsResponse> Handle(ReleaseResourceCommand request, CancellationToken cancellationToken)
        {
            var allocationDetails = await _resourceAllocationRepository.GetAllocationByGuidHandler(request.guid);
            var response = await _resourceAllocationRepository.ReleaseResourceActiveAllocation(request.guid, request.ModifiedBy);
            List<AddProjectUserRole> projectRoles = new();
            var resourceAllocationDetail = await _resourceAllocationRepository.GetAllocationByGuidHandler(request.guid, false);
            if (resourceAllocationDetail.Id != null && !resourceAllocationDetail.IsActive)
            {
                projectRoles.Add(new AddProjectUserRole
                {
                    User = resourceAllocationDetail.EmpEmail,
                    UserName = resourceAllocationDetail.EmpName,
                    Role = Constants.UserRoles.Employee //Utils.Constants.Employee
                });
                await Utils.Helper.RemoveProjectRoles(projectRoles, 0, resourceAllocationDetail.PipelineCode, resourceAllocationDetail.JobCode, _projectServiceHttpApi);
            }
            if (response != null)
            {
                List<RefreshProjectCompetencyPayload> competencyPayloads = new();
                competencyPayloads.Add(new RefreshProjectCompetencyPayload
                {
                    PipelineCode = allocationDetails.PipelineCode,
                    JobCode = string.IsNullOrEmpty(allocationDetails.JobCode) ? null : allocationDetails.JobCode,
                });
                RefreshPayload payload = new()
                {
                    token = string.IsNullOrEmpty(request.token) ? string.Empty : request.token,
                    action = ServiceBusActions.REFRESH_PROJECT_COMPETENCY,
                    payload = JsonSerializer.Serialize(competencyPayloads)
                };
                await _azureHttpService.PublishMessageOnAzureServiceBus(payload, "project");

                RefreshPayload payloadForBudget = new()
                {
                    token = string.IsNullOrEmpty(request.token) ? string.Empty : request.token,
                    action = ServiceBusActions.REFRESH_PROJECT_BUDGET_STATUS,
                    payload = JsonSerializer.Serialize(competencyPayloads)
                };
                await _azureHttpService.PublishMessageOnAzureServiceBus(payloadForBudget, "budgetstatus");

                int allocationcount = await _resourceAllocationRepository.GetAllocationCount(allocationDetails.PipelineCode, allocationDetails.JobCode);
                int requicount = await _resourceAllocationRepository.GetRequisitionCount(allocationDetails.PipelineCode, allocationDetails.JobCode);
                await _projectServiceHttpApi.AddUpdateProjectRequisitionAllocation(allocationDetails.PipelineCode, allocationDetails.JobCode, requicount, allocationcount);

            }            
            return resourceAllocationDetail;
        }
    }
}
