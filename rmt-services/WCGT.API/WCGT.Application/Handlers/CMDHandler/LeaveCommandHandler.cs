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
    public class LeaveQuery : IRequest<List<LeaveListResponse>>
    {
        public List<GTLeaveDTO> leaves { get; set; }
    }
    public class LeaveCommandHandler : IRequestHandler<LeaveQuery, List<LeaveListResponse>>
    {
        private readonly IWcgtDataRepository _repository;

        public LeaveCommandHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LeaveListResponse>> Handle(LeaveQuery request, CancellationToken cancellationToken)
        {
            List<LeaveListResponse> response = new List<LeaveListResponse>();

            LeaveListResponse _response = null;
            foreach (var current_item in request.leaves)
            {
                _response = WcgtMapper.Mapper.Map<LeaveListResponse>(current_item);
                try
                {
                    Leave leave = WcgtMapper.Mapper.Map<Leave>(current_item);
                    leave.start_date_half = current_item.start_date_half;
                    leave.end_date_half = current_item.end_date_half;
                    Leave response1 = await _repository.UpdateLeave(leave);
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
