using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMT.Allocation.API.Controllers;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.Handlers.CommandHandlers;
using RMT.Allocation.Application.Handlers.QueryHandlers;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.Entities;
using RMT.Allocations.API.Attributes;

namespace RMT.Allocations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BudgetController : BaseController
    {
        private readonly IMediator _mediator;
        public BudgetController(IMediator mediator, ILogger<BaseController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpPost("BudgetOverview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<BudgetOverviewDto>> BudgetOverview(BudgetOverviewRequest request)
        {
            try
            {
                BudgetOverviewCommand query = AllocationMapper.Mapper.Map<BudgetOverviewCommand>(request);

                var result = await _mediator.Send(query);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("BudgetDesignationWise")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SanitizeInput]
        public async Task<List<DesignationBudget>> DesignationBudget(string jobCode, string pipelineCode)
        {
            try
            {
                DesignationBudgetQuery query = new() { JobCode = jobCode, PipelineCode = pipelineCode };

                var result = await _mediator.Send(query);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("AllcoatedActualGraph")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<AllocationDayGroupResponseDTO>> ActualPlannedView(string pipelineCode, string jobCode, string timeoption, DateTime startDate, DateTime endDate)
        {
            try
            {
                ResourceAllocationDayGroupQuery query = new ResourceAllocationDayGroupQuery
                {
                    JobCode = jobCode,
                    StartDate = startDate,
                    EndDate = endDate,
                    TimeOption = timeoption,
                    PipelineCode = pipelineCode
                };
                var result = await _mediator.Send(query);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("ActualPlannedResourceView")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SanitizeInput]
        public async Task<List<AcutalAllocatedBudgetResourceDTO>> ActualPlannedResourceView(string jobCode, string pipelineCode, DateTime startDate, DateTime endDate)
        {
            try
            {
                GetAllocationDayResourceQuery query = new()
                {
                    JobCode = jobCode,
                    StartDate = startDate,
                    EndDate = endDate,
                    PipelineCode = pipelineCode
                };
                var result = await _mediator.Send(query);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("BudgetOverviewLimit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<BudgetOverviewLimit> BudgetOverviewLimit(string JobCode, string PipelineCode)
        {
            try
            {
                BudgetOverViewLimitQuery query = new BudgetOverViewLimitQuery { JobCode = JobCode, PipelineCode = PipelineCode };

                var result = await _mediator.Send(query);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateDesignationCost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<UpdateDesignationCost>> UpdateDesignationCost(UpdateDesignationCostCommand updateRequest)
        {
            try
            {
                List<UpdateDesignationCost> result = await _mediator.Send(updateRequest);
                return result;
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
