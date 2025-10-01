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

    public class GetBUTreeMappingListQuery: IRequest<List<GTBUTreeMappingDTO>>
    {

    }
    public class GetBUTreeMappingListQueryHandler : IRequestHandler<GetBUTreeMappingListQuery, List<GTBUTreeMappingDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetBUTreeMappingListQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GTBUTreeMappingDTO>> Handle(GetBUTreeMappingListQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllBUTreeMappings();
            List<GTBUTreeMappingDTO> response = WcgtMapper.Mapper.Map<List<GTBUTreeMappingDTO>>(result);
            return response;
        }
    }
}
