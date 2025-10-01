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

    public class GetLocationListQuery : IRequest<List<GTLocationDTO>>
    {

    }
    public class GetLocationListQueryHandler : IRequestHandler<GetLocationListQuery, List<GTLocationDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetLocationListQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GTLocationDTO>> Handle(GetLocationListQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllLocations();
            List<GTLocationDTO> response = WcgtMapper.Mapper.Map<List<GTLocationDTO>>(result);
            return response;
        }
    }
}
