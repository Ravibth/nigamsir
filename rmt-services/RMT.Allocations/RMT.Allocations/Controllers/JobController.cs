using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMT.Allocation.API.Controllers;
using RMT.Allocation.Application.Handlers.CommandHandlers;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Application.Handlers.QueryHandlers;
using RMT.Allocations.API.Services;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;

namespace RMT.Allocations.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class JobController : BaseController
  {
    private readonly IMediator _mediator;
    private readonly IUserAccessor _userAccessor;
    public JobController(IMediator mediator, IUserAccessor userAccessor, ILogger<BaseController> logger) : base(logger)
    {
      _mediator = mediator;
      _userAccessor = userAccessor;
    }

    [HttpPost("GetJobAllocationMapping")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<JobAllocationMappingDTO>> GetJobAllocationMapping([FromBody] GetJobAllocationMappingQuery inputDto)
    {
      try
      {
        //var userInfo = _userAccessor.GetUser();
        GetJobAllocationMappingQuery query = new GetJobAllocationMappingQuery()
        {
          AllocationConfirmedDate = inputDto.AllocationConfirmedDate,
          //BearerToken = _userAccessor.GetToken(),
          //GetProjectRoles = false,
          //GetEmployeeIds = true,
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


    [HttpPost("GetConfirmedPerDayHoursByDate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<EmployeePerDayAllocationDto>> GetConfirmedPerDayHoursByDate([FromBody] GetConfirmedPerDayHoursByDateQuery inputDto)
    {
      try
      {
        //var userInfo = _userAccessor.GetUser();
        GetConfirmedPerDayHoursByDateQuery query = new()
        {
          StartDate = inputDto.StartDate,
          EndDate = inputDto.EndDate,
          GetEmployeeIds = true,
          GetClientIds = true,
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




    /*
     Input: (Datetime collection)
    [
      "2024-02-08"
    ]

    OutPut:

    {
        "pipelineCode": "string",
        "jobCode": "string",
        "emp": "string",
        "allocationConfirmationDate": "2024-02-08T17:17:03.461Z",
        "projectRoles": {
          "roleName1": [
            "emp"
          ],
          "roleName2": [
            "emp"
          ],
          "roleName3": [
            "emp"
          ]
        }
      }
     */
  }
}
