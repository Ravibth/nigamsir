using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Mappers;
using WCGT.Application.Responses;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;
using WCGT.Infrastructure.Migrations;

namespace WCGT.Application.Handlers.CMDHandler
{
    public class UpdateRateMasterCommand : IRequest<List<DesignationRateMasterResponse>>
    {
        public List<GTDesignationRateMasterDTO> DesingationRateMaster { get; set; }
    }
    public class UpdateRateMasterCommandHandler : IRequestHandler<UpdateRateMasterCommand, List<DesignationRateMasterResponse>>
    {
        private readonly IWcgtDataRepository _repository;

        public UpdateRateMasterCommandHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<DesignationRateMasterResponse>> Handle(UpdateRateMasterCommand request, CancellationToken cancellationToken)
        {
            List<DesignationRateMasterResponse> response = new List<DesignationRateMasterResponse>();

            DesignationRateMasterResponse _response = null;
            foreach (var current_item in request.DesingationRateMaster)
            {
                _response = WcgtMapper.Mapper.Map<DesignationRateMasterResponse>(current_item);
                try
                {
                    RateDesignationMaster rate = WcgtMapper.Mapper.Map<RateDesignationMaster>(current_item);
                    RateDesignationMaster response1 = await _repository.UpdateRateDesignationMaster(rate);
                }
                catch (Exception ex)
                {
                    _response.isfailed = true;
                    _response.failed_message = ex.Message;
                    var dataLog = Common.CreateWCGTDataLogObject(_response, current_item.GetType(), ex);
                    await _repository.AddWCGTDataLogEntry(dataLog);
                }
                response.Add(_response);
            }

            return response;
        }
    }
}
