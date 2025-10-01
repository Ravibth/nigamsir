using MediatR;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetAllJobCodesForPipelineCodeQuery : IRequest<List<string>>
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }

        public bool? SameTeamAllocation { get; set; }
    }
    public class GetAllJobCodesForPipelineCodeQueryHandler : IRequestHandler<GetAllJobCodesForPipelineCodeQuery, List<string>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetAllJobCodesForPipelineCodeQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<string>> Handle(GetAllJobCodesForPipelineCodeQuery getAllJobCodesForPipelineCodeQuery, CancellationToken cancellationToken)
        {
            return await _projectRepository.GetAllJobCodesForPipelineCode(getAllJobCodesForPipelineCodeQuery.PipelineCode, getAllJobCodesForPipelineCodeQuery.JobCode, getAllJobCodesForPipelineCodeQuery.SameTeamAllocation);
        }
    }
}
