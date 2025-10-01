using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMT.Allocation.API.Controllers;
using RMT.Allocation.Application.DTOs.RequisitionDTOs;
using RMT.Allocation.Application.Handlers.CommandHandlers;
using RMT.Allocation.Application.Handlers.QueryHandlers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocations.API.Attributes;
using RMT.Allocations.API.Services;
using static RMT.Allocation.Domain.ConstantsDomain;

namespace RMT.Allocations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequisitionController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;
        public RequisitionController(IMediator mediator, IUserAccessor userAccessor, ILogger<BaseController> logger) : base(logger)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        [HttpGet("GetRequisitionDetailsByRequisitionId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SanitizeInput]
        public async Task<ActionResult<RequisitionResponse>> GetRequisitionDetailsByRequisitionId([FromQuery] GetRequisitionDetailsByRequisitionIdQuery getRequisitionDetailsByRequisitionIdQuery)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var request = new GetRequisitionDetailsByRequisitionIdQuery()
                {
                    requisitionId = getRequisitionDetailsByRequisitionIdQuery.requisitionId,
                    isRequsitionFilterByProjectRoles = getRequisitionDetailsByRequisitionIdQuery.isRequsitionFilterByProjectRoles,
                    userEmail = userInfo == null ? null : userInfo.email,
                    UserInfo = userInfo
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)

            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetAllRequisitionByProjectCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ValidateAntiForgeryToken]
        [SanitizeInput]
        public async Task<List<GetAllRequisitionByProjectCodeResponse>> GetAllRequisitionByProjectCode([FromQuery] GetAllRequisitionByProjectCodeQuery getAllRequisitionByProjectCodeQuery)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var request = new GetAllRequisitionByProjectCodeQuery()
                {
                    PipelineCode = getAllRequisitionByProjectCodeQuery.PipelineCode,
                    JobCode = getAllRequisitionByProjectCodeQuery.JobCode == Requisition_Parameter_type.Null ? null : getAllRequisitionByProjectCodeQuery.JobCode,
                    limit = getAllRequisitionByProjectCodeQuery.limit,
                    pagination = getAllRequisitionByProjectCodeQuery.pagination,
                    currentEmp = getAllRequisitionByProjectCodeQuery.currentEmp,
                    UserEmail = userInfo == null ? null : userInfo.email,
                    ScoreCalculationForRequisitionIdsAllowed = getAllRequisitionByProjectCodeQuery.ScoreCalculationForRequisitionIdsAllowed,
                    IsRequsitionFilterByProjectRoles = getAllRequisitionByProjectCodeQuery.IsRequsitionFilterByProjectRoles,
                    UserInfo = userInfo

                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetAllRequisitionByProjectCodeForProjectDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SanitizeInput]
        public async Task<List<GetAllRequisitionByProjectCodeResponse>> GetAllRequisitionByProjectCodeForProjectDetails([FromQuery] GetAllRequisitionByProjectCodeForProjectDetailsQuery getAllRequisitionByProjectCodeQuery)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var request = new GetAllRequisitionByProjectCodeForProjectDetailsQuery()
                {
                    PipelineCode = getAllRequisitionByProjectCodeQuery.PipelineCode,
                    JobCode = getAllRequisitionByProjectCodeQuery.JobCode == Requisition_Parameter_type.Null ? null : getAllRequisitionByProjectCodeQuery.JobCode,
                    limit = getAllRequisitionByProjectCodeQuery.limit,
                    pagination = getAllRequisitionByProjectCodeQuery.pagination,
                    currentEmp = getAllRequisitionByProjectCodeQuery.currentEmp,
                    UserEmail = userInfo == null ? null : userInfo.email,
                    ScoreCalculationForRequisitionIdsAllowed = getAllRequisitionByProjectCodeQuery.ScoreCalculationForRequisitionIdsAllowed,
                    IsRequsitionFilterByProjectRoles = getAllRequisitionByProjectCodeQuery.IsRequsitionFilterByProjectRoles,
                    UserInfo = userInfo
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("IsRequisitionExistsInProjectCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Boolean> IsRequisitionExistsInProjectCode([FromQuery] IsRequistionExistsQuery request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return false;
            }
        }
        //Post Api to create requisition

        [HttpPost("SubmitRequisitionForProjectCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<RequisitionRequest>> SubmitRequisitionForProjectCode([FromBody] CreateRequisitionDTO createRequisitionDTO)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var token = _userAccessor.GetToken();
                var result = await _mediator.Send(new CreateRequisitionCommand()
                {
                    PipelineCode = createRequisitionDTO.PipelineCode,
                    JobCode = createRequisitionDTO.JobCode,
                    PipelineName = createRequisitionDTO.PipelineName,
                    JobName = createRequisitionDTO.JobName,
                    RequisitionDescription = createRequisitionDTO.Description,
                    BusinessUnit = createRequisitionDTO.BU,
                    Solutions = createRequisitionDTO.Solutions,
                    Offerings = createRequisitionDTO.Offerings,
                    Designation = createRequisitionDTO.Designation,
                    Grade = createRequisitionDTO.Grade,
                    Description = createRequisitionDTO.Description,
                    ResourceEntities = createRequisitionDTO.ResourceEntities,
                    NumberOfResources = createRequisitionDTO.NumberOfResources,
                    IsAllResourcesSimilar = createRequisitionDTO.IsAllResourcesSimilar,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow,
                    CreatedBy = userInfo.email,
                    ModifiedBy = userInfo.email,
                    ClientName = createRequisitionDTO.ClientName,
                    Token = token
                }); ;
                return Ok(result[0]);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("DeleteRequisitionById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<DeleteRequisitionResponse> DeleteRequisitionById([FromBody] DeleteRequistionDTO deleteRequisitionDTO)
        {
            try
            {

                var res = await _mediator.Send(new DeleteRequisitionCommand
                {
                    Id = deleteRequisitionDTO.Id,
                });
                return res;

            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;

            }
        }

        [HttpPut("UpdateRequisition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<RequisitionResponse>> UpdateRequisition([FromBody] UpdateRequisitionDTO updateRequisitionDTO)
        {
            var userInfo = _userAccessor.GetUser();
            try
            {
                var result = await _mediator.Send(new UpdateRequisitionCommand
                {
                    ResourceEntities = updateRequisitionDTO.ResourceEntities,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = userInfo.email,
                    token = _userAccessor.GetToken(),
                    userInfo = userInfo
                }); ;
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetRequisitionDetailsByDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SanitizeInput]
        public async Task<ActionResult<List<RequisitionResponse>>> GetRequisitionDetailsByDate([FromQuery] GetRequisitionDetailsByDateQuery requistionRequest)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var request = new GetRequisitionDetailsByDateQuery()
                {
                    ModifiedAt = requistionRequest.ModifiedAt,
                    CreatedAt = requistionRequest.CreatedAt
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)

            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetPublishAllocationDetailsByDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PublishAllocationResponse>>> GetPublishAllocationDetailsByDate([FromQuery] GetRequisitionDetailsByDateQuery requistionRequest)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var request = new GetPublishAllocationDetailsByDateQuery()
                {
                    ModifiedAt = requistionRequest.ModifiedAt,
                    CreatedAt = requistionRequest.CreatedAt
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
