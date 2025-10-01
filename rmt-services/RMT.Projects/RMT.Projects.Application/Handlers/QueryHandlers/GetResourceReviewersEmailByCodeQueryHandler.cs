using MediatR;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetResourceReviewersEmailByCodeQuery : IRequest<List<ProjectRolesResponseDTO>>
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
    }
    public class GetResourceReviewersEmailByCodeQueryHandler : IRequestHandler<GetResourceReviewersEmailByCodeQuery, List<ProjectRolesResponseDTO>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetResourceReviewersEmailByCodeQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<List<ProjectRolesResponseDTO>> Handle(GetResourceReviewersEmailByCodeQuery request, CancellationToken cancellationToken)
        {
            List<ProjectRolesView> resp = await _projectRepository.GetReviewerEmailsByPipelineCode(request.PipelineCode, request.JobCode);

            //var result = ProjectMapper.Mapper.Map<List<ProjectRoles>>(resp);

            var response = ProjectMapper.Mapper.Map<List<ProjectRolesResponseDTO>>(resp);
            return response;
        }
    }
}
