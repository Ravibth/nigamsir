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

    public class GetDesignationListQuery: IRequest<List<GTDesignationDTO>>
    {

    }
    public class GetDesignationListQueryHandler : IRequestHandler<GetDesignationListQuery, List<GTDesignationDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetDesignationListQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GTDesignationDTO>> Handle(GetDesignationListQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllDesignations();
            List<GTDesignationDTO> response = WcgtMapper.Mapper.Map<List<GTDesignationDTO>>(result);
            return response;
        }
    }
}
