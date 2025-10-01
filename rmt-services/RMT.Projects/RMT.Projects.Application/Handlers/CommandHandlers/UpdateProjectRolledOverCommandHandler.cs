using MediatR;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class UpdateProjectRolledOverCommand : IRequest<ProjectResponse>
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }

        public Boolean IsRollover { get; set; }

        public int RolloverDays { get; set; }

        public string? ModifiedBy { get; set; }

        //public DateTime ModifiedAt { get; set; }

    }

    public class UpdateProjectRolledOverCommandHandler : IRequestHandler<UpdateProjectRolledOverCommand, ProjectResponse>
    {
        private readonly IProjectRepository _ProjectRepo;
        public UpdateProjectRolledOverCommandHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<ProjectResponse> Handle(UpdateProjectRolledOverCommand request, CancellationToken cancellationToken)
        {
            var ProjectEntitiy = ProjectMapper.Mapper.Map<Project>(request);
            if (ProjectEntitiy is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            var newProject = await _ProjectRepo.UpdateProjectRolledOver(ProjectEntitiy);
            ProjectResponse ProjectResponse = ProjectMapper.Mapper.Map<ProjectResponse>(newProject);
            return ProjectResponse;
        }
    }

}
