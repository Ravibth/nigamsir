using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMT.Report.Api.Services;
using RMT.Report.Application.Handlers.QueryHandlers;
using RMT.Reports.Application.DTO.Request;
using RMT.Reports.Application.DTO.Response;
using RMT.Reports.Application.Handlers.CommandHandlers;
using RMT.Reports.Application.Handlers.QueryHandlers;
using RMT.Reports.Domain;
using RMT.Reports.Domain.Entities;

namespace RMT.Report.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;

        public ReportsController(IMediator mediator, IUserAccessor userAccessor, ILogger<BaseController> logger) : base(logger)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        [HttpGet("v1/employee-allocations")]
        public async Task<List<EmployeeAllocationTimeSheetEntity>> GetAllEmployeeAllocation([FromQuery] EmployeeAllocationTimeSheetRequestDto args)
        {
            try
            {
                var request = new GetEmployeeAllocationTimeSheetQuery()
                {
                    business_unit = args.business_unit
                };
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("v1/refresh-materilized-views")]
        public async Task<Dictionary<string, bool>> RefreshMateriliezedView(Dictionary<string, bool> materializedViewNames)
        {
            try
            {
                var request = new RefreshMaterialViewCommand()
                {
                    materializedViewNames = materializedViewNames
                };
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("v1/refresh-employee-allocation")]
        public async Task<bool> RefreshEmployeeAllocation()
        {
            try
            {
                var request = new RefreshEmployeeAllocationViewCommand()
                {
                };
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return false;
            }
        }

        [HttpPost("v1/capacity-utilization-overview-chart")]
        public async Task<List<CapacityUtilizationOverviewResponseDto>> CapacityUtilizationOverviewChart([FromBody] CapacityUtiliationOverviewRequestDto args)
        {
            try
            {
                UserDecorator user = _userAccessor.GetUser();

                var request = new GetCapacityUtilizationOverviewQuery()
                {
                    args = args,
                    userDecorator = user
                };
                var resposne = await _mediator.Send(request);
                return resposne;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("v1/scheduled-vs-variance-chart")]
        public async Task<List<ScheduledVsActualVarianceChartResponseDto>> ScheduledVsActualVarianceChart([FromBody] ScheduledVsActualVarianceChartRequestDto args)
        {
            try
            {
                UserDecorator user = _userAccessor.GetUser();
                var request = new ScheduledVsActualVarianceChartQuery()
                {
                    args = args,
                    userDecorator = user
                };
                var resposne = await _mediator.Send(request);
                return resposne;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("v1/summary-statistics-chart")]
        public async Task<SummaryStatisticsChartResponseDto> SummaryStatisticsChart([FromBody] SummaryStatisticsChartRequestDto args)
        {
            try
            {
                var request = new SummaryStatisticsChartQuery()
                {
                    args = args
                };
                var resposne = await _mediator.Send(request);
                return resposne;
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
