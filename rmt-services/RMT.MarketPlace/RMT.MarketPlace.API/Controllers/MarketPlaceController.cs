using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMT.MarketPlace.API.Attributes;
using RMT.MarketPlace.API.Services;
using RMT.MarketPlace.Application.DTO;
using RMT.MarketPlace.Application.Handlers.CommandHandlers;
using RMT.MarketPlace.Application.Handlers.QueryHandlers;
using RMT.MarketPlace.Application.Responses;
using RMT.MarketPlace.Domain.DTOs.Response;
using RMT.MarketPlace.Domain.Entities;
using System.Text.Encodings.Web;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RMT.MarketPlace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketPlaceController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;

        public MarketPlaceController(IMediator mediator, IUserAccessor userAccessor, ILogger<BaseController> logger) : base(logger)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        [HttpPost("SubmitEmpProjectInterest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmpProjectInterestResponse>> SubmitEmpProjectInterest([FromBody] EmpProjectInterestCommand command)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var result = await _mediator.Send(new EmpProjectInterestCommand()
                {
                    MarketPlaceId = command.MarketPlaceId,
                    IsInterested = command.IsInterested,
                    InterestDate = command.InterestDate,
                    EmpEmail = command.EmpEmail,
                    EmpName = command.EmpName,
                    IsActive = command.IsActive,
                    CreatedBy = userInfo.email,
                    ModifiedBy = userInfo.email,
                });

                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("CreateOrUpdateEmpProjectInterestScore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<EmpProjectInterestScoreResponse>>> CreateOrUpdateEmpProjectInterestScore([FromBody] List<EmpProjectInterestScoreRequest> command)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var result = await _mediator.Send(new EmpProjectInterestScoreCommand()
                {
                    allRequistion = command,
                    UserInfo = userInfo
                }); ;

                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }
        [HttpPost("RefreshEmpProjectInterestScore")]
        public async Task<string> RefreshEmpProjectInterestScore(RefreshEmpProjectInterestScoreRequestDTO request)
        {
            try
            {
                List<KeyValuePair<string, string?>> PipelineJobCodes = new();
                KeyValuePair<string, string> pair1 = new(request.PipelineCode, string.IsNullOrEmpty(request.JobCode) ? null : request.JobCode);
                PipelineJobCodes.Add(pair1);
                RefreshEmpProjectInterestScoreCommand req = new RefreshEmpProjectInterestScoreCommand()
                {
                    PipelineJobCodes = PipelineJobCodes,
                    userDecorator = null,
                    RequisitionActionType = request.RequisitionActionType,
                };
                var response = await _mediator.Send(req);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetAllEmpProjectInterestScore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SanitizeInput]
        public async Task<List<EmpProjectInterestScoreDTO>> GetAllEmpProjectInterestScoreByPipelineCode([FromQuery] GetAllEmpProjectInterestScoreByPipelineCodeQuery command)
        {
            try
            {
                var request = new GetAllEmpProjectInterestScoreByPipelineCodeQuery()
                {
                    PipelineCode = command.PipelineCode,
                    JobCode = command.JobCode,
                };
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }

        }

        [HttpPost("GetAllProjectDetailsForMarketPlace")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SanitizeInput]
        public async Task<List<MarketPlaceProjectDetailDTO>> GetAllProjectDetailsForMarketPlace([FromBody] GetAllProjectDetailsForMarketPlaceCommand command)
        {
            try
            {
                var request = new GetAllProjectDetailsForMarketPlaceCommand()
                {
                    limit = command.limit,
                    pagination = command.pagination,
                    currentDateValue = command.currentDateValue.HasValue ? command.currentDateValue.Value.Date : null,
                    showLiked = command.showLiked,
                    emailId = string.IsNullOrEmpty(command.emailId) ? string.Empty : (command.emailId.Trim()),
                    buFiltervalue = (command.buFiltervalue),

                    offeringsFiltervalue = (command.offeringsFiltervalue),
                    solutionsFiltervalue = (command.solutionsFiltervalue),

                    industryFiltervalue = (command.industryFiltervalue),
                    subIndustryFiltervalue = (command.subIndustryFiltervalue),
                    locationFiltervalue = (command.locationFiltervalue),
                    isAllocatedToProject = command.isAllocatedToProject,
                    startDateFiltervalue = command.startDateFiltervalue?.ToLocalTime(),
                    endDateFiltervalue = command.endDateFiltervalue?.ToLocalTime(),
                    selectedValueForSorting = (command.selectedValueForSorting),
                    orderBy = command.orderBy,
                };
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetAllRequisitionByProjectCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SanitizeInput]
        public async Task<List<GetAllRequisitionByProjectCodeResponse>> GetAllRequisitionByProjectCode([FromQuery] GetAllRequisitionByProjectCodeQuery getAllRequisitionByProjectCodeQuery)
        {
            try
            {
                var request = new GetAllRequisitionByProjectCodeQuery()
                {
                    //id is marketplaceRecordID
                    id = getAllRequisitionByProjectCodeQuery.id,
                    limit = getAllRequisitionByProjectCodeQuery.limit,
                    pagination = getAllRequisitionByProjectCodeQuery.pagination,
                    currentEmp = getAllRequisitionByProjectCodeQuery.currentEmp,
                    ScoreCalculationForRequisitionIdsAllowed = getAllRequisitionByProjectCodeQuery.ScoreCalculationForRequisitionIdsAllowed,
                    IsRequsitionFilterByProjectRoles = getAllRequisitionByProjectCodeQuery.IsRequsitionFilterByProjectRoles
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetAllMarketPlaceFilters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SanitizeInput]
        public async Task<MarketPlaceFiltersDTO> GetAllMarketPlaceFilters([FromQuery] GetAllMarketPlaceFiltersQuery query)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var request = new GetAllMarketPlaceFiltersQuery()
                {
                };
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("AddProjectToMarketPlace")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MarketPlaceProjectDetailResponse>> AddProjectToMarketPlace([FromBody] AddProjectToMarketPlaceCommand command)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var result = await _mediator.Send(new AddProjectToMarketPlaceCommand()
                {
                    JobCode = command.JobCode,
                    JobName = command.JobName,

                    PipelineCode = command.PipelineCode,
                    PipelineName = command.PipelineName,
                    ClientName = command.ClientName,
                    ClientGroup = command.ClientGroup,
                    StartDate = command.StartDate.Value.Date,
                    EndDate = command.EndDate.Value.Date,
                    Description = command.Description,
                    IsPublishedToMarketPlace = true,

                    CreatedDate = command.CreatedDate,
                    CreatedBy = userInfo.email,
                    ModifiedDate = command.ModifiedDate,
                    ModifiedBy = userInfo.email,
                    IsActive = command.IsActive,
                    JsonData = command.JsonData,
                    ChargableType = command.ChargableType,
                    Location = command.Location,
                    BusinessUnit = command.BusinessUnit,
                    BUId = command.BUId,
                    Offerings = command.Offerings,
                    OfferingsId = command.OfferingsId,
                    Solutions = command.Solutions,
                    SolutionsId = command.SolutionsId,

                    Industry = command.Industry,
                    Subindustry = command.Subindustry,
                    Csp = command.Csp,
                    ProposedCsp = command.ProposedCsp,
                    ElForJob = command.ElForJob,
                    ElForPipeLine = command.ElForPipeLine,
                    JobManager = command.JobManager,
                    IsInterested = command.IsInterested,
                    MarketPlaceExpirationDate = command.MarketPlaceExpirationDate.Value.ToLocalTime(),
                    IspipeLine = command.IspipeLine,
                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetListOfUsersInterestedByPipelineCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SanitizeInput]
        public async Task<List<string>> GetListOfUsersInterestedByPipelineCode([FromQuery] string pipelineCode, [FromQuery] string? jobCode)
        {
            try
            {
                var request = new GetListOfUsersInterestedByPipelineCodeQuery()
                {
                    pipelineCode = pipelineCode,
                    jobCode = jobCode
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetProjectDetailsByPipelineCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SanitizeInput]
        public async Task<MarketPlaceProjectDetail> GetProjectDetailsByPipelineCode([FromQuery] string pipelineCode, [FromQuery] string? jobCode)
        {
            try
            {
                var request = new GetProjectDetailsByPipelineCodeQuery()
                {
                    pipelineCode = pipelineCode,
                    jobCode = jobCode
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateMarketPlaceProjectDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MarketPlaceProjectDetailResponse>> UpdateMarketPlaceProjectDetails([FromBody] UpdateMarketPlaceProjectCommand command)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();

                var cmd = new UpdateMarketPlaceProjectCommand()
                {
                    PipelineCode = command.PipelineCode,
                    JobCode = command.JobCode,

                    JobName = command.JobName,
                    PipelineName = command.PipelineName,
                    ClientName = command.ClientName,
                    ClientGroup = command.ClientGroup,
                    StartDate = command.StartDate,
                    EndDate = command.EndDate,
                    Description = command.Description,

                    ModifiedDate = command.ModifiedDate,
                    ModifiedBy = userInfo.email,
                    ChargableType = command.ChargableType,
                    Location = command.Location,
                    BusinessUnit = command.BusinessUnit,

                    BUId = command.BUId,
                    Offerings = command.Offerings,
                    OfferingsId = command.OfferingsId,
                    Solutions = command.Solutions,
                    SolutionsId = command.SolutionsId,

                    Industry = command.Industry,
                    Subindustry = command.Subindustry,
                };

                var result = await _mediator.Send(cmd);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateExpiredMarketPlaceProjects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MarketPlaceProjectDetailResponse>>> UpdateExpiredMarketPlaceProjects([FromBody] UpdateExpiredMarketPlaceProjectsCommand command)
        {
            try
            {
                var cmd = new UpdateExpiredMarketPlaceProjectsCommand()
                {
                    ExpiryDate = command.ExpiryDate,
                    DaysAdjustment = (int)(command.DaysAdjustment == null ? 0 : command.DaysAdjustment),
                };

                var result = await _mediator.Send(cmd);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }
        [HttpGet("GetProjectListedInMarketplaceByProjectListingDate")]
        public async Task<List<MarketPlaceProjectDetailDTO>> GetProjectListedInMarketplaceByProjectListingDate([FromQuery] ProjectListedInMarketPlaceByListingDateQuery args)
        {
            try
            {
                var cmd = new ProjectListedInMarketPlaceByListingDateQuery()
                {
                    MarketPlacePublishDate = args.MarketPlacePublishDate,
                };
                var result = await _mediator.Send(cmd);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet("GetAllMarketPlaceProjectDetailsIntrest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<MarketPlaceProjectDetaillsIntrestDTO>> GetAllMarketPlaceProjectDetailsIntrest()
        {
            try
            {
                var request = new GetMarketPlaceProjectDetailsIntrestQuery()
                {
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// SanitizeInputData
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SanitizeInputData(string? str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string strEncode = HtmlEncoder.Default.Encode(str);
                return strEncode;
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// SanitizeInputData
        /// </summary>
        /// <param name="coll"></param>
        /// <returns></returns>
        public static List<string> SanitizeInputData(List<string> coll)
        {
            if (coll != null && coll.Count > 0)
            {
                for (int i = 0; i < coll.Count; i++)
                {
                    if (!string.IsNullOrEmpty(coll[i]))
                    {
                        coll[i] = HtmlEncoder.Default.Encode(coll[i]);
                    }
                }
            }
            return coll;
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
