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

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetProjectFullDetailsByUniqueCodesQuery : IRequest<List<ProjectFullDetailsResponse>>
    {
        public List<KeyValuePair<string, string?>> uniqueCodes { get; set; }
    }

    public class GetProjectFullDetailsByUniqueCodesQueryHandler : IRequestHandler<GetProjectFullDetailsByUniqueCodesQuery, List<ProjectFullDetailsResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetProjectFullDetailsByUniqueCodesQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<ProjectFullDetailsResponse>> Handle(GetProjectFullDetailsByUniqueCodesQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectRepository.GetProjectsByUniqueCodes(request.uniqueCodes);

            List<ProjectFullDetailsResponse> response = null;

            if (result != null)
            {
                ProjectMapper.Mapper.Map<ProjectDetailsRequestorDto>(result);
            }

            return response;

        }
    }
}
