using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RMT.Allocation.API.Controllers;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.DTOs.CommonResourceAllocationDTOs;
using RMT.Allocation.Application.DTOs.RequisitionDTOs;
using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;
using RMT.Allocation.Application.Handlers.CommandHandlers;
using RMT.Allocation.Application.Handlers.QueryHandlers;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.DTO.Response;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Infrastructure.DTOs;
using RMT.Allocations.API.Attributes;
using RMT.Allocations.API.Services;
using System.Collections.Generic;
using System.Linq;
using ResourceAllocationResponse = RMT.Allocation.Infrastructure.DTOs.ResourceAllocationResponse;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RMT.Allocations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceAllocationController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;

        public ResourceAllocationController(IMediator mediator, IUserAccessor userAccessor, ILogger<BaseController> logger) : base(logger)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// GetProjectsByEmployeeEmail / GetAllocationByProjectCodeForEmployee
        /// </summary>
        /// <param name="getResourceAllocationByEmailQuery"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        // GET: api/<ResourceAllocationController>
        [HttpGet("GetAllocationsByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ResourceAllocationResponse>>> GetAllocationsByEmail([FromQuery] GetResourceAllocationByEmailQuery getResourceAllocationByEmailQuery)
        {
            try
            {
                var request = new GetResourceAllocationByEmailQuery()
                {
                    EmpEmail = getResourceAllocationByEmailQuery.EmpEmail
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetAllocationsByEmailorClients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ResourceAllocationResponse>>> GetAllocationsByEmailorClients([FromBody] GetResourcesAllocationByEmailOrClientsQuery getResourceAllocationByEmailQuery)
        {
            try
            {
                var request = new GetResourcesAllocationByEmailOrClientsQuery()
                {
                    EmpEmail = getResourceAllocationByEmailQuery.EmpEmail,
                    clientName= getResourceAllocationByEmailQuery.clientName?.ToList(),
                    clientGroup = getResourceAllocationByEmailQuery.clientGroup?.ToList()
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// GetProjectsByEmployeeEmailAndPipelineCode
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pipelineCode"></param>
        /// <param name="jobCode"></param>
        /// <returns></returns>
        [HttpGet("GetProjectsByEmployeeEmailAndPipelineCode")]
        public async Task<List<EmployeeProject>> GetProjectsByEmployeeEmailAndPipelineCode(string email, string? pipelineCode, string? jobCode)
        {
            try
            {
                var request = new GetProjectsByEmployeeEmailAndPipelineCodeQuery()
                {
                    Email = email,
                    PipelineCode = pipelineCode,
                    JobCode = jobCode
                };
                List<EmployeeProject> result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// GetProjectsByPipelineCode
        /// </summary>
        /// <param name="pipelineCode"></param>
        /// <param name="jobCode"></param>
        /// <param name="emailId"></param>
        /// <returns></returns>
        [HttpGet("GetProjectsByPipelineCode")]
        [SanitizeInput]
        public async Task<List<EmployeeProject>> GetProjectsByPipelineCode(string pipelineCode, string? jobCode, string? emailId)
        {
            try
            {
                var user = _userAccessor.GetUser();
                var request = new GetProjectsByPipelineCodeQuery()
                {
                    PipelineCode = pipelineCode,
                    JobCode = jobCode,
                    userAppRoles = user.roles,
                    EmailId = !String.IsNullOrEmpty(emailId) ? emailId : user.email

                };
                List<EmployeeProject> result = await _mediator.Send(request);
                return result;

            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("v2/GetProjectsByPipelineCode")]
        [SanitizeInput]
        public async Task<List<EmployeeProject>> GetProjectsByPipelineCodV2(string pipelineCode, string? jobCode, string? emailId, bool? isAllocationDetailsFilterByUserRoles)
        {
            try
            {
                var user = _userAccessor.GetUser();
                var request = new GetProjectsByPipelineCodeQuery()
                {
                    PipelineCode = pipelineCode,
                    JobCode = jobCode,
                    userAppRoles = user.roles,
                    EmailId = !String.IsNullOrEmpty(emailId) ? emailId : user.email,
                    isAllocationDetailsFilterByUserRoles = isAllocationDetailsFilterByUserRoles

                };
                List<EmployeeProject> result = await _mediator.Send(request);
                if (isAllocationDetailsFilterByUserRoles != null && (bool)isAllocationDetailsFilterByUserRoles && !string.IsNullOrEmpty(emailId))
                {
                    return result.Where(a => a.EmpEmail == emailId).ToList();
                }
                return result;

            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// GetCurrentUserAllocationCalander
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost("GetCurrentUserAllocationCalander")]
        public async Task<List<EmployeeProject>> GetCurrentUserAllocationCalander([FromBody] GetCurrentUserAllocationCalanderRequestDto args)
        {
            try
            {
                var user = _userAccessor.GetUser();
                if (user == null)
                {
                    throw new Exception("User Can not be null");
                }
                var request = new GetCurrentUserAllocationCalanderQuery()
                {
                    UserEmail = user.email == null ? null : user.email,
                    StartDate = args.StartDate,
                    EndDate = args.EndDate,
                    PipelineCodes = args.FilterParameters.PipelineCodes
                };
                List<EmployeeProject> result = await _mediator.Send(request);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// GetCurrentUserAllocationCalanderFilterOptions
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCurrentUserAllocationCalanderFilterOptions")]
        public async Task<GetCurrentUserAllocationCalanderFilterOptionsResponse> GetCurrentUserAllocationCalanderFilterOptions()
        {
            try
            {
                var user = _userAccessor.GetUser();
                if (user == null)
                {
                    throw new Exception("User Can not be null");
                }
                var request = new GetCurrentUserAllocationCalanderFilterOptionsQuery()
                {
                    UserEmail = user.email == null ? null : user.email,
                };
                GetCurrentUserAllocationCalanderFilterOptionsResponse result = await _mediator.Send(request);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("UpdatePublishAllocationActualEfforts")]
        public async Task<bool> UpdatePublishAllocationActualEfforts(List<UpdatePublishAllocationActualEffortsRequestDTO> req)
        {
            try
            {
                await _mediator.Send(new UpdatePublishAllocationActualEffortsCommand()
                {
                    AllocationActualEfforts = req
                });
                return true;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return false;

            }
        }

        /// <summary>
        /// GetActiveAllocationByPipeLineCode
        /// </summary>
        /// <param name="pipelineCode"></param>
        /// <param name="jobCode"></param>
        /// <param name="isAllocationDetailsFilterByUserRoles"></param>
        /// <returns></returns>
        [HttpGet("GetActiveAllocationByPipeLineCode")]
        [SanitizeInput]
        public async Task<List<GetResourceAllocationDetailsListByCurrentUserRoleResponse>> GetActiveAllocationByPipeLineCode(string pipelineCode, string? jobCode, bool? isAllocationDetailsFilterByUserRoles)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var request = new GetActiveAllocationQuery()
                {
                    PipelineCode = pipelineCode,
                    JobCode = jobCode,
                    userEmail = userInfo != null && userInfo.email == null ? null : userInfo.email,
                    isAllocationDetailsFilterByUserRoles = isAllocationDetailsFilterByUserRoles,
                    userAppRoles = userInfo.roles,
                };
                List<GetResourceAllocationDetailsListByCurrentUserRoleResponse> result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {

                //this.LogException(ex);
                this.HandleException(ex); return null;
            }

        }
        [HttpPost("GetPublishedActiveAllocationByPipeLineCode")]
        public async Task<List<ResourceAllocationDetailsResponse>> GetActivePublishedAllocationByPipeLineCode(GetActivePublishedAllocationByPipeLineCodeRequestDTO req)
        {
            try
            {
                var request = new GetActivePublishedAllocationByPipeLineCodeQuery
                {
                    res = req.PipelineCodes
                };
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
                //throw;
            }


        }

        /// <summary>
        /// Get All the resource allocated to a project code currently => GetAllocationByProjectCodeForRequestor
        /// </summary>
        /// <param name="pipelineCode"></param>
        /// <param name="jobCode"></param>
        /// <returns></returns>
        [HttpGet("GetResourceAllocationByProjectCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ResourceAllocationResponse>> GetResourceAllocationByProjectCode(string pipelineCode, string jobCode)
        {
            try
            {
                var request = new GetResourceAllocationByProjectCodeQuery() { PipelineCode = pipelineCode, JobCode = jobCode };
                List<ResourceAllocationResponse> result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// GetAllocationByRequisitionID
        /// </summary>
        [HttpGet("GetMultipleAllocationsByRequisitionIDs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SanitizeInput]
        public async Task<List<ResourceAllocationDetailsWithConsumedHours>> GetMultipleAllocationsByRequisitionIDs([FromQuery] Guid[] ids)
        {
            try
            {
                List<ResourceAllocationDetailsWithConsumedHours> result = new();

                foreach (var requisitionId in ids)
                {
                    var request = new GetAllocationByRequisitionIDQery() { RequisitionId = requisitionId };
                    result.Add(await _mediator.Send(request));
                }
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// GetAvaiableHoursByEmailId
        /// </summary>
        [HttpGet("GetAvaiableHoursByEmailId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ResourceAvailable>> GetAvaiableHoursByEmailId([FromQuery] GetAvaiableHoursByEmailIdQery request)
        {
            try
            {
                var result = await _mediator.Send(new GetAvaiableHoursByEmailIdQery()
                {
                    RequisitionId = request.RequisitionId,
                    EmailId = request.EmailId,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    RequireWorkingHours = request.RequireWorkingHours,
                    isPerDayHourAllocation = request.isPerDayHourAllocation,
                    PipelineCode = request.PipelineCode,
                    JobCode = request.JobCode,
                });
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }

        }

        /// <summary>
        /// GetSystemSuggestionsByRequisitionId
        /// </summary>
        [HttpGet("GetSystemSuggestionsByRequisitionId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SanitizeInput]
        public async Task<List<SystemSuggestionResponseDTO>> GetSystemSuggestionsByRequisitionId([FromQuery] GetSystemSuggestionsByRequisitionIdQuery getSystemSuggestionsByRequisitionIdQuery)
        {
            try
            {
                var request = new GetSystemSuggestionsByRequisitionIdQuery()
                {
                    limit = getSystemSuggestionsByRequisitionIdQuery.limit,
                    pagination = getSystemSuggestionsByRequisitionIdQuery.pagination,
                    requisitionId = getSystemSuggestionsByRequisitionIdQuery.requisitionId,
                    parameter_value_pairs = getSystemSuggestionsByRequisitionIdQuery.parameter_value_pairs
                };
                List<SystemSuggestionResponseDTO> result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// NOT IN USE
        /// IsUserAvailable
        /// </summary>
        //[HttpGet("IsUserAvailable")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<List<UsersAvailability>> IsUserAvailable([FromQuery] IsUserAvailableQuery isUserAvailableQuery)
        //{
        //    try
        //    {
        //        var request = new IsUserAvailableQuery()
        //        {
        //            emails = isUserAvailableQuery.emails,
        //            end_date = isUserAvailableQuery.end_date,
        //            start_date = isUserAvailableQuery.start_date,
        //            //leaves = isUserAvailableQuery.leaves,
        //            total_required_hours = isUserAvailableQuery.total_required_hours
        //        };
        //        List<UsersAvailability> result = await _mediator.Send(request);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        //this.LogException(ex);
        //        this.HandleException(ex); return null;
        //    }
        //}

        /// <summary>
        /// GetUsersTimeline
        /// </summary>
        [HttpGet("GetUsersTimeline")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<GetUsersTimelineResponse>> GetUsersTimeline([FromQuery] GetUsersTimelineDTO request)
        {
            try
            {
                GetUsersTimelineQuery query = new GetUsersTimelineQuery()
                {
                    emails = request.emails.Split(",").ToList(),
                    end_date = request.end_date,
                    start_date = request.start_date,
                };
                var result = await _mediator.Send(query);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// GET : ResourceAllocationDetailsByGuid
        /// </summary>
        [HttpGet("ResourceAllocationDetailsByGuid")]
        public async Task<ResourceAllocationDetailsResponse> GetResourceAllocationDetailsByGuid(Guid guid)
        {
            try
            {
                var result = await _mediator.Send(new GetResourceAllocationDetailsByGuidQuery()
                {
                    guid = guid
                });
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// UpdateAllocationStatusInResourceAllocationDetails
        /// </summary>
        [HttpPost("UpdateAllocationStatusInResourceAllocationDetails")]
        public async Task<ResourceAllocationDetailsResponse> UpdateAllocationStatusInResourceAllocationDetails([FromBody] AllocationStatusRadDTO allocationStatusRequest)
        {
            try
            {
                var result = await _mediator.Send(new UpdateResourceAllocationDetailsAllocationStatusCommand()
                {
                    guid = allocationStatusRequest.Guid,
                    AllocationStatus = allocationStatusRequest.AllocationStatus,
                    ModifiedDate = DateTime.UtcNow
                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// UpdateListOfAllocationStatusInResourceAllocationDetails
        /// </summary>
        /// <param name="allocationStatusRequests"></param>
        /// <returns></returns>
        [HttpPost("UpdateListOfAllocationStatusInResourceAllocationDetails")]
        public async Task<UpdateListOfAllocationDetailsStatusResponse> UpdateListOfAllocationAndRequisitionStatusInResourceAllocationDetails([FromBody] List<AllocationStatusRadDTO> allocationStatusRequests)
        {
            try
            {
                var requests = new List<ResouceAllocationDetailsStatusUpdate>();
                foreach (var allocationReq in allocationStatusRequests)
                {
                    var req = new ResouceAllocationDetailsStatusUpdate()
                    {
                        AllocationStatus = allocationReq.AllocationStatus,
                        guid = allocationReq.Guid,
                        WorkflowSubModule = allocationReq.WorkflowSubModule,
                        WorkflowModule = allocationReq.WorkflowModule,
                        ModifiedDate = DateTime.UtcNow,
                        token = allocationReq.token
                    };
                    requests.Add(req);
                }
                var response = await _mediator.Send(new UpdateListOfResourceAllocationDetailsAllocationStatusCommand()
                {
                    UpdateAllocationStatusList = requests
                });
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// GetAllocatedHoursRatioByPipelineCode
        /// </summary>
        [HttpPost("GetAllocatedHoursRatioByPipelineCode")]
        public async Task<List<ProjectAllocatedHoursRatioDto>> GetAllocatedHoursRatioByPipelineCode([FromBody] List<KeyValuePair<string, string>> pipelineCodes)
        {
            try
            {
                var request = new GetAllocatedHoursRatioByPipelineCodeQuery()
                {
                    pipelineCodes = pipelineCodes
                };
                List<ProjectAllocatedHoursRatioDto> result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }




        /// <summary>
        /// SuspendAllocation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("SuspendAllocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<SuspendAllocationResponse>> SuspendAllocation([FromBody] List<KeyValuePair<string, string>> request)
        {
            try
            {
                SuspendAllocationCommand command = new()
                {
                    ProjectCode = request
                };
                var result = await _mediator.Send(command);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// GET : GetAllocationInformationByLeaves
        /// </summary>
        [HttpPost("GetAllocationInformationByLeaves")]
        public async Task<List<AllocationWithLeavesAndResourceRequestorsResponse>> GetAllocationDetailsByEmailAndLeaveDates([FromBody] List<GetAllocationByEmployeeEmailAndLeaveDTO> getAllocationByEmployeeEmailAndLeaveDTOs)
        {
            try
            {
                var token = _userAccessor.GetToken();
                var request = new GetAllocationsByEmailAndLeavesQuery()
                {
                    EmployeeLeaves = getAllocationByEmployeeEmailAndLeaveDTOs,
                    token = token
                };
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }

        }

        /// <summary>
        /// GET : GetResourceAllocationByGuid
        /// </summary>

        [HttpGet("GetResourceAllocationByGuid")]
        public async Task<ResourceAllocationDetailsResponse> GetResourceAllocationByGuid(Guid guid)
        {
            try
            {
                var request = new GetAllocationByGuidQuery()
                {
                    guid = guid
                };
                ResourceAllocationDetailsResponse result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }

        }

        /// <summary>
        /// GET : ReleaseResourceByGuid
        /// </summary>
        [HttpPost("ReleaseResourceByGuid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResourceAllocationDetailsResponse> ReleaseResourceByGuid([FromBody] ReleaseResourceDTO releaseResourceDTO)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var token = _userAccessor.GetToken();
                var result = await _mediator.Send(new ReleaseResourceCommand
                {
                    guid = releaseResourceDTO.guid,
                    ModifiedBy = userInfo.email,
                    token = token
                });
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// ResourceAllocationDayGroup
        /// </summary>
        /// <param name="allocationDayGroup"></param>
        /// <returns></returns>
        [HttpGet("ResourceAllocationDayGroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<AllocationDayGroupResponseDTO>> ResourceAllocationDayGroup([FromQuery] AllocationDayGroupDTO allocationDayGroup)
        {
            try
            {
                var response = await _mediator.Send(new ResourceAllocationDayGroupQuery
                {
                    JobCode = allocationDayGroup.JobCode,
                    TimeOption = allocationDayGroup.TimeOption

                });

                return response;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("PublishResouceAllocationDayGrouped")]
        public async Task<List<AllocationDayResourceGroup>> PublishResouceAllocationDayGrouped()
        {
            try
            {
                var req = new PublishedResouceAllocationDaysGroupQuery();
                return await _mediator.Send(req);
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// CommonResourceAllocation
        /// </summary>
        /// <param name="allResourceAllocationDTO"></param>
        /// <param name="isDraft"></param>
        /// <returns></returns>
        [HttpPost("CommonResourceAllocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ResourceAllocationDetailsResponse>> CommonResourceAllocation([FromBody] AllResourceAllocationDTO[] allResourceAllocationDTO, [FromQuery] bool isDraft)
        {
            try
            {
                var user = _userAccessor.GetUser();
                var request = new CommonResourceAllocationCommand()
                {
                    resourceAllocationDTO = allResourceAllocationDTO,
                    user = user,
                    isDraft = isDraft
                };
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// NewResourceAllocationMove
        /// </summary>
        /// <param name="AllResourceAllocationDTO"></param>
        /// <param name="isDraft"></param>
        /// <returns></returns>
        [HttpPost("NewResourceAllocationMove")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<NewJobCodeMoveResponseDTO> NewResourceAllocationMove([FromBody] NewJobCodeMoveDTO AllResourceAllocationDTO, [FromQuery] bool isDraft)
        {
            try
            {
                NewResourceAllocationMoveQuery req = new()
                {
                    newJobCodeMoveDTO = AllResourceAllocationDTO

                };
                return await _mediator.Send(req);

            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// RolloverAllocationByPipelineCode
        /// </summary>
        /// <param name="pipelineCodes"></param>
        /// <returns></returns>
        [HttpPost("RolloverAllocationByPipelineCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<AllocationRolloverResponseDTO> RolloverAllocationByPipelineCode([FromBody] List<ProjectRolloverRequestDTO> pipelineCodes)
        {
            try
            {
                var token = _userAccessor.GetToken();
                var email = _userAccessor.GetUser()?.email;

                var cmd = new RolloverAllocationByPipelineCodeCommand
                {
                    ProjectRolloverRequestData = pipelineCodes.ToList(),
                    UserEmail = email,
                    UserToken = token,
                    Message = string.Empty,
                    user = _userAccessor.GetUser()

                };
                AllocationRolloverResponseDTO response = await _mediator.Send(cmd);

                try
                {
                    string msg = JsonConvert.SerializeObject(response?.projectRolloverRequest?.Message);
                    this.LogInformation("-RolloverAllocationByPipelineCode-Message-" + msg);
                    string output = JsonConvert.SerializeObject(response?.projectRolloverRequest);
                    this.LogInformation("-RolloverAllocationByPipelineCode-projectRolloverRequest-" + output);
                    output = JsonConvert.SerializeObject(response?.allWorkflowToStart);
                    this.LogInformation("-RolloverAllocationByPipelineCode-allWorkflowToStart-" + output);
                    output = JsonConvert.SerializeObject(response?.allWorkflowToTerminate);
                    this.LogInformation("-RolloverAllocationByPipelineCode-allWorkflowToTerminate-" + output);
                }
                catch (Exception innerEx)
                {
                    this.LogInformation("-RolloverAllocationByPipelineCode-Response-Exception--{0}--{1}", innerEx.Message, innerEx.StackTrace);
                }
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// GetUsersTimelineForMultipleDates
        /// </summary>
        [HttpPost("GetUsersTimelineForMultipleDates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<GetUsersTimelineResponse>> GetUsersTimelineForMultipleDates([FromBody] List<GetUsersTimelineDTO> getUserTimelineRequest)
        {
            try
            {
                List<GetUsersTimelineResponse> result = new();
                foreach (var request in getUserTimelineRequest)
                {
                    GetUsersTimelineQuery query = new GetUsersTimelineQuery()
                    {
                        emails = request.emails.Split(",").ToList(),
                        end_date = request.end_date,
                        start_date = request.start_date,
                    };

                    var response = await _mediator.Send(query);
                    foreach (var item in response)
                    {
                        result.Add(item);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// UpdateProjectJobCode
        /// </summary>
        /// <param name="changeCodeDTO"></param>
        /// <returns></returns>
        [HttpPost("UpdateProjectJobCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Boolean> UpdateProjectJobCode([FromBody] ChangeCodeDTO changeCodeDTO)
        {
            try
            {
                var request = new ChangeCodeForProjectCommand()
                {
                    changeProjectCodeDTO = changeCodeDTO
                };
                var result = await _mediator.Send(request);
                return true;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return false;
            }
        }

        /// <summary>
        /// GetSkillByEmailJobCode
        /// </summary>
        /// <param name="emailId"></param>
        /// <param name="pipelineCode"></param>
        /// <param name="jobCode"></param>
        /// <returns></returns>
        [HttpGet("GetSkillByEmailJobCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<SkillResponseDto>> GetSkillByEmailJobCode([FromQuery] string emailId, string pipelineCode, string jobCode)
        {
            try
            {
                GetSkillbyEmailJobCodeQuery request = new()
                {
                    EmailId = emailId,
                    JobCode = jobCode,
                    PipelineCode = pipelineCode
                };
                var response = await _mediator.Send(request);
                return response;

            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// DoesUserHaveAnyFutureOrOngoingAllocations
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("DoesUserHaveAnyFutureOrOngoingAllocations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<string>> DoesUserHaveAnyFutureOrOngoingAllocationsQuery([FromQuery] DoesUserHaveAnyFutureOrOngoingAllocationsQuery query)
        {
            try
            {
                return await _mediator.Send(new DoesUserHaveAnyFutureOrOngoingAllocationsQuery()
                {
                    emails = query.emails
                });
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// RemoveUsersAllDraftAllocations
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("RemoveUsersAllDraftAllocations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> RemoveUsersAllDraftAllocations([FromQuery] RemoveUsersAllDraftAllocationsCommand query)
        {
            try
            {
                return await _mediator.Send(new RemoveUsersAllDraftAllocationsCommand()
                {
                    emails = query.emails.ToList()
                });
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return false;
            }
        }

        /// <summary>
        /// GetAllAllocationByPipelineCode
        /// </summary>
        /// <param name="pipelineCode"></param>
        /// <param name="jobCode"></param>
        /// <returns></returns>
        [HttpGet("GetAllAllocationByPipelineCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ResourceAllocationDetailsResponse>> GetAllAllocationByPipelineCode([FromQuery] string pipelineCode, [FromQuery] string? jobCode)
        {
            try
            {
                var user = _userAccessor.GetUser();
                var request = new GetAllAllocationByPipelineOrJobCodeQuery()
                {
                    PipelineCode = pipelineCode,
                    JobCode = jobCode
                };
                List<ResourceAllocationDetailsResponse> result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("EmployeeTimelineForEmployeePortfolioReport")]
        public async Task<List<EmployeePerDayTimelineForEmployeePortfolioDto>> GetEmployeeTimelineForEmployeePortfolioReport([FromBody] GetEmployeeTimelineForEmployeePortfolioReportRequestDto req)
        {
            try
            {
                //var user = _userAccessor.GetUser();
                var request = new GetEmployeeTimelineForEmployeePortfolioReportQuery
                {
                    reqInput = req
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
                return null;
                //throw;
            }
        }
        [HttpPost("PublishedResourceAllocationDays")]
        public async Task<List<ResourceAllocationDaysResponse>> GetPublishedResourceAllocationDays(PublishedResourceAllocationDaysQuery req)
        {
            try
            {
                var response = await _mediator.Send(req);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
                return null;
            }
        }
        /// <summary>
        /// UpdateResourceAllocations
        /// </summary>
        /// <param name="allResourceAllocationDTO"></param>
        /// <returns></returns>
        [HttpPost("UpdateResourceAllocations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ResourceAllocationDetailsResponse>> UpdateResourceAllocations([FromBody] ResourceAllocationDetailsResponse[] allResourceAllocationDTO)
        {
            try
            {
                var user = _userAccessor.GetUser();
                var token = _userAccessor.GetToken();

                var request = new UpdateResourceAllocationsCommand()
                {
                    resourceAllocationDTO = allResourceAllocationDTO,
                    user = user,
                    UserToken = token,
                };
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        //not in use currently
        [HttpPost("GetResourceAllocationByProjectCodeList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ResourceAllocationResponse>>> GetResourceAllocationByProjectCodeList(string startDate, string endDate)
        {
            try
            {
                UserDecorator user = _userAccessor.GetUser();

                var request = new GetResourceAllocationByProjectCodeListQuery
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    User = user
                };

                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }



        /// <summary>
        /// HandleException
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        private object HandleException(Exception ex)
        {
            Guid guid = Guid.NewGuid();
            this.LogException(ex, guid);
            throw new BadHttpRequestException($"{ex.Message}-errorid:{guid}", StatusCodes.Status400BadRequest);//, ex);
        }

    }
}
