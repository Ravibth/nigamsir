using MediatR;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class CreateProjectCommand : IRequest<ProjectResponse>
    {
        public Int64 ProjectId { get; set; }

        //public string ProjectCode { get; set; }//feb

        //public string ProjectName { get; set; }//feb

        public string JobName { get; set; }

        public string PipelineName { get; set; }

        public string? JobCode { get; set; }

        public string PipelineCode { get; set; }

        public string ClientName { get; set; }

        public string Expertise { get; set; }//Recheck

        public string SME { get; set; }//Recheck

        public string SMEG { get; set; }//Recheck

        public string Offerings { get; set; }
        public string Solutions { get; set; }
        public string OfferingsId { get; set; }
        public string SolutionsId { get; set; }


        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string PipelineStatus { get; set; }
        public string Description { get; set; }

        public string AllocationStatus { get; set; }

        public string ResourceRequestor { get; set; }
        
        public List<ProjectRoles> ProjectRoles { get; set; }

    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectResponse>
    {
        private readonly IProjectRepository _ProjectRepo;
        public CreateProjectCommandHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<ProjectResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var ProjectEntitiy = ProjectMapper.Mapper.Map<Project>(request);
            if (ProjectEntitiy is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            var newProject = await _ProjectRepo.AddAsync(ProjectEntitiy);
            ProjectResponse ProjectResponse = ProjectMapper.Mapper.Map<ProjectResponse>(newProject);
            return ProjectResponse;
        }
    }

}
