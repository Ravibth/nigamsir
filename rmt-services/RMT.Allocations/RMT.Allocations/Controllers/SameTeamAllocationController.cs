using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMT.Allocation.API.Controllers;
using RMT.Allocation.Application.Handlers.QueryHandlers;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Infrastructure.DTOs;

namespace RMT.Allocations.API.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]

    public class SameTeamAllocationController : BaseController
    {

        private readonly IMediator _mediator;
        public SameTeamAllocationController(IMediator mediator, ILogger<BaseController> logger) : base(logger)
        {
            _mediator = mediator;
        }

        [HttpPost("GetAllocationByJobCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ResourceAllocationDetailsResponse>>> GetAllocationByJobCode([FromBody] List<string> jobCodes)
        {
            try
            {
                var request = new GetAllocationByJobCodeQuery()
                {
                    JobCodes = jobCodes
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
