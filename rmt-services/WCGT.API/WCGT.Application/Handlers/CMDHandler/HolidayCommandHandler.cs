using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WCGT.Application.Mappers;
using WCGT.Application.Responses;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Handlers.CMDHandler
{
    public class HolidayQuery : IRequest<List<HolidayListResponse>>
    {
        public List<GTHolidayDTO> holidays { get; set; }
    }
    public class HolidayCommandHandler : IRequestHandler<HolidayQuery, List<HolidayListResponse>>
    {
        private readonly IWcgtDataRepository _repository;

        public HolidayCommandHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<HolidayListResponse>> Handle(HolidayQuery request, CancellationToken cancellationToken)
        {
            List<HolidayListResponse> response = new();

            HolidayListResponse _response = new();
            foreach (var current_item in request.holidays)
            {
                _response = WcgtMapper.Mapper.Map<HolidayListResponse>(current_item);
                try
                {
                    Holiday holiday = WcgtMapper.Mapper.Map<Holiday>(current_item);
                    Holiday response1 = await _repository.UpdateHoliday(holiday);
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
