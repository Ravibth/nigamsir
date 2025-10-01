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
    public class ClientQuery : IRequest<List<ClientListResponse>>
    {
        public List<GTClientDTO> clients { get; set; }
    }
    public class ClientCommandHandler : IRequestHandler<ClientQuery, List<ClientListResponse>>
    {
        private readonly IWcgtDataRepository _repository;

        public ClientCommandHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ClientListResponse>> Handle(ClientQuery request, CancellationToken cancellationToken)
        {
            List<ClientListResponse> response = new();

            ClientListResponse _response = null;
            foreach (var current_item in request.clients)
            {
                _response = WcgtMapper.Mapper.Map<ClientListResponse>(current_item);
                try
                {
                    Client client = WcgtMapper.Mapper.Map<Client>(current_item);
                    Client response1 = await _repository.UpdateClient(client);
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
