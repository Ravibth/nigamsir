using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.DTOs;
using WCGT.Application.Mappers;
using WCGT.Application.Responses;
using WCGT.Domain.DTO;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Handlers.CMDHandler
{
    public class BUEfficiencyLeaderCommand : IRequest<List<GTBUEfficiencyLeaderResponse>>
    {
        public List<BUEfficiencyLeaderDTO> buEfficiencyLeaders { get; set; }
    }
    public class BUEfficiencyLeaderCommandHandler : IRequestHandler<BUEfficiencyLeaderCommand, List<GTBUEfficiencyLeaderResponse>>
    {
        private readonly IWcgtDataRepository _repository;

        public BUEfficiencyLeaderCommandHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GTBUEfficiencyLeaderResponse>> Handle(BUEfficiencyLeaderCommand request, CancellationToken cancellationToken)
        {
            List<GTBUEfficiencyLeaderResponse> response = new List<GTBUEfficiencyLeaderResponse>();

            GTBUEfficiencyLeaderResponse _response = null;
            foreach (var current_item in request.buEfficiencyLeaders)
            {
                _response = WcgtMapper.Mapper.Map<GTBUEfficiencyLeaderResponse>(current_item);
                try
                {
                    BUEfficiencyLeaderDTO buEfficiencyLeader = WcgtMapper.Mapper.Map<BUEfficiencyLeaderDTO>(current_item);
                    BUEfficiencyLeaderDTO response1 = await _repository.UpdateBUEfficiencyLeader(buEfficiencyLeader);
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