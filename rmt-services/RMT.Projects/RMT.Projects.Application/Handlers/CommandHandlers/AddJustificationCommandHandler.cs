using MediatR;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class AddJustificationCommand : IRequest<ProjectResponse>
    {
        public string JustificationText { get; set; }
        //public string ProjectCode { get; set; }//feb
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
    }
    public class AddJustificationCommandHandler : IRequestHandler<AddJustificationCommand, ProjectResponse>
    {
        private readonly IProjectRepository _ProjectRepo;
        public AddJustificationCommandHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }
        public async Task<ProjectResponse> Handle(AddJustificationCommand request, CancellationToken cancellationToken)
        {
            ProjectResponse result = new ProjectResponse();

            var ProjectEntitiy = await _ProjectRepo.GetProjectByCode(request.PipelineCode, request.JobCode);
            if (ProjectEntitiy is null)
            {
                throw new ApplicationException("Project not found");
            }
            var newProject = await _ProjectRepo.AddJusticiationText(ProjectEntitiy, request.JustificationText);
            ProjectResponse ProjectResponse = ProjectMapper.Mapper.Map<ProjectResponse>(newProject);

            return result;
        }
    }
}
