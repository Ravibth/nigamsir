using MediatR;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetRequestorEmailsForAllocationWorkflowQuery : IRequest<List<ProjectRolesResponseDTO>>
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string WorkflowStartedBy { get; set; }
    }
    public class GetRequestorEmailsForAllocationWorkflowQueryHandler : IRequestHandler<GetRequestorEmailsForAllocationWorkflowQuery, List<ProjectRolesResponseDTO>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetRequestorEmailsForAllocationWorkflowQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<List<ProjectRolesResponseDTO>> Handle(GetRequestorEmailsForAllocationWorkflowQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectRepository.GetRequestorEmailForAllocationWorkflow(request.PipelineCode, request.JobCode, request.WorkflowStartedBy);
            List<ProjectRolesResponseDTO> response = ProjectMapper.Mapper.Map<List<ProjectRolesResponseDTO>>(result);
            return response;
        }
    }
}
