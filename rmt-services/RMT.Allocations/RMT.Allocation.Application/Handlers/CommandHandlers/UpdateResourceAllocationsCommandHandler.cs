using MediatR;
using Microsoft.Extensions.Configuration;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.DTOs.CommonResourceAllocationDTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Application.Utils;
using RMT.Allocation.Domain;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.DTO.Response;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.DTOs;
using RMT.Allocation.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Net.Http;
using Constants_Infra = RMT.Allocation.Infrastructure.Constants;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class UpdateResourceAllocationsCommand : IRequest<List<ResourceAllocationDetailsResponse>>
    {
        public ResourceAllocationDetailsResponse[] resourceAllocationDTO { get; set; }
        public string UserToken { get; set; }
        public UserDecorator user { get; set; }
        public bool isDraft { get; set; }
       
    }
    public class UpdateResourceAllocationsCommandHandler : IRequestHandler<UpdateResourceAllocationsCommand, List<ResourceAllocationDetailsResponse>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
        private readonly IRequisitionRepository _requisitionRepository;
        private readonly IWCGTMasterHttpApi _wCGTMasterHttpApi;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IWorkflowHttpApi _workflowHttpApi;
        private readonly IIdentityUserDetailsHttpApi _identityHttpServices;
        public UpdateResourceAllocationsCommandHandler(IResourceAllocationRepository resourceAllocationRepository, IProjectServiceHttpApi projectServiceHttpApi, 
            IRequisitionRepository requisitionRepository, IWCGTMasterHttpApi wCGTMasterHttpApi, IConfiguration configuration, 
            HttpClient httpClient, IWorkflowHttpApi workflowHttpApi, IIdentityUserDetailsHttpApi identityHttpServices)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _projectServiceHttpApi = projectServiceHttpApi;
            _requisitionRepository = requisitionRepository;
            _wCGTMasterHttpApi = wCGTMasterHttpApi;
            _configuration = configuration;
            _httpClient = httpClient;
            _workflowHttpApi = workflowHttpApi;
            _identityHttpServices = identityHttpServices;
        }
        public async Task<List<ResourceAllocationDetailsResponse>> Handle(UpdateResourceAllocationsCommand request, CancellationToken cancellationToken)
        {
            if (request != null && request.resourceAllocationDTO != null && request.resourceAllocationDTO.Length > 0)
            {

                List<TerminateWorkflowDTO> allWorkflowToTerminate = new();
                List<ResourceAllocationDetailsResponse> pipelineAllocations = await _resourceAllocationRepository.GetActiveAllocationByPipeLineCode(request.resourceAllocationDTO[0].PipelineCode, request.resourceAllocationDTO[0].JobCode);

                if (pipelineAllocations != null && pipelineAllocations.Count > 0)
                {
                    foreach (var radAllocation in pipelineAllocations)
                    {
                        allWorkflowToTerminate.Add(new TerminateWorkflowDTO()
                        {
                            ItemId = radAllocation.Id.ToString(),
                            WorkflowStatus = RMT.Allocation.Infrastructure.Constants
                        .WorkflowStatus.WORKFLOW_TERMINATED_BY_JOBCODE_MOVE
                        });
                    }
                    UserInfoDTO userInfo = await _identityHttpServices.GetUserInfo(request.user.email);
                    await _workflowHttpApi.TerminateWorkflow(allWorkflowToTerminate, request.UserToken, userInfo);
                }
                var response = await _resourceAllocationRepository.UpdateResourcesAllocations(request.resourceAllocationDTO);
                return response;
            }
            else
            {
                return null;
            }
        }
    }
}

