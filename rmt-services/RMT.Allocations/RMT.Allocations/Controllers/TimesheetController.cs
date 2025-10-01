using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMT.Allocation.API.Controllers;
using RMT.Allocation.Application.Handlers.QueryHandlers;
using RMT.Allocation.Domain.DTO;
using RMT.Allocations.API.Services;

namespace RMT.Allocations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;
        public TimesheetController(IMediator mediator, IUserAccessor userAccessor, ILogger<BaseController> logger) : base(logger)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        [HttpGet("GetDraftTimesheet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<DraftTimesheetResponse>>> GetDraftTimesheet([FromQuery] GetDraftTimesheetQuery getDraftTimesheetQuery)
        {
            try
            {
                var request = new GetDraftTimesheetQuery()
                {
                    dates = getDraftTimesheetQuery.dates
                };
                return await _mediator.Send(request);
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
