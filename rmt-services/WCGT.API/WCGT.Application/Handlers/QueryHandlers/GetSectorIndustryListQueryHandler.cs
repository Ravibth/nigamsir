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

    public class GetSectorIndustryListQuery : IRequest<List<GTSectorIndustryDTO>>
    {

    }
    public class GetSectorIndustryListQueryHandler : IRequestHandler<GetSectorIndustryListQuery, List<GTSectorIndustryDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetSectorIndustryListQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GTSectorIndustryDTO>> Handle(GetSectorIndustryListQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllSectorIndustries();
            List<GTSectorIndustryDTO> response = WcgtMapper.Mapper.Map<List<GTSectorIndustryDTO>>(result);
            return response;
        }
    }
}
