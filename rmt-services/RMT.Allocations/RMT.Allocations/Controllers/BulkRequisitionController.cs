using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMT.Allocation.API.Controllers;
using RMT.Allocation.Application.DTOs.RequisitionDTOs;
using RMT.Allocation.Application.Handlers.CommandHandlers;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Infrastructure;
using RMT.Allocations.API.Services;
using System.Collections.Generic;

namespace RMT.Allocations.API.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class BulkRequisitionController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;
        public BulkRequisitionController(IMediator mediator, IUserAccessor userAccessor, ILogger<BaseController> logger) : base(logger)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        [HttpPost("BulkUploadRequisition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<BulkRequistionResponse> BulkUploadRequisition([FromBody] List<BulkCreateRequisitionDTO> bulkCreateRequisitionDTO)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var response = await _mediator.Send(new BulkRequisitionCommand()
                {
                    bulkRequisitions = bulkCreateRequisitionDTO,
                    userInfo = userInfo
                });
                return response;
            }

            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("BulkUploadRequisitionValidation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<OkObjectResult> BulkUploadRequisitionValidation([FromBody] List<BulkCreateRequisitionDTO> bulkCreateRequisitionDTO)
        {
            try
            {
                //var userInfo = _userAccessor.GetUser();
                //await _mediator.Send(new BulkRequisitionValidationCommand()
                //{
                //    bulkRequisitions = bulkCreateRequisitionDTO,

                //});
                return Ok(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }
        
        [HttpPost("WcgtValidation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<BulkUploadValidationResponse>> WcgtValidation([FromBody] List<BulkCreateRequisitionDTO> bulkCreateRequisitionDTO)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var request = new UploadWcgtValidationCommand
                {
                    bulkRequisitionsValidation = bulkCreateRequisitionDTO,
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
