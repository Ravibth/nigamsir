using MediatR;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetProjectFullDetailsByPipelineCodeQuery : IRequest<Project>
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
    }

    public class GetProjectFullDetailsByPipelineCodeQueryHandler : IRequestHandler<GetProjectFullDetailsByPipelineCodeQuery, Project>
    {
        private readonly IProjectRepository _projectRepository;
        public GetProjectFullDetailsByPipelineCodeQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Project> Handle(GetProjectFullDetailsByPipelineCodeQuery request, CancellationToken cancellationToken)
        {
            return await _projectRepository.GetProjectByPipelineCode(request.PipelineCode, request.JobCode);
        }
    }
}
