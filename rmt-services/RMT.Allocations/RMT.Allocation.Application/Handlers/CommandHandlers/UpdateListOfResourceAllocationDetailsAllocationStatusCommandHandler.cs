using MediatR;
using Microsoft.Extensions.Configuration;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.HttpServices.HolidayService;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Utils;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.DTO.Response;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure;
using RMT.Allocation.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static RMT.Allocation.Domain.ConstantsDomain;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class ResouceAllocationDetailsStatusUpdate
    {
        public Guid guid { get; set; }
        public string AllocationStatus { get; set; }
        public string? WorkflowModule { get; set; }
        public string? WorkflowSubModule { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string token { get; set; }
    }
    public class UpdateListOfResourceAllocationDetailsAllocationStatusCommand : IRequest<UpdateListOfAllocationDetailsStatusResponse>
    {
        public List<ResouceAllocationDetailsStatusUpdate> UpdateAllocationStatusList { get; set; }
    }
    public class UpdateListOfResourceAllocationDetailsAllocationStatusCommandHandler : IRequestHandler<UpdateListOfResourceAllocationDetailsAllocationStatusCommand, UpdateListOfAllocationDetailsStatusResponse>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IAzureHttpService _azureHttpService;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IWCGTMasterHttpApi _wCGTMasterHttpApi;
        private readonly IHolidayHttpService _holidayHttpService;
        private readonly IIdentityUserDetailsHttpApi _identityUserDetailsHttpApi;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;


        public UpdateListOfResourceAllocationDetailsAllocationStatusCommandHandler(
            IResourceAllocationRepository resourceAllocationRepository
            , IAzureHttpService azureHttpService
            , IConfiguration configuration
            , HttpClient httpClient
            , IWCGTMasterHttpApi wCGTMasterHttpApi
            , IHolidayHttpService holidayHttpService
            , IIdentityUserDetailsHttpApi identityUserDetailsHttpApi
            , IProjectServiceHttpApi projectServiceHttpApi

        )
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _azureHttpService = azureHttpService;
            _configuration = configuration;
            _httpClient = httpClient;
            _wCGTMasterHttpApi = wCGTMasterHttpApi;
            _holidayHttpService = holidayHttpService;
            _identityUserDetailsHttpApi = identityUserDetailsHttpApi;
            _projectServiceHttpApi = projectServiceHttpApi;
        }
        public async Task<UpdateListOfAllocationDetailsStatusResponse> Handle(UpdateListOfResourceAllocationDetailsAllocationStatusCommand request, CancellationToken cancellationToken)
        {
            List<UpdateListOfAllocationDetailsStatusRequest> req = AllocationMapper.Mapper.Map<List<UpdateListOfAllocationDetailsStatusRequest>>(request.UpdateAllocationStatusList);
            if (req is null)
            {
                throw new ApplicationException("Issue With the mapper");
            }


            var allocationDetails = await _resourceAllocationRepository.GetListOfAllocationByGuidHandler(req.Select(m => m.guid).ToList());

            var unpubDetails = allocationDetails.Where(m => m.Type == AllocationType.UnPUBLISHED).ToList();

            var uniqueEmailIds = unpubDetails.Select(m => m.EmpEmail).Distinct().ToList();

            var holidayResult = await _holidayHttpService.GetLocationSpecificHolidays(uniqueEmailIds, null, null);

            Dictionary<string, string> EmployeeEmailLocation = holidayResult.EmailLocationCollection;
            var leaveResults = new GetHolidayLeaveResignedAbsconded();
            if (unpubDetails.Count > 0)
            {
                DateTime start_date_min = unpubDetails.MinBy(e => e.StartDate).StartDate.ToDateTime(TimeOnly.MinValue);
                DateTime end_date_max = unpubDetails.MaxBy(e => e.EndDate).EndDate.ToDateTime(TimeOnly.MinValue);
                leaveResults = await Helper.GetHolidayLeaveResignedAbscondedByEmailIds(_configuration, uniqueEmailIds, _httpClient, _wCGTMasterHttpApi, start_date_min, end_date_max);
            }
            leaveResults.HolidayResponseTask = holidayResult.HolidayList;
            //1. check if task is created for SC
            //2. call identity to get SC
            //3. take SC and add to project 
            var response = await _resourceAllocationRepository.UpdateListOfAllocationDetailsStatus(req, leaveResults);
            if(response.EmployeeListForSuperCoach.Count > 0)
            {
                var usersInfo = await _identityUserDetailsHttpApi.GetUsersByEmailDataHttpApiQuery(response.EmployeeListForSuperCoach);
                List<SuperCoachInformation> superCoachInformation = new();
                var superCoachInfo = usersInfo.Select(e => new SuperCoachInformation()
                {
                    SuperCoachEmailId = e.supercoach_name,
                    SuperCoachName = e.supercoach_name,
                    NewAllocationSuperCoachDelegateEmail = e != null && e.delegate_details != null ? e.delegate_details.allocation_delegate_email : null,
                    NewAllocationSuperCoachDelegateName = e != null && e.delegate_details != null ? e.delegate_details.allocation_delegate_name : null,
                    
                }).ToList();
                AddSuperCoachProjectRoleRequest superCoachToAdd = new() 
                {
                    PipelineCode = response.PipelineCode,
                    JobCode = string.IsNullOrEmpty(response.JobCode) ? null : response.JobCode,
                    SuperCoachInformation = superCoachInfo
                };
                //send request to project
                await _projectServiceHttpApi.AddSuperCoachProjectRole(superCoachToAdd);

            }
            if (response.IsProjectCompetencyRefresh != null && response.IsProjectCompetencyRefresh == true)
            {
                List<RefreshProjectCompetencyPayload> payload = new();
                payload.Add(new RefreshProjectCompetencyPayload
                {
                    PipelineCode = response.PipelineCode,
                    JobCode = response.JobCode,
                });
                RefreshPayload refreshPayload = new()
                {
                    token = request.UpdateAllocationStatusList.FirstOrDefault() != null ? request.UpdateAllocationStatusList.FirstOrDefault().token : string.Empty,
                    action = ServiceBusActions.REFRESH_PROJECT_COMPETENCY,
                    payload = JsonSerializer.Serialize(payload)
                };
                await _azureHttpService.PublishMessageOnAzureServiceBus(refreshPayload, "project");

                RefreshPayload payloadForBudget = new()
                {
                    token = request.UpdateAllocationStatusList.FirstOrDefault() != null ? request.UpdateAllocationStatusList.FirstOrDefault().token : string.Empty,
                    action = ServiceBusActions.REFRESH_PROJECT_BUDGET_STATUS,
                    payload = JsonSerializer.Serialize(payload)
                };
                await _azureHttpService.PublishMessageOnAzureServiceBus(payloadForBudget, "budgetstatus");
            }

            int allocationcount = await _resourceAllocationRepository.GetAllocationCount(response.PipelineCode, response.JobCode);
            int requicount = await _resourceAllocationRepository.GetRequisitionCount(response.PipelineCode, response.JobCode);
            await _projectServiceHttpApi.AddUpdateProjectRequisitionAllocation(response.PipelineCode, response.JobCode, requicount, allocationcount);
            
            return response;
        }
    }
}
