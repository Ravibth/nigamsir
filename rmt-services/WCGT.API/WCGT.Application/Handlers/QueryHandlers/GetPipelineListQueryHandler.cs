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

    public class GetPipelineListQuery : IRequest<List<GTPipelineDTO>>
    {

    }
    public class GetPipelineListQueryHandler : IRequestHandler<GetPipelineListQuery, List<GTPipelineDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetPipelineListQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GTPipelineDTO>> Handle(GetPipelineListQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllPipelines();
            List<GTPipelineDTO> response = WcgtMapper.Mapper.Map<List<GTPipelineDTO>>(result);
            return response;
        }
    }
}
