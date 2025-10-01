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
    public class ProjectDetailsByCreationDateQuery : IRequest<List<ProjectFullDetailsResponse>>
    {
        public DateOnly CreationDate { get; set; }
    }
    public class ProjectDetailsByCreationDateQueryHandle : IRequestHandler<ProjectDetailsByCreationDateQuery, List<ProjectFullDetailsResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectDetailsByCreationDateQueryHandle(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<List<ProjectFullDetailsResponse>> Handle(ProjectDetailsByCreationDateQuery request, CancellationToken cancellationToken)
        {
            var projectsList = await _projectRepository.GetAllProjectsByCreationDate(request.CreationDate);
            var result = ProjectMapper.Mapper.Map<List<ProjectFullDetailsResponse>>(projectsList);
            return result;
        }
    }
}
