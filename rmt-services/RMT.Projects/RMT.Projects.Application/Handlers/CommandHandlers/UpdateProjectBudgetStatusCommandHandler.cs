using MediatR;
using RMT.Projects.Application.DTOs;
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
    public class UpdateProjectBudgetStatusCommand : IRequest<List<ProjectResponse>>
    {
        public List<UpdateBudgetStatusDTO> projects { get; set; }
    }
    public class UpdateProjectBudgetStatusCommandHandler : IRequestHandler<UpdateProjectBudgetStatusCommand, List<ProjectResponse>>
    {
        private readonly IProjectRepository _ProjectRepo;
        public UpdateProjectBudgetStatusCommandHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<List<ProjectResponse>> Handle(UpdateProjectBudgetStatusCommand request, CancellationToken cancellationToken)
        {
            List<ProjectResponse> result = new List<ProjectResponse>();
            foreach (var item in request.projects)
            {
                var ProjectEntitiy = await _ProjectRepo.GetProjectByCode(item.PipelineCode, item.JobCode);
                if (ProjectEntitiy is null)
                {
                    throw new ApplicationException("Project not found");
                }
                var newProject = await _ProjectRepo.UpdateProjectBudgetStatus(ProjectEntitiy, item.BudgetStatus);
                ProjectResponse ProjectResponse = ProjectMapper.Mapper.Map<ProjectResponse>(newProject);
                result.Add(ProjectResponse);
            }
            return result;
        }
    }
}
