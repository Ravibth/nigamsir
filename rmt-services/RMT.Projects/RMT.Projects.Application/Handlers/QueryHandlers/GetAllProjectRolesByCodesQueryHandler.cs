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
    public class GetAllProjectRolesByCodesQuery : IRequest<List<ProjectRolesResponseDTO>>
    {
        public PipelineCodeAndRolesDto PipelineCodeAndRolesDto { get; set; }
    }
    public class GetAllProjectRolesByCodesQueryHandler : IRequestHandler<GetAllProjectRolesByCodesQuery, List<ProjectRolesResponseDTO>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetAllProjectRolesByCodesQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<ProjectRolesResponseDTO>> Handle(GetAllProjectRolesByCodesQuery request, CancellationToken cancellationToken)
        {
            KeyValuePair<string, string?> codes = new KeyValuePair<string, string?>(request.PipelineCodeAndRolesDto.pipelineCode, request.PipelineCodeAndRolesDto.jobCode);

            List<ProjectRolesView> respResult = await _projectRepository.GetAllProjectRolesByCodes(codes);

            List<ProjectRolesResponseDTO> response = ProjectMapper.Mapper.Map<List<ProjectRolesResponseDTO>>(respResult);
            return response;
        }
    }

}
