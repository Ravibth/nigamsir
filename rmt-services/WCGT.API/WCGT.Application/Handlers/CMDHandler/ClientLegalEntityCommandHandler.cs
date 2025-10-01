using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
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
    public class ClientLegalEntityQuery : IRequest<List<ClientLegalEntityListResponse>>
    {
        public List<GTClientLegalEntityDTO> clientLegalEntitys { get; set; }
    }
    public class ClientLegalEntityCommandHandler : IRequestHandler<ClientLegalEntityQuery, List<ClientLegalEntityListResponse>>
    {
        private readonly IWcgtDataRepository _repository;

        public ClientLegalEntityCommandHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ClientLegalEntityListResponse>> Handle(ClientLegalEntityQuery request, CancellationToken cancellationToken)
        {
            List<ClientLegalEntityListResponse> response = new();

            ClientLegalEntityListResponse _response = null;
            foreach (var current_item in request.clientLegalEntitys)
            {
                _response = WcgtMapper.Mapper.Map<ClientLegalEntityListResponse>(current_item);
                try
                {
                    ClientLegalEntity clientLegalEntity = WcgtMapper.Mapper.Map<ClientLegalEntity>(current_item);
                    ClientLegalEntity response1 = await _repository.UpdateClientLegalEntity(clientLegalEntity);
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
