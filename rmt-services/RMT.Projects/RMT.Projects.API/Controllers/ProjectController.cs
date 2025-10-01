using MediatR;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.Handlers.CommandHandlers;
using RMT.Projects.Application.Handlers.QueryHandlers;
using RMT.Projects.Application.QueryParameter;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using RMT.Projects.Domain.DTOs.Response;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using RMT.Projects.Domain.DTOs.Request;
using RMT.Projects.Application.HttpServices.DTOs;
using RMT.Projects.API.Services;
using System.Reflection.Emit;
using RMT.Projects.Domain;
using System.Security.AccessControl;
using RMT.Projects.API.Attributes;
using System.Data;

namespace RMT.Projects.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;
        public ProjectController(IMediator mediator, IUserAccessor userAccessor, ILogger<BaseController> logger) : base(logger)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProjectFullDetailsResponse>> Get()
        {
            try
            {
                var request = new GetAllProjectsQuery();
                List<ProjectFullDetailsResponse> result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetAllProjectsForBudgetByJobCodes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<BasicProjectDetailsRequestorResponse>> GetAllProjectsForBudgetByJobCodes(List<string> JobCodes)
        {
            try
            {
                var request = new GetAllProjectForBudgetByJobCodesQuery()
                {
                    JobCodes = JobCodes,
                };

                List<BasicProjectDetailsRequestorResponse> result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }


        //To be revisted again for budget section
        [HttpGet("GetAllProjectsForBudget")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<BasicProjectDetailsRequestorResponse>> GetAllProjectsForBudget()
        {
            try
            {
                var request = new GetAllProjectForBudgetQuery();
                List<BasicProjectDetailsRequestorResponse> result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetAllProjectByBUandExpertise")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProjectFullDetailsResponse>> GetAllProjectByBUandExpertise([FromQuery] string BU, [FromQuery] string Expertise, [FromQuery] string Offerings, [FromQuery] DateTime EndDate)//Recheck
        {
            try
            {
                GetAllProjectsByBUandExpertiseQuery request = new()
                {
                    BU = BU,
                    Expertise = Expertise,//Recheck
                    Offerings = Offerings,
                    EndDate = EndDate
                };
                List<ProjectFullDetailsResponse> result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                throw new BadHttpRequestException(ex.Message, StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpGet("GetAllProjectByCreationDate")]
        public async Task<List<ProjectFullDetailsResponse>> GetAllProjectProjectsByCreateDate([FromQuery] DateOnly creationDate)
        {
            try
            {
                var cmd = new ProjectDetailsByCreationDateQuery
                {
                    CreationDate = creationDate
                };
                var result = await _mediator.Send(cmd);
                return result;
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                throw new BadHttpRequestException(ex.Message, StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpPost("GetProjectListDataByEmailId")]
        [SanitizeInput]
        public async Task<List<ProjectFullDetailsResponse>> GetProjectListDataByEmailId([FromBody] ProjectListByEmailDTO projectListRequestDTO)
        {
            try
            {
                UserDecorator user = _userAccessor.GetUser();
                var request = new GetProjectListDataByEmailIdQuery()
                {
                    Limit = projectListRequestDTO.limit > 0 ? projectListRequestDTO.limit : 10000,
                    Pagination = projectListRequestDTO.pagination > 0 ? projectListRequestDTO.pagination : 1,
                    UserEmail = projectListRequestDTO.userEmail,
                    Role = Domain.Constant.UserRoles.Employee
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
        /// GetProjectListDataByUser
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="projectChargeType"></param>
        /// <param name="limit"></param>
        /// <param name="pagination"></param>
        /// <param name="orderBy"></param>
        /// <param name="filterQueryParameters"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        [HttpPost("GetProjectListDataByUser")]
        [SanitizeInput]
        public async Task<List<ProjectFullDetailsResponse>> GetProjectListDataByUser([FromBody] ProjectListRequestDTO projectListRequestDTO)
        {
            try
            {
                UserDecorator user = _userAccessor.GetUser();

                var request = new GetProjectListDataByUserQuery()
                {
                    Limit = projectListRequestDTO.limit > 0 ? projectListRequestDTO.limit : 10000,
                    Pagination = projectListRequestDTO.pagination > 0 ? projectListRequestDTO.pagination : 1,
                    OrderBy = projectListRequestDTO.orderBy,
                    SearchQuery = projectListRequestDTO.searchQuery,
                    RequestorEmail = projectListRequestDTO.userEmail,
                    ProjectChargeType = projectListRequestDTO.filterQueryParameters.ProjectChargeType,
                    Expertises = projectListRequestDTO.filterQueryParameters.Expertises,//Recheck
                    Smes = projectListRequestDTO.filterQueryParameters.Smes,//Recheck
                    Offerings = projectListRequestDTO.filterQueryParameters.Offerings,
                    Solutions = projectListRequestDTO.filterQueryParameters.Solutions,
                    Industry = projectListRequestDTO.filterQueryParameters.Industry,
                    SubIndustry = projectListRequestDTO.filterQueryParameters.SubIndustry,
                    Smegs = projectListRequestDTO.filterQueryParameters.Smegs,//Recheck
                    ClientNames = projectListRequestDTO.filterQueryParameters.ClientNames,
                    PipelineCodes = projectListRequestDTO.filterQueryParameters.PipelineCodes,
                    Bu = projectListRequestDTO.filterQueryParameters.Bu,
                    JobCodes = projectListRequestDTO.filterQueryParameters.JobCodes,
                    JobNames = projectListRequestDTO.filterQueryParameters.JobName,
                    MarketPlace = projectListRequestDTO.filterQueryParameters.MarketPlace,
                    ProjectStatus = projectListRequestDTO.filterQueryParameters.ProjectStatus,
                    ProjectType = projectListRequestDTO.filterQueryParameters.ProjectType,
                    RevenueUnit = projectListRequestDTO.filterQueryParameters.RevenueUnit,//Recheck
                    Roles = projectListRequestDTO.searchRoles != null && projectListRequestDTO.searchRoles.Count > 0 ? projectListRequestDTO.searchRoles : null,
                    //have default value as true or pass value as false
                    IsAllocatedHoursRequired = projectListRequestDTO.filterQueryParameters.IsAllocatedHoursRequired == null
                ? true : (projectListRequestDTO.filterQueryParameters.IsAllocatedHoursRequired == true ? true : false),
                    userDecorator = user,
                };
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }
        [HttpPost("ProjectActivationStatusChange")]
        public async Task<ProjectResponse> ProjectActivationStatusChange([FromBody] ProjectActivationStatusChangeRequestDTO req)
        {
            try
            {
                ProjectActivationStatusChangeCommand request = new ProjectActivationStatusChangeCommand()
                {
                    PipelineCode = req.PipelineCode,
                    JobCode = string.IsNullOrEmpty(req.JobCode) ? null : req.JobCode,
                    IsJobClosed = req.IsJobClosed
                };
                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {

                this.HandleException(ex);
                return null;
            }

        }

        /// <summary>
        /// GetProjectFullDetailsForRequestor / GetProjectBasicDetailsByProjectCode
        /// </summary>
        /// <param name="pipelineCode"></param>
        /// <returns></returns>
        [HttpGet("GetBasiProjectDetailRequestorByPipelineCode")]
        //[ValidateAntiForgeryToken]
        [SanitizeInput]
        public async Task<BasicProjectDetailsRequestorResponse> GetBasicProjectDetailRequestorByPipelineCode(string pipelineCode, string? jobCode)
        {
            try
            {
                var request = new GetBasicProjectDetailRequestorByPipelineCodeQuery()
                {
                    PipelineCode = pipelineCode,
                    JobCode = jobCode
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
        /// GetProjectFullDetailsByPipelineCode
        /// </summary>
        /// <param name="pipelineCode"></param>
        /// <returns></returns>
        [HttpGet("GetProjectFullDetailsByPipelineCode")]
        public async Task<Project> GetProjectFullDetailsByPipelineCode(string pipelineCode, string? jobCode)
        {
            try
            {
                var request = new GetProjectFullDetailsByPipelineCodeQuery()
                {
                    PipelineCode = pipelineCode,
                    JobCode = jobCode
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
        /// AddProjectUserWithRole
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("AddProjectUserWithRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ProjectRoles[]> AddProjectUserWithRole([FromBody] AddProjectUserCommand command)
        {
            try
            {
                var result = await _mediator.Send(new AddProjectUserCommand
                {
                    AddProjectUserRoles = command.AddProjectUserRoles
                });

                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }
        [HttpPost("RemoveProjectUserWithRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ProjectRoles[]> RemoveProjectUserWithRole([FromBody] RemoveProjectUserCommand command)
        {
            try
            {
                var result = await _mediator.Send(new RemoveProjectUserCommand
                {
                    AddProjectUserRoles = command.AddProjectUserRoles
                });

                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }


        [HttpPost("ReplaceProjectsSuperCoachRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ProjectRoles[]> ReplaceProjectsSuperCoachRole([FromBody] ReplaceProjectsSuperCoachRoleCommand command)
        {
            try
            {
                var result = await _mediator.Send(new ReplaceProjectsSuperCoachRoleCommand
                {
                    PreviouseUser = command.PreviouseUser,
                    User = command.User,
                    ProjectCodes = command.ProjectCodes
                });

                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        //[HttpPost("UpdateProjectUserRoleWithNewUser")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ProjectRoles[]> UpdateProjectUserRoleWithNewUser([FromBody] RemoveProjectUserCommand command)
        //{
        //    try
        //    {
        //        var result = await _mediator.Send(new RemoveProjectUserCommand
        //        {
        //            AddProjectUserRoles = command.AddProjectUserRoles
        //        });

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.HandleException(ex); return null;
        //    }
        //}

        /// <summary>
        /// UpdateProject
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("UpdateProjectDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProjectResponse>> UpdateProjectDetails([FromBody] UpdateProjectCommand command)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var result = await _mediator.Send(new UpdateProjectCommand()
                {
                    Id = command.Id,
                    //ProjectCode = command.ProjectCode,
                    //ProjectName = command.ProjectName,
                    //JobCode = command.JobCode,
                    PipelineCode = command.PipelineCode,
                    jobCode = command.jobCode,

                    //ClientName = command.ClientName,
                    //Expertise = command.Expertise,
                    //SME = command.SME,
                    //StartDate = command.StartDate,
                    //EndDate = command.EndDate,
                    Description = command.Description,
                    ModifiedBy = userInfo != null ? userInfo.email : null,// "",
                                                                          //ModifiedAt = // "2023-07-18 11:56:26.748+05:30",
                    ProjectRoles = command.ProjectRoles,// no need to chnage to ProjectRolesView
                    ProjectSkills = command.ProjectSkills,
                    ProjectDemands = command.ProjectDemands,
                    ProjectEndDate = command.ProjectEndDate,
                    IsEndDateChanged = command.IsEndDateChanged,
                    ModifiedAt = DateTime.UtcNow,
                    IsConfidential = command.IsConfidential
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }
        [HttpPost("RefreshProjectCompetency")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProjectCompetency>> RefreshProjectCompetency(List<RefreshProjectCompetencyRequestDTO> requestDto)
        {
            try
            {
                RefreshProjectCompetencyCommand request = new RefreshProjectCompetencyCommand
                {
                    competencyRequest = requestDto
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }

        }

        [HttpPost("CreateProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProjectResponse>> CreateProject([FromBody] CreateProjectCommand command)
        {
            try
            {
                var result = await _mediator.Send(new CreateProjectCommand()
                {
                    //ProjectCode = command.ProjectCode,
                    //ProjectName = command.ProjectName,
                    PipelineName = command.PipelineName,
                    JobName = command.JobName,
                    JobCode = command.JobCode,
                    PipelineCode = command.PipelineCode,
                    ClientName = command.ClientName,
                    Expertise = command.Expertise,//Recheck
                    SME = command.SME,//Recheck
                    SMEG = command.SMEG,//Recheck

                    Offerings = command.Offerings,
                    Solutions = command.Solutions,
                    OfferingsId = command.OfferingsId,
                    SolutionsId = command.SolutionsId,

                    StartDate = command.StartDate,
                    EndDate = command.EndDate,
                    Description = command.Description,
                    PipelineStatus = command.PipelineStatus,
                    AllocationStatus = Constants.AllocationStatus.Open.ToString(),
                    ResourceRequestor = command.ResourceRequestor,
                    ProjectRoles = command.ProjectRoles
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetProjectByCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ValidateAntiForgeryToken]
        [SanitizeInput]
        public async Task<Project> GetProjectByCode([FromQuery] GetProjectByCodeQuery getProjectByCodeQuery)
        {
            try
            {
                var request = new GetProjectByCodeQuery() { pipelineCode = getProjectByCodeQuery.pipelineCode, jobCode = getProjectByCodeQuery.jobCode };
                Project result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetProjectDetailsForRequestor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ValidateAntiForgeryToken]
        [SanitizeInput]
        public async Task<ProjectDetailsRequestorDto> GetProjectDetailsForRequestor(string pipelineCode, string? jobCode)
        {
            try
            {
                var request = new GetProjectDetailsForRequestorQuery() { PipelineCode = pipelineCode, JobCode = jobCode };
                ProjectDetailsRequestorDto result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetProjectDetailsForEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ProjectDetailsEmployeeDto> GetProjectDetailsForEmployee(string pipelineCode, string? jobCode)
        {
            try
            {
                var request = new GetProjectDetailsForEmployeeQuery() { PipelineCode = pipelineCode, JobCode = jobCode };
                ProjectDetailsEmployeeDto result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetProjectDetailsAsPerPipelineCodeAndUserRole")]
        //[ValidateAntiForgeryToken]
        [SanitizeInput]
        public async Task<GetProjectDetailsByUserRole> GetProjectDetailsAsPerPipelineCodeAndUserRole([FromQuery] string PipelineCode, string? JobCode)
        {
            try
            {
                var user = _userAccessor.GetUser();
                var request = new GetProjectDetailsAsPerPipelineCodeAndUserRoleQuery()
                {
                    PipelineCode = PipelineCode,
                    JobCode = JobCode,
                    UserEmail = user.email,
                    ApplicationRoles = user.roles != null ? user.roles.ToList() : new List<string>()
                };
                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetProjectFilterOptionsQuery")]
        public async Task<ProjectFilterOptionsResponse> GetProjectFilterOptions()
        {
            try
            {
                var user = _userAccessor.GetUser();
                var request = new GetProjectFilterOptionsQuery()
                {
                    UserEmail = user != null ? user.email : null,
                    userDecorator = user,
                };
                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("AddSuperCoachProjectRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> AddSuperCoachProjectRole(AddSuperCoachProjectRoleRequestDTO requestDto)
        {
            try
            {
                AddSuperCoachProjectRoleCommand cmd = new()
                {
                    request = requestDto
                };
                return await _mediator.Send(cmd);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return false;
            }
        }

        /// <summary>
        /// Get Project data for employee listing view
        /// </summary>
        /// <param name="pipelineCodes"></param>
        /// <returns></returns>
        [HttpPost("GetEmployeeListingProjectDataByPipelineCodes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<EmployeeListingProjectData>> GetEmployeeListingProjectDataByPipelineCodes(IEnumerable<KeyValuePair<string, string>> pipelineCodes)
        {
            try
            {
                var request = new GetEmployeeListingProjectDataQuery() { codes = pipelineCodes };
                List<EmployeeListingProjectData> result = await _mediator.Send(request);
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateProjectRollOverStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProjectResponse>> UpdateProjectRollOverStatus([FromBody] UpdateProjectRolledOverCommand command)
        {
            try
            {
                var result = await _mediator.Send(new UpdateProjectRolledOverCommand()
                {
                    PipelineCode = command.PipelineCode,
                    JobCode = command.JobCode,
                    IsRollover = command.IsRollover,
                    RolloverDays = command.RolloverDays,
                    ModifiedBy = command.ModifiedBy,
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetAllEmployeesExcludingEmployessWithRoles")]
        public async Task<List<EmployeeResponseDTO>> GetAllEmployeesExcludingEmployessWithRolesInProject([FromBody] GetEmployeesListWithMatchQueryRequestDTO args)
        {
            try
            {
                var response = await _mediator.Send(new GetEmployeesListWithMatchQuery()
                {
                    InputEmail = args.InputEmail,
                    PipelineCode = args.PipelineCode,
                    JobCode = args.JobCode,
                    UsersNotToInclude = args.UsersNotToInclude
                });
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateProjectSuspensionStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProjectResponse>> UpdateProjectSuspensionStatus([FromBody] UpdateProjectSuspensionStatusQuery command)
        {
            try
            {
                var result = await _mediator.Send(new UpdateProjectSuspensionStatusQuery()
                {
                    ProjectCodes = command.ProjectCodes,
                    IsSuspended = command.IsSuspended,
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetRolesEmailByPipelineCodesAndRoles")]
        public async Task<List<RoleEmailsByPipelineCodeResponse>> GetRolesEmailByPipelineCodesAndRoles([FromBody] List<PipelineCodeAndRolesDto> pipelineCodeAndRolesDto)
        {
            try
            {
                var result = new GetRoleEmailsByPipelineCodeQuery
                {
                    PipelineCodeAndRolesDto = pipelineCodeAndRolesDto
                };
                var response = await _mediator.Send(result);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetAppRolesEmailByPipelineCodes")]
        public async Task<List<RoleEmailsByPipelineCodeResponse>> GetAppRolesEmailByPipelineCodes([FromBody] List<PipelineCodeAndRolesDto> pipelineCodeAndRolesDto)
        {
            try
            {
                var result = new GetAppRolesEmailByPipelineCodesQuery
                {
                    PipelineCodeAndRolesDto = pipelineCodeAndRolesDto
                };
                var response = await _mediator.Send(result);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetEmployeeRoleByByPipelineJobCodes")]
        public async Task<List<RoleEmailsByPipelineCodeResponse>> GetEmployeeRoleByByPipelineJobCodes([FromBody] List<PipelineCodeAndRolesDto> pipelineCodeJobDto)
        {
            try
            {
                var result = new GetEmployeeRoleByByPipelineJobCodesQuery
                {
                    PipelineCodeAndRolesDto = pipelineCodeJobDto
                };

                var response = await _mediator.Send(result);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetResourceReviewerEmailsByPipelineCode")]
        public async Task<List<ProjectRolesResponseDTO>> GetResourceReviewerEmailByPipelineCode(string pipelineCode, string? jobCode)
        {
            try
            {
                var result = await _mediator.Send(new GetResourceReviewersEmailByCodeQuery()
                {
                    PipelineCode = pipelineCode,
                    JobCode = jobCode,
                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetAllProjectRolesByCodes")]
        public async Task<List<ProjectRolesResponseDTO>> GetAllProjectRolesByCodes([FromBody] PipelineCodeAndRolesDto pipelineCodeJobDto)
        {
            try
            {
                var result = new GetAllProjectRolesByCodesQuery
                {
                    PipelineCodeAndRolesDto = pipelineCodeJobDto
                };

                var response = await _mediator.Send(result);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetResourceRequestorEmailsByPipelineCode")]
        public async Task<List<ProjectRolesResponseDTO>> GetResourceRequestorEmailByPipelineCode(string pipelineCode, string? jobCode)
        {
            try
            {
                var result = await _mediator.Send(new GetResourceRequestorEmailByPipelineCodeQuery()
                {
                    PipelineCode = pipelineCode,
                    JobCode = jobCode,
                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetRequestorEmailsForAllocationWorkflow")]
        public async Task<List<ProjectRolesResponseDTO>> GetRequestorEmailsForAllocationWorkflow([FromQuery] string pipelineCode, [FromQuery] string? jobCode, [FromQuery] string workflowStartedBy)
        {
            try
            {
                var result = await _mediator.Send(new GetRequestorEmailsForAllocationWorkflowQuery()
                {
                    PipelineCode = pipelineCode,
                    JobCode = jobCode,
                    WorkflowStartedBy = workflowStartedBy
                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetResourceRequestorEmailsListByPipelineCode")]
        public async Task<List<GetResouceRequestorEmailListByPipelineCodesResponse>> GetResourceRequestorEmailListByPipelineCodes(List<KeyValuePair<string, string?>> pipelineCodes)
        {
            try
            {
                var result = await _mediator.Send(new GetResouceRequestorEmailListByPipelineCodesQuery()
                {
                    PipelineCodes = pipelineCodes
                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetMultipleProjectsByCodes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Project>> GetMultipleProjectsByCodes([FromBody] List<KeyValuePair<string, string>> projectCode)
        {
            try
            {
                var request = new GetMultipleProjectsByCodesQuery() { ProjectCode = projectCode };
                List<Project> result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("SetIsRequisitionCreationallowed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProjectResponse>> SetIsRequisitionCreationAllowed([FromBody] SetIsRequisitionCreationAllowedCommand command)
        {
            try
            {
                var result = await _mediator.Send(new SetIsRequisitionCreationAllowedCommand()
                {
                    IsRequisitionCreationallowed = command.IsRequisitionCreationallowed,
                    //ProjectCode = command.ProjectCode
                    PipelineCode = command.PipelineCode,
                    JobCode = command.JobCode,
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("SetPublishedToMarketPlace")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProjectResponse>>> SetPublishedToMarketPlace([FromBody] List<UpdatePublishedToMarketPlaceDTO> unpublishToMarketplaceDto)
        {
            try
            {
                var result = await _mediator.Send(new UpdatePublishedToMarketPlaceCommand()
                {
                    updatePublishedToMarketPlaceDTO = unpublishToMarketplaceDto
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetActiveFieldForMarketPlace")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<FieldForMarketPlace>> GetActiveFieldForMarketPlace()
        {
            try
            {
                var request = new GetFieldForMarketPlaceQuery();
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("CreateOrUpdateActiveFieldForMarketPlace")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<FieldForMarketPlace>> CreateOrUpdateActiveFieldForMarketPlace([FromBody] CreateOrUpdateActiveFieldForMarketPlaceCommand command)
        {
            try
            {
                var result = await _mediator.Send(new CreateOrUpdateActiveFieldForMarketPlaceCommand()
                {
                    DisplayName = command.DisplayName,
                    IsActive = command.IsActive,
                    InternalName = command.InternalName,
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetPublishedFieldByprojectCodeForMarketPlace")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SanitizeInput]
        public async Task<List<PublishedFieldForMarketPlace>> GetPublishedFieldByprojectCodeForMarketPlace(string pipelineCode, string? jobCode)
        {
            try
            {
                var request = new GetPublishedFieldForMarketPlaceQuery() { PipelineCode = pipelineCode, JobCode = jobCode };
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("CreateOrUpdatePublishedFieldForMarketPlace")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PublishedFieldForMarketPlace>> CreateOrUpdatePublishedFieldForMarketPlace([FromBody] CreateOrUpdatePublishedFieldForMarketPlaceCommand command)
        {
            try
            {
                var result = await _mediator.Send(new CreateOrUpdatePublishedFieldForMarketPlaceCommand()
                {
                    //ProjectCode = command.ProjectCode,
                    PipelineCode = command.PipelineCode,
                    JobCode = command.JobCode,
                    FieldNameList = command.FieldNameList,
                    IsActiveList = command.IsActiveList,
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetProjectBudget")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProjectBudgetDto>> GetProjectBudget([FromQuery] string pipelineCode, string? jobCode)
        {
            try
            {
                var request = new GetProjectBudgetQuery() { JobCode = jobCode, PipelineCode = pipelineCode };
                List<ProjectBudgetDto> result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("AddUpdateProjectRequisitionAllocationCommand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> AddUpdateProjectRequisitionAllocation([FromBody] List<ProjectRequisitionAllocationRequestDTO> projectRequisitionAllocationRequestDTOs)
        {
            try
            {
                return await _mediator.Send(new AddUpdateProjectRequisitionAllocationCommand
                {
                    projectRequisitionAllocationRequestDTOs = projectRequisitionAllocationRequestDTOs,
                    updatedBy = "System"
                });
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return false;
            }
        }

        [HttpPost("UpdateProjectBudgetStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<ProjectResponse>> UpdateProjectBudgetStatus([FromBody] List<UpdateBudgetStatusDTO> projectRequest)
        {
            try
            {
                var request = new UpdateProjectBudgetStatusCommand() { projects = projectRequest };
                List<ProjectResponse> result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// AddJustification
        /// </summary>
        /// <param name="justtificationRequest"></param>
        /// <returns></returns>
        [HttpPost("AddJustification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ProjectResponse> AddJustification(JustificationTextDto justtificationRequest)
        {
            try
            {
                var request = new AddJustificationCommand()
                {
                    JustificationText = justtificationRequest.JustificationText,
                    //ProjectCode = justtificationRequest.ProjectCode
                    PipelineCode = justtificationRequest.PipelineCode,
                    JobCode = justtificationRequest.JobCode,
                };
                ProjectResponse result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetAllUsersExcludingEmployessWithRoles")]
        public async Task<List<EmployeeResponseDTO>> GetAllUsersExcludingEmployessWithRolesInProject([FromBody] GetAllUsersListWithMatchRequestDTO args)
        {
            try
            {
                UserDecorator user = _userAccessor.GetUser();
                var response = await _mediator.Send(new GetAllUsersListWithMatchQuery()
                {
                    InputEmail = args.InputEmail,
                    PipelineCode = args.PipelineCode,
                    JobCode = args.JobCode,
                    CurrentUserRoles = args.CurrentUserRoles,
                    EmailId = user.email,
                    usersNotToInclude = args.UsersNotToInclude
                });
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// ChangeCodeForProject
        /// </summary>
        /// <returns></returns>
        [HttpPost("ChangeCodeForProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ChangeCodeResponseDTO> ChangeCodeForProject([FromBody] ChangeCodeDTO changeCodeDTO)
        {
            try
            {
                var user = _userAccessor.GetUser();
                var request = new ChangeCodeForProjectCommand
                {
                    changeProjectCodeDTO = changeCodeDTO,
                    UserEamil = user.email,
                };
                ChangeCodeResponseDTO res = await _mediator.Send(request);
                return res;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }
        [HttpGet("ProjectActualBudgetOverShoot")]
        public async Task<List<ProjectActualBudgetResponse>> ProjectActualBudgetOverShoot()
        {
            try
            {
                ProjectActualBudgetOverShootQuery req = new();
                var result = await _mediator.Send(req);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }

        }
        /// <summary>
        /// UpdateProjectRolesForSupercoachDelegate
        /// Update the role for supercoach delegate
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("UpdateProjectRolesForSupercoachDelegate")]
        public async Task<List<ProjectRoles>> UpdateProjectRolesForSupercoachDelegate([FromBody] UpdateProjectRolesForSupercoachDelegateCommand req)
        {
            try
            {
                var result = await _mediator.Send(req);
                return result;
            }
            catch (Exception ex)
            {

                this.HandleException(ex); return null;
            }

        }

        /// <summary>
        /// GetAllJobCodesForPipelineCode
        /// </summary>
        /// <param name="pipelineCode"></param>
        /// <returns></returns>
        [HttpGet("GetAllJobCodesForPipelineCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<string>> GetAllJobCodesForPipelineCode([FromQuery] string pipelineCode, [FromQuery] string? jobCode, [FromQuery] bool? SameTeamAllocation)
        {
            try
            {
                return await _mediator.Send(
                    new GetAllJobCodesForPipelineCodeQuery
                    {
                        PipelineCode = pipelineCode,
                        JobCode = jobCode,
                        SameTeamAllocation = SameTeamAllocation
                    }
                );
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// GetProjectFullDetailsByUniqueCodes
        /// </summary>
        /// <param name="pipelineCode"></param>
        /// <returns></returns>
        [HttpGet("GetProjectFullDetailsByUniqueCodes")]
        public async Task<List<ProjectFullDetailsResponse>> GetProjectFullDetailsByUniqueCodes(List<KeyValuePair<string, string?>> projectUniqueCodes)
        {
            try
            {
                var request = new GetProjectFullDetailsByUniqueCodesQuery()
                {
                    uniqueCodes = projectUniqueCodes,
                };

                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetProjectRolesByEmails")]
        public async Task<List<GetProjectRolesByEmailsResponse>> GetProjectRolesByEmails([FromBody] List<string> emails)
        {
            try
            {
                var result = new GetProjectsRolesByEmailsQuery
                {
                    Emails = emails
                };
                var response = await _mediator.Send(result);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }


        [HttpPost("GetOfferingSolutionsByJobCode")]
        public async Task<List<GetOfferingSolutionsByJobCodeResponseDTO>> GetOfferingSolutionsByJobCode([FromBody] List<string> jobCodes)
        {
            try
            {
                return await _mediator.Send(new GetOfferingSolutionsByJobCodeQuery()
                {
                    jobCodes = jobCodes
                });
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
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
