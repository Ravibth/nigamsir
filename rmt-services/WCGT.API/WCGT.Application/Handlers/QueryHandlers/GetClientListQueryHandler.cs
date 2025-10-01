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

namespace WCGT.Application.Handlers.QueryHandlers
{

    public class GetClientListQuery : IRequest<List<GTClientDTO>>
    {

    }
    public class GetClientListQueryHandler : IRequestHandler<GetClientListQuery, List<GTClientDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetClientListQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GTClientDTO>> Handle(GetClientListQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllClients();
            List<GTClientDTO> response = WcgtMapper.Mapper.Map<List<GTClientDTO>>(result);
            return response;
        }
    }
}
