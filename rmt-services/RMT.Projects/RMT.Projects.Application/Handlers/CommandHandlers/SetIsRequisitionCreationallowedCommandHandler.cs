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
    public class SetIsRequisitionCreationAllowedCommand : IRequest<ProjectResponse>
    {
        public bool IsRequisitionCreationallowed { get; set; }
        //public string ProjectCode { get; set; }//feb
        public string PipelineCode { get; set; }//feb
        public string? JobCode { get; set; }//feb


    }

    public class SetIsRequisitionCreationAllowedCommandHandler : IRequestHandler<SetIsRequisitionCreationAllowedCommand, ProjectResponse>
    {
        private readonly IProjectRepository _ProjectRepo;
        public SetIsRequisitionCreationAllowedCommandHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<ProjectResponse> Handle(SetIsRequisitionCreationAllowedCommand request, CancellationToken cancellationToken)
        {
            /* var ProjectEntitiy = ProjectMapper.Mapper.Map<Project>(request);
             if (ProjectEntitiy is null)
             {
                 throw new ApplicationException("Issue with mapper");
             }*/
            var newProject = await _ProjectRepo.SetIsRequisitionCreationAllowed(request.IsRequisitionCreationallowed, request.PipelineCode, request.JobCode);
            ProjectResponse ProjectResponse = ProjectMapper.Mapper.Map<ProjectResponse>(newProject);
            return ProjectResponse;
        }
    }

}
