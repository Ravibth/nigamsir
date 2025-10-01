using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Mappers;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Handlers.QueryHandlers
{
    public class GetJobByJobCodeQuery : IRequest<GTJobDTO>
    {
        public string? PipelineCode { get; set; }    
        public string JobCode { get; set; }
    }
    public class GetJobByJobCodeQueryHandler : IRequestHandler<GetJobByJobCodeQuery, GTJobDTO>
    {
        private readonly IWcgtDataRepository _repository;
        public GetJobByJobCodeQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<GTJobDTO> Handle(GetJobByJobCodeQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetJobByJobCode(request.PipelineCode,request.JobCode);
            GTJobDTO response = WcgtMapper.Mapper.Map<GTJobDTO>(result);
            return response;
        }
    }
}
