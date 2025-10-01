using MediatR;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System.Collections.Generic;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetResourceRequestorEmailByPipelineCodeQuery : IRequest<List<ProjectRolesResponseDTO>>
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
    }
    public class GetResourceRequestorEmailByPipelineCodeQueryHandler : IRequestHandler<GetResourceRequestorEmailByPipelineCodeQuery, List<ProjectRolesResponseDTO>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetResourceRequestorEmailByPipelineCodeQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<List<ProjectRolesResponseDTO>> Handle(GetResourceRequestorEmailByPipelineCodeQuery request, CancellationToken cancellationToken)
        {
            List<ProjectRolesView> resp = await _projectRepository.GetRequestorEmailsByPipelineCode(request.PipelineCode, request.JobCode);

            //var result = ProjectMapper.Mapper.Map<List<ProjectRoles>>(resp);

            List<ProjectRolesResponseDTO> response = ProjectMapper.Mapper.Map<List<ProjectRolesResponseDTO>>(resp);
            return response;
            //throw new NotImplementedException();
        }
    }
}
