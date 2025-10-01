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
    public class BUTreeMappingQuery : IRequest<List<BUTreeMappingListResponse>>
    {
        public List<GTBUTreeMappingDTO> buTreeMappings { get; set; }
    }
    public class BUTreeMappingCommandHandler : IRequestHandler<BUTreeMappingQuery, List<BUTreeMappingListResponse>>
    {
        private readonly IWcgtDataRepository _repository;

        public BUTreeMappingCommandHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BUTreeMappingListResponse>> Handle(BUTreeMappingQuery request, CancellationToken cancellationToken)
        {
            List<BUTreeMappingListResponse> response = new();

            BUTreeMappingListResponse _response = null;
            foreach (var current_item in request.buTreeMappings)
            {
                _response = WcgtMapper.Mapper.Map<BUTreeMappingListResponse>(current_item);
                try
                {
                    BUTreeMapping buTreeMapping = WcgtMapper.Mapper.Map<BUTreeMapping>(current_item);
                    BUTreeMapping response1 = await _repository.UpdateBUTreeMapping(buTreeMapping);
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
