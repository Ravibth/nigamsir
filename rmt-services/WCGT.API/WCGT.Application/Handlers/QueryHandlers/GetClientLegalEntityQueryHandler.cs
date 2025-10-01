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

    public class GetClientLegalEntityListQuery : IRequest<List<GTClientLegalEntityDTO>>
    {

    }
    public class GetClientLegalEntityLegalEntityQueryHandler : IRequestHandler<GetClientLegalEntityListQuery, List<GTClientLegalEntityDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetClientLegalEntityLegalEntityQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GTClientLegalEntityDTO>> Handle(GetClientLegalEntityListQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllClientLegalEntities();
            List<GTClientLegalEntityDTO> response = WcgtMapper.Mapper.Map<List<GTClientLegalEntityDTO>>(result);
            return response;
        }
    }
}
