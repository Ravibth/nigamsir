using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMT.Allocation.API.Controllers;
using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;
using RMT.Allocation.Application.Handlers.QueryHandlers;
using RMT.Allocation.Domain.DTO;

namespace RMT.Allocations.API.Controllers
{

        [Route("api/[controller]")]
        [Microsoft.AspNetCore.Mvc.ApiController]
        public class ResourceUnvailabilityController : BaseController
        {
                private readonly IMediator _mediator;
                public ResourceUnvailabilityController(IMediator mediator, ILogger<BaseController> logger) : base(logger)
                {
                        _mediator = mediator;
                }


                //[HttpGet("GetTrainingDetailsForEmp")]
                //[ProducesResponseType(StatusCodes.Status200OK)]
                //public async Task<ActionResult<Dictionary<string, List<TrainingDetailsDTO>>>> GetTrainingDetailsForEmp([FromQuery] List<string> emailId)
                //{
                //    try
                //    {
                //        var request = new GetEmpTrainingDetailsHandlerQuery()
                //        {
                //            EmailId = emailId
                //        };
                //        return await _mediator.Send(request);
                //    }
                //    catch (Exception ex)
                //    {
                //        this.HandleException(ex); return null;
                //    }
                //}

                //Merged in leave
                //[HttpGet("GetHolidayDetailsForEmp")]
                //[ProducesResponseType(StatusCodes.Status200OK)]
                //public async Task<ActionResult<Dictionary<string, List<HolidayDetailsDTO>>>> GetHolidayDetailsForEmp([FromQuery] List<string> emailId)
                //{
                //    try
                //    {
                //        var request = new GetHolidayDetailsQueryHandlerQuery()
                //        {
                //            EmailId = emailId
                //        };
                //        return await _mediator.Send(request);
                //    }
                //    catch (Exception ex)
                //    {
                //        this.HandleException(ex); return null;
                //    }
                //}
        }
}
