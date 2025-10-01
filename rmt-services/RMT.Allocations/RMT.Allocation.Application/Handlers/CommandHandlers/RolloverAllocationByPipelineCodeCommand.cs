using MediatR;
using Newtonsoft.Json;
using RMT.Allocation.Application.Handlers.QueryHandlers;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Utils;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Domain;
using RMT.Allocation.Infrastructure;
using Constants = RMT.Allocation.Infrastructure.Constants;
using static RMT.Allocation.Domain.ConstantsDomain;
using RMT.Allocation.Application.Responses;
using static RMT.Allocation.Infrastructure.Constants;
using RMT.Allocation.Infrastructure.DTOs;
using RMT.Allocation.Application.DTOs.CommonResourceAllocationDTOs;
using RMT.Allocation.Application.HttpServices.HolidayService;
using RMT.Allocation.Infrastructure.Data;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class RolloverAllocationByPipelineCodeCommand : IRequest<AllocationRolloverResponseDTO>
    {
        public List<ProjectRolloverRequestDTO> ProjectRolloverRequestData { get; set; }
        public string UserEmail { get; set; }
        public string UserToken { get; set; }
        public string Message { get; set; }
        public UserDecorator user { get; set; }
    }

    public class RolloverAllocationByPipelineCodeCommandHandler : IRequestHandler<RolloverAllocationByPipelineCodeCommand, AllocationRolloverResponseDTO>
    {
        private readonly IRequisitionRepository _requisitionRepository;
        private readonly IResourceAllocationRepository _allocationRepository;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IConfigurationHttpService _configurationHttpService;
        private readonly IMediator _mediator;

        private readonly IResourceAllocationRepository _resourceAllocationRepository;

        private readonly IWCGTMasterHttpApi _wCGTMasterHttpApi;
        private readonly IIdentityUserDetailsHttpApi _identityUserDetailsHttpApi;
        private readonly IHolidayHttpService _holidayHttpService;

        public RolloverAllocationByPipelineCodeCommandHandler(
            IRequisitionRepository requisitionRepository
            , IResourceAllocationRepository resourceAllocationRepository
            , IResourceAllocationRepository allocationRepository
            , IProjectServiceHttpApi projectServiceHttpApi
            , IConfiguration configuration
            , HttpClient httpClient
            , IConfigurationHttpService configurationHttpService
            , IMediator mediator
            , IWCGTMasterHttpApi wCGTMasterHttpApi
            , IIdentityUserDetailsHttpApi identityUserDetailsHttpApi
            , IHolidayHttpService holidayHttpService
            )
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _requisitionRepository = requisitionRepository;
            _allocationRepository = allocationRepository;
            _projectServiceHttpApi = projectServiceHttpApi;
            _configuration = configuration;
            _httpClient = httpClient;
            _configurationHttpService = configurationHttpService;
            _mediator = mediator;
            _wCGTMasterHttpApi = wCGTMasterHttpApi;
            _identityUserDetailsHttpApi = identityUserDetailsHttpApi;
            _holidayHttpService = holidayHttpService;

        }

        public async Task<AllocationRolloverResponseDTO> Handle(RolloverAllocationByPipelineCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<string> allocationConfirmedStatuses = new()
                {
                    WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_EMPLOYEE,
                    WorkflowStatus.EMPLOYEE_ALLOCATION_WITHDRAWL_BY_EMPLOYEE,
                    WorkflowStatus.EMPLOYEE_ALLOCATION_SUPERCOACH_ACCEPTED_RESOURCE_REQUESTOR_REJECTION,
                    WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_REVIEWER,
                    WorkflowStatus.EMPLOYEE_ALLOCATION_ACCEPTED_BY_SUPERCOACH
                };
                int rollForwardDays;

                AllocationRolloverResponseDTO allocationRolloverResponse = new AllocationRolloverResponseDTO();
                List<NotificationPayloadDTO> allWorkflowToStart = new();
                List<TerminateWorkflowDTO> allWorkflowToTerminate = new();
                var commanResourceAllocation = new CommonResourceAllocationCommandHandler
                    (_resourceAllocationRepository, _projectServiceHttpApi, _requisitionRepository, _wCGTMasterHttpApi, _configuration, _httpClient, _identityUserDetailsHttpApi, _holidayHttpService);
                foreach (var requestData in request.ProjectRolloverRequestData)
                {
                    try
                    {
                        ProjectDTO projectDetail = await _projectServiceHttpApi.GetProjectDetailsByCode(requestData.PipelineCode, requestData.JobCode);
                        if (projectDetail != null && projectDetail.PipelineStatus != PipelineStatuses.Suspended && requestData.PipelineStartDate > DateTime.UtcNow.Date
                            && requestData.PipelineOldStartDate.Date < requestData.PipelineStartDate)// 
                        {
                            //TODO 0209 by jay expertise is removed and BU value is passed for now, need to manage the same on configuration endpoint also
                            string expertiesName = projectDetail.bu + "|" + projectDetail.Offerings;
                            //roll-forward if new start date is greater than todays date

                            //Project is eligible for rollover based on its status and dates etc, now process it

                            rollForwardDays = (requestData.PipelineStartDate.Date - requestData.PipelineOldStartDate.Date).Days;
                            //if start date in greator than old start date then rollforward

                            //Get all Allocation for pipelinecode
                            List<ResourceAllocationDetailsResponse> pipelineAllocations = await _allocationRepository.GetActiveAllocationByPipeLineCode(requestData.PipelineCode, requestData.JobCode);

                            if (pipelineAllocations != null)
                            {

                                requestData.ProjectAllocations = new List<AllocationRolloverUserResponseDTO>();

                                foreach (var radAllocation in pipelineAllocations)
                                {
                                    string radAllocationStatusPreValue = radAllocation.AllocationStatus;

                                    AllocationRolloverUserResponseDTO outProjectAllocations = new AllocationRolloverUserResponseDTO();

                                    outProjectAllocations.Id = radAllocation.Id;
                                    outProjectAllocations.RequisitionId = radAllocation.RequisitionId;
                                    outProjectAllocations.UserEmail = radAllocation.EmpEmail;
                                    outProjectAllocations.UserName = radAllocation.EmpName;
                                    outProjectAllocations.OldAllocationStartDate = radAllocation.StartDate;
                                    outProjectAllocations.OldAllocationEndDate = radAllocation.EndDate;
                                    outProjectAllocations.RollOverAllocaionStatus = RollOverAllocationStatuses.None;

                                    //process each rad allocation
                                    try
                                    {

                                        var oldStartDate = new DateOnly(radAllocation.StartDate.Year, radAllocation.StartDate.Month, radAllocation.StartDate.Day);
                                        var oldEndDate = new DateOnly(radAllocation.EndDate.Year, radAllocation.EndDate.Month, radAllocation.EndDate.Day);

                                        bool isAllSlotsAvailable = await this.CheckIfUserIsAvailableOnNewDates(radAllocation, rollForwardDays);
                                        requestData.Message += " " + radAllocation.Id + "-Availability-" + isAllSlotsAvailable;

                                        //user all slots available then process
                                        if (isAllSlotsAvailable == true)
                                        {

                                            #region Available-Allocation-AutoForward

                                            List<ResourceAllocationDetailsResponse> newResourceAllocation = null;
                                            if (radAllocationStatusPreValue != AllocationStatuses.DRAFT)
                                            {
                                                //User is available on new dates 
                                                foreach (var ra in radAllocation.ResourceAllocations)
                                                {
                                                    ra.StartDate = ra.StartDate.AddDays(rollForwardDays);
                                                    ra.EndDate = ra.EndDate.AddDays(rollForwardDays);
                                                    //ra.AllocationStatus = RequisitionStatuses.PENDING_APPROVAL;
                                                }

                                                radAllocation.StartDate = radAllocation.StartDate.AddDays(rollForwardDays);
                                                radAllocation.EndDate = radAllocation.EndDate.AddDays(rollForwardDays);
                                                radAllocation.AllocationStatus = AllocationStatuses.PENDING_APPROVAL;

                                                //Remove the existing allocation
                                                if (radAllocation.Type == AllocationType.PUBLISHED)
                                                {
                                                    var prevDetails = await _allocationRepository.GetPublishedResAllocDetailsById(radAllocation.Id);
                                                    if (prevDetails != null)
                                                    {
                                                        await _allocationRepository.DeletePublishedAllocationByDetails(prevDetails);
                                                    }
                                                }

                                                //update the rad and ra slots for roll-forward
                                                newResourceAllocation = await UpdateAllocationByRollOver(radAllocation, projectDetail, request.user, commanResourceAllocation, cancellationToken);

                                                ////update the requisition also for roll-forward 
                                                //var req = await _requisitionRepository.RollOverRequisition(radAllocation.RequisitionId, rollForwardDays);
                                            }

                                            #endregion Available-Allocation-AutoForward


                                            #region Available-Allocation-Worklfow-Logic

                                            // check for workflow termination for by guid
                                            if (allocationConfirmedStatuses.Any(ac => string.Compare(ac, radAllocationStatusPreValue, true) == 0)
                                                && newResourceAllocation != null && newResourceAllocation.Count > 0)
                                            {
                                                //current workflow completed, initiate a new workflow
                                                requestData.Message += " " + radAllocation.Id + " Current Workflow was Completed and new workflow will be started.";

                                                outProjectAllocations.OldAllocationStartDate = oldStartDate;
                                                outProjectAllocations.OldAllocationEndDate = oldEndDate;
                                                outProjectAllocations.RollOverAllocaionStatus = RollOverAllocationStatuses.OldCompletedNewStarted;


                                                //ResourceAllocated same as radAllocation
                                                var singleAllocationWorkflow = await CREATE_USER_ALLOCATION_WORKFLOW_BY_ROLLOVER_ALLOCATION(newResourceAllocation.First(), expertiesName, request.UserToken);
                                                requestData.Message += " WF Count" + singleAllocationWorkflow.Count;

                                                allWorkflowToStart.AddRange(singleAllocationWorkflow);

                                            }
                                            else if (allocationConfirmedStatuses.Any(ac => string.Compare(ac, radAllocationStatusPreValue, true) == 0) == false && radAllocationStatusPreValue != AllocationStatuses.DRAFT
                                                && newResourceAllocation != null && newResourceAllocation.Count > 0)
                                            {
                                                //current workflow inprogress, terminate and initiate a new workflow
                                                requestData.Message += " " + radAllocation.Id + " In Progress Workflow Terminated and new workflow will be started.";

                                                outProjectAllocations.OldAllocationStartDate = oldStartDate;
                                                outProjectAllocations.OldAllocationEndDate = oldEndDate;
                                                outProjectAllocations.RollOverAllocaionStatus = RollOverAllocationStatuses.OldInProgressNewStarted;

                                                allWorkflowToTerminate.Add(
                                                    new TerminateWorkflowDTO() { ItemId = radAllocation.Id.ToString(), WorkflowStatus = Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_TERMINTION_DUE_TO_PROJECT_ROLL_FORWARD }
                                                    );

                                                var singleAllocationWorkflow = await CREATE_USER_ALLOCATION_WORKFLOW_BY_ROLLOVER_ALLOCATION(newResourceAllocation.First(), expertiesName, request.UserToken);
                                                requestData.Message += " WF Count" + singleAllocationWorkflow.Count;

                                                allWorkflowToStart.AddRange(singleAllocationWorkflow);

                                            }
                                            else if (radAllocationStatusPreValue == AllocationStatuses.DRAFT)
                                            {
                                                //no action needed
                                                requestData.Message += " " + radAllocation.Id + " Draft Allocation, No action needed.";

                                                outProjectAllocations.OldAllocationStartDate = oldStartDate;
                                                outProjectAllocations.OldAllocationEndDate = oldEndDate;
                                                outProjectAllocations.RollOverAllocaionStatus = RollOverAllocationStatuses.OldDraftNoAction;

                                            }
                                            else
                                            {
                                                //no action needed 
                                            }

                                            #endregion Available-Allocation-Worklfow-Logic

                                        }
                                        else
                                        {
                                            //user is not available on new dates

                                            #region NotAvailable-Allocation-Worklfow-Logic

                                            // check for workflow termination for by guid
                                            if (allocationConfirmedStatuses.Any(ac => string.Compare(ac, radAllocationStatusPreValue, true) == 0))
                                            {
                                                // current workflow completed so change the allocation as draft

                                                //roll-forward the child timeslots of allocations
                                                foreach (var ra in radAllocation.ResourceAllocations)
                                                {
                                                    ra.StartDate = ra.StartDate.AddDays(rollForwardDays);
                                                    ra.EndDate = ra.EndDate.AddDays(rollForwardDays);
                                                    //ra.AllocationStatus = RequisitionStatuses.DRAFT;
                                                }

                                                radAllocation.StartDate = radAllocation.StartDate.AddDays(rollForwardDays);
                                                radAllocation.EndDate = radAllocation.EndDate.AddDays(rollForwardDays);
                                                radAllocation.AllocationStatus = AllocationStatuses.DRAFT;

                                                //var ResourceAllocated = AllocationMapper.Mapper.Map<ResourceAllocationDetails>(radAllocation);
                                                //if (ResourceAllocated is null)
                                                //{
                                                //    throw new ApplicationException("Issue with mapper");
                                                //}
                                                //var newResourceAllocation = await _allocationRepository.UpdateAsync(radAllocation);

                                                //Remove the existing allocation
                                                if (radAllocation.Type == AllocationType.PUBLISHED)
                                                {
                                                    var prevDetails = await _allocationRepository.GetPublishedResAllocDetailsById(radAllocation.Id);
                                                    if (prevDetails != null)
                                                    {
                                                        await _allocationRepository.DeletePublishedAllocationByDetails(prevDetails);
                                                    }
                                                }

                                                var newResourceAllocation = await UpdateAllocationByRollOver(radAllocation, projectDetail, request.user, commanResourceAllocation, cancellationToken);

                                                //update requisition for new roll-forward dates
                                                //var req = await _requisitionRepository.RollOverRequisition(radAllocation.RequisitionId, rollForwardDays);

                                                //current workflow completed, no new workflow needed
                                                requestData.Message += " " + radAllocation.Id + " Current Workflow was Completed and new allocation will be updated as draft.";

                                                outProjectAllocations.OldAllocationStartDate = oldStartDate;
                                                outProjectAllocations.OldAllocationEndDate = oldEndDate;
                                                outProjectAllocations.RollOverAllocaionStatus = RollOverAllocationStatuses.OldCompletedNewDraft;
                                            }
                                            else if (allocationConfirmedStatuses.Any(ac => string.Compare(ac, radAllocationStatusPreValue, true) == 0) == false && radAllocationStatusPreValue != AllocationStatuses.DRAFT)
                                            {
                                                // current workflow completed so release the allocation and terminate workflow

                                                //release resource - isActive marked false
                                                //update requisition 

                                                var resourceReleasedObj = await _allocationRepository.ReleaseResourceByGuid(radAllocation.Id, radAllocation.ModifiedBy);

                                                //current workflow inprogress, terminate and no new workflow needed
                                                requestData.Message += " " + radAllocation.Id + " Resource release and Inprogress Workflow will be Terminated.";

                                                outProjectAllocations.OldAllocationStartDate = oldStartDate;
                                                outProjectAllocations.OldAllocationEndDate = oldEndDate;
                                                outProjectAllocations.RollOverAllocaionStatus = RollOverAllocationStatuses.OldInProgressNewReleased;

                                                allWorkflowToTerminate.Add(
                                                    new TerminateWorkflowDTO() { ItemId = radAllocation.Id.ToString(), WorkflowStatus = Constants.WorkflowStatus.EMPLOYEE_ALLOCATION_TERMINTION_DUE_TO_PROJECT_ROLL_FORWARD }
                                                    );

                                            }
                                            else if (radAllocationStatusPreValue == AllocationStatuses.DRAFT)
                                            {
                                                //no action needed
                                                requestData.Message += " " + radAllocation.Id + " Draft Allocation, No action needed.";

                                                outProjectAllocations.OldAllocationStartDate = oldStartDate;
                                                outProjectAllocations.OldAllocationEndDate = oldEndDate;
                                                outProjectAllocations.RollOverAllocaionStatus = RollOverAllocationStatuses.OldDraftNoAction;

                                            }
                                            else
                                            {
                                                //no action needed 
                                            }

                                            #endregion NotAvailable-Allocation-Worklfow-Logic

                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        requestData.Message += " Exception - " + ex.Message;
                                        //handle individual record error
                                    }

                                    requestData.ProjectAllocations.Add(outProjectAllocations);
                                }

                                //Update Project EndDate to new roll forward end date only 

                                UpdateProjectRolledOverDto _UpdateProjectRolledOverDto = new()
                                {
                                    PipelineCode = requestData.PipelineCode,
                                    JobCode = requestData.JobCode,
                                    IsRollover = false,
                                    RolloverDays = rollForwardDays,//rollForwardDays
                                    ModifiedBy = request.UserEmail
                                };

                                var projectUpdateResponseDto = await _projectServiceHttpApi.UpdateProjectRollOverStatus(_UpdateProjectRolledOverDto);

                                requestData.Message += " Allocation Forwarded and project updated.";

                            }
                            else
                            {
                                requestData.Message += " No Resource Allocation found ";
                            }

                        }
                        else
                        {
                            //show message that new Project is not eligible for rollover
                            requestData.Message += " Project is not eligible for Rollover due to status and dates etc." + projectDetail.PipelineStatus + "," + requestData.PipelineStartDate;
                        }
                    }
                    catch (Exception ex)
                    {
                        requestData.Message += " Exception-" + ex.Message + "-Stack-" + ex.StackTrace?.Substring(0, 500) + "...";
                        //handle individual record error
                    }
                }

                allocationRolloverResponse.projectRolloverRequest = (request.ProjectRolloverRequestData != null && request.ProjectRolloverRequestData.Count > 0) ? request.ProjectRolloverRequestData.First() : new ProjectRolloverRequestDTO();
                allocationRolloverResponse.allWorkflowToTerminate = allWorkflowToTerminate;
                allocationRolloverResponse.allWorkflowToStart = allWorkflowToStart;
                List<string> actions = new List<string>();
                actions.Add(Constants.NotificationActions.ROLL_OVER_ALLOCATED_EMPLOYEES_AVAILABLE_FOR_REVISED_DATES_ALLOCATION_SHIFTED_NEW_WF_TRIGGERED_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER);
                actions.Add(Constants.NotificationActions.NOTIFICATION_TO_REVIEWER_ALLOCATIONS_ARE_SHIFTED_AND_NEW_WORKFLOW_ARE_TRIGGERED);
                allocationRolloverResponse.NotificationActions = actions;

                List<AllocationRolloverUserResponseDTO>? allocationsInDraft = new List<AllocationRolloverUserResponseDTO>();
                if (allocationRolloverResponse != null && allocationRolloverResponse.projectRolloverRequest != null && allocationRolloverResponse.projectRolloverRequest.ProjectAllocations != null)
                {
                    allocationsInDraft = allocationRolloverResponse?.projectRolloverRequest?.ProjectAllocations?.Where(d => d.RollOverAllocaionStatus == RollOverAllocationStatuses.OldCompletedNewDraft).ToList();
                }
                if (allocationRolloverResponse != null && allocationsInDraft != null && allocationsInDraft.Count > 0)
                {
                    allocationRolloverResponse.EmployeeEmailInDraft = allocationsInDraft.Select(e => e.UserEmail).ToList();
                    actions.Add(Constants.NotificationActions.ROLL_OVER_ALLOCATION_IN_DRAFT_DUE_TO_UNAVAILABLITY);
                }
                List<AllocationRolloverUserResponseDTO>? allocationTerminated = new List<AllocationRolloverUserResponseDTO>();
                if (allocationRolloverResponse != null && allocationRolloverResponse.projectRolloverRequest != null && allocationRolloverResponse.projectRolloverRequest.ProjectAllocations != null)
                {
                    allocationTerminated = allocationRolloverResponse?.projectRolloverRequest?.ProjectAllocations?.Where(d => d.RollOverAllocaionStatus == RollOverAllocationStatuses.OldInProgressNewReleased).ToList();
                }

                if (allocationRolloverResponse != null && allocationTerminated != null && allocationTerminated.Count > 0)
                {
                    allocationRolloverResponse.EmployeeEmailTerminated = allocationTerminated.Select(e => e.UserEmail).ToList();
                    actions.Add(Constants.NotificationActions.ROLL_OVER_ALLOCATION_TERMINATED_DUE_TO_UNAVAILABLITY);
                }


                return allocationRolloverResponse;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<bool> CheckIfUserIsAvailableOnNewDates(ResourceAllocationDetailsResponse radAllocation, int rollForwardDays)
        {
            bool isAllSlotsAvailable = false;
            //Loop through all RA Slots 
            foreach (var ra in radAllocation.ResourceAllocations)
            {
                DateTime dtTempRASlotEndDate = ra.EndDate.AddDays(rollForwardDays).ToDateTime(TimeOnly.MaxValue);

                var availabilities = await _mediator.Send(new GetAvaiableHoursByEmailIdQery()
                {
                    RequisitionId = ra.RequisitionId,
                    EmailId = new string[] { ra.EmpEmail },
                    StartDate = ra.StartDate.AddDays(rollForwardDays).ToDateTime(TimeOnly.MinValue),
                    EndDate = dtTempRASlotEndDate,
                    RequireWorkingHours = ra.Efforts,
                    isPerDayHourAllocation = (bool)ra.IsPerDayAllocation
                });

                if (availabilities != null && availabilities.Count > 0)
                {
                    var isUserAvailable = availabilities.Where(m => m.EmailId.ToLower() == radAllocation.EmpEmail.ToLower()).FirstOrDefault();
                    if (isUserAvailable != null && isUserAvailable.IsHoursAvialable == true && string.IsNullOrEmpty(isUserAvailable.ErrorMsg))
                    {
                        //user slot is available and continue
                        isAllSlotsAvailable = true;
                        continue;
                    }
                    else
                    {
                        //user slot is not availbale break loop and handle unavailbale case based on WF status

                        isAllSlotsAvailable = false;
                        break;
                    }

                }
                else
                {
                    //not available 
                    isAllSlotsAvailable = false;
                    break;
                }

            }

            return isAllSlotsAvailable;
        }

        public async Task<List<NotificationPayloadDTO>> CREATE_USER_ALLOCATION_WORKFLOW_BY_ROLLOVER_ALLOCATION(ResourceAllocationDetailsResponse resourceAllocationDetails, string expertiseName, string token)//InitNotificationDTO notificationParams
        {
            var workflows = new List<NotificationPayloadDTO>();
            //config data:- 
            var configurationDetails = await _configurationHttpService.GetConfigurationByExpertiesNameAndGroupName(expertiseName, Constants.ConfigurationTypes.RESOURCE_ALLOCATION_REVIEW);//TODO:- TO BE CHANGED

            var configValueString = configurationDetails.Count > 0 ? configurationDetails[0].AttributeValue : "";
            //var configValue = JsonConvert.DeserializeObject<ReviewerAttributeValue>(configValueString);
            int configValue;
            bool isValueValid = int.TryParse(configValueString, out configValue);

            // Removed circular reference as the object in not needed
            resourceAllocationDetails.ResourceAllocations = null;
            if (resourceAllocationDetails.Requisition != null)
                resourceAllocationDetails.Requisition.demands = null;
            if (resourceAllocationDetails.Requisition != null)
                resourceAllocationDetails.Requisition.RequisitionSkill = null;
            if (resourceAllocationDetails.Requisition != null)
                resourceAllocationDetails.Requisition.RequisitionParameters = null;
            if (resourceAllocationDetails.Requisition != null)
                resourceAllocationDetails.Requisition.RequisitionParameterValues = null;

            CreateWorkflowRequestDTO createWorkflow = new()
            {
                name = Constants.WorkflowStatus.WORKFLOW_NAME_ROLLOVER_ALLOCATION,
                item_id = resourceAllocationDetails.Id, //Guid.NewGuid(),//resourceAllocationDetails.Guid
                module = Constants.WorkflowStatus.WORKFLOW_MODULE_EMPLOYEE_ALLOCATION,
                sub_module = Constants.WorkflowStatus.WORKFLOW_SUB_MODULE_EMPLOYEE_ALLOCATION,
                status = Constants.WorkflowStatus.WORKFLOW_STATUS_EMPLOYEE_ALLOCATION_REVIEW_PENDING_REVIEWER,
                entity_type = Constants.WorkflowStatus.WORKFLOW_ENTITY_TYPE_RESOURCE_ALLOCATION_RESPONSE,
                entity_meta_data = resourceAllocationDetails,
                assigned_to = string.Empty
            };
            if (isValueValid == true && configValue <= 0)
            {
                createWorkflow.status = Constants.WorkflowStatus.WORKFLOW_STATUS_EMPLOYEE_ALLOCATION_REVIEW_PENDING_EMPLOYEE;
            }

            var workflowNotificationPayload = new NotificationPayloadDTO()
            {
                action = Constants.CreateUserAllocationWorkflowAction,
                token = token,// notificationParams.token,
                payload = JsonConvert.SerializeObject(createWorkflow)
            };
            workflows.Add(workflowNotificationPayload);
            return workflows;
        }

        public async Task TerminateWorkflow(List<TerminateWorkflowDTO> workflowRADGUID, string token, UserInfoDTO userInfo)
        {
            //terminate the workflow by guids
            try
            {
                var environmentVariables = Environment.GetEnvironmentVariables();
                var baseUrl = _configuration.GetSection("MicroserviceApiSettings").GetSection("WorkflowBaseUrl").Value;
                var terminateWorkflowUrl = _configuration.GetSection("MicroserviceApiSettings").GetSection("TerminateWorkflowPath").Value;


                var content = new StringContent(JsonConvert.SerializeObject(workflowRADGUID), Encoding.UTF8, "application/json");
                var tokenSplit = token.Trim().Split(" ");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenSplit[1]);
                //Call workflow service post request to terminate workflows
                var url = baseUrl + terminateWorkflowUrl;

                if (_httpClient.DefaultRequestHeaders.Contains("userinfo") == false)
                {
                    _httpClient.DefaultRequestHeaders.Add("userinfo", JsonConvert.SerializeObject(userInfo).ToString());
                }

                var apiResponse = await _httpClient.PostAsync(url, content);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine(await apiResponse.Content.ReadAsStringAsync());
                    throw new Exception("Error fetching TerminateWorkflow");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task CreateWorkflow(NotificationPayloadDTO payload, UserInfoDTO userInfo)
        {
            try
            {
                var environmentVariables = Environment.GetEnvironmentVariables();

                //Call workflow creation call via gateway to update the Allocation response status in response to workflow task action 
                var baseUrl = _configuration.GetSection("MicroserviceApiSettings").GetSection("WorkflowBaseUrl").Value;
                var createWorkflowUrl = _configuration.GetSection("MicroserviceApiSettings").GetSection("CreateWorkflowPath").Value;

                var objValue = JsonConvert.DeserializeObject(payload.payload);
                var content = new StringContent(JsonConvert.SerializeObject(objValue), Encoding.UTF8, "application/json");
                var tokenSplit = payload.token.Trim().Split(" ");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenSplit[1]);
                //Call workflow service post request to create new workflow
                var url = baseUrl + createWorkflowUrl;

                if (_httpClient.DefaultRequestHeaders.Contains("userinfo") == false)
                {
                    _httpClient.DefaultRequestHeaders.Add("userinfo", JsonConvert.SerializeObject(userInfo).ToString());
                }

                //_logger.LogInformation($"Sending request to {url}");

                var apiResponse = await _httpClient.PostAsync(url, content);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                }
                else
                {
                    Console.WriteLine(await apiResponse.Content.ReadAsStringAsync());
                    throw new Exception("Error fetching CreateWorkflow");
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error in Create Workflow", ex.Message, ex.StackTrace, ex.InnerException);
                throw;
            }
        }

        public async Task<List<ResourceAllocationDetailsResponse>> UpdateAllocationByRollOver(ResourceAllocationDetailsResponse allocations, ProjectDTO project, UserDecorator user, CommonResourceAllocationCommandHandler commanResourceAllocation, CancellationToken cancellationToken)
        {
            List<SkillsEntities> _skills = AllocationMapper.Mapper.Map<List<SkillsEntities>>(allocations.Skills);
            List<FormValuesForAllocationDTO> _allocation = new List<FormValuesForAllocationDTO>();
            if (allocations.ResourceAllocations != null && allocations.ResourceAllocations.Count > 0)
            {
                foreach (var item in allocations.ResourceAllocations)
                {
                    _allocation.Add(new FormValuesForAllocationDTO()
                    {
                        ConfirmedAllocationEndDate = item.EndDate.ToDateTime(TimeOnly.MaxValue),
                        ConfirmedAllocationStartDate = item.StartDate.ToDateTime(TimeOnly.MinValue),
                        ConfirmedPerDayHours = item.Efforts,
                        PerDayAllocation = item.IsPerDayAllocation
                    });
                }
            }

            AllResourceAllocationDTO allocationItem = new AllResourceAllocationDTO();
            {
                allocationItem.Available = true;
                allocationItem.Skills = _skills != null ? _skills.ToArray() : new SkillsEntities[] { };
                allocationItem.Description = allocations.Description;
                allocationItem.Email = allocations.EmpEmail;
                allocationItem.ProjectInfo = project;
                allocationItem.RequisitionId = allocations.RequisitionId;
                allocationItem.TotalEfforts = allocations.TotalEffort;
                allocationItem.type = allocations.Type;
                allocationItem.UserInfo = new UserDetailsCommonDTO()
                {
                    Designation = allocations.Designation != null ? allocations.Designation : string.Empty,
                    Competency = allocations.Requisition?.Competency != null ? allocations.Requisition?.Competency : string.Empty,
                    Email = allocations.EmpEmail,
                    EmpName = allocations.EmpName,
                    Grade = allocations.Grade != null ? allocations.Grade : string.Empty,
                };
                allocationItem.Allocations = _allocation;
            };
            List<AllResourceAllocationDTO> allocationItems = new List<AllResourceAllocationDTO>(); ;
            allocationItems.Add(allocationItem);
            var request = new CommonResourceAllocationCommand()
            {
                resourceAllocationDTO = allocationItems.ToArray(),
                user = user,
                isDraft = allocations.AllocationStatus == AllocationStatuses.DRAFT ? true : false
            };
            var response = await commanResourceAllocation.Handle(request, cancellationToken);
            return response;
        }
    }
}
