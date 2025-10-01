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
    public class GetProjectDetailsByCodeQuery : IRequest<Project>
    {
        //public string ProjectCode { get; set; }//feb
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
    }

    public class GetProjectDetailsByCodeQueryHandler : IRequestHandler<GetProjectDetailsByCodeQuery, Project>
    {

        private readonly IProjectRepository _ProjectRepo;
        public GetProjectDetailsByCodeQueryHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }
        public async Task<Project> Handle(GetProjectDetailsByCodeQuery request, CancellationToken cancellationToken)
        {
            return await _ProjectRepo.GetProjectDetailsByCode(request.PipelineCode, request.JobCode);

        }
    }
}









