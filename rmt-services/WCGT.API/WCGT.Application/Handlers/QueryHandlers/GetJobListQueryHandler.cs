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

    public class GetJobListQuery : IRequest<List<GTJobDTO>>
    {

    }
    public class GetJobListQueryHandler : IRequestHandler<GetJobListQuery, List<GTJobDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetJobListQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GTJobDTO>> Handle(GetJobListQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllJobs();
            List<GTJobDTO> response = WcgtMapper.Mapper.Map<List<GTJobDTO>>(result);
            return response;
        }
    }
}
