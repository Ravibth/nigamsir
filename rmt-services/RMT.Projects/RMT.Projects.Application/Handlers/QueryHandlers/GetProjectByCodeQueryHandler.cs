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

    public class GetProjectByCodeQuery : IRequest<Project>
    {
        //public string ProjectCode { get; set; }//feb
        public string pipelineCode { get; set; }
        public string? jobCode { get; set; }

    }

    public class GetProjectByCodeQueryHandler : IRequestHandler<GetProjectByCodeQuery, Project>
    {
        private readonly IProjectRepository _ProjectRepo;
        public GetProjectByCodeQueryHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<Project> Handle(GetProjectByCodeQuery request, CancellationToken cancellationToken)
        {
            return await _ProjectRepo.GetProjectByCode(request.pipelineCode, request.jobCode);
            //return await Task.FromResult(new List<Project>());
        }
    }

}
