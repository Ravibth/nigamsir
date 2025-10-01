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

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetAllProjectsQuery : IRequest<List<ProjectFullDetailsResponse>>
    {

    }

    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectFullDetailsResponse>>
    {
        private readonly IProjectRepository _ProjectRepo;
        public GetAllProjectsQueryHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<List<ProjectFullDetailsResponse>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var result = await _ProjectRepo.GetAllAsync();
            var response = ProjectMapper.Mapper.Map<List<ProjectFullDetailsResponse>>(result);
            return response;
            //return await Task.FromResult(new List<Project>());
        }
    }
}
