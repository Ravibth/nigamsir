using MediatR;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.DTOs.Response;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetProjectDetailsAsPerPipelineCodeAndUserRoleQuery : IRequest<GetProjectDetailsByUserRole>
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? UserEmail { get; set; }
        public List<string>? ApplicationRoles { get; set; }
    }
    public class GetProjectDetailsAsPerPipelineCodeAndUserRoleQueryHandler : IRequestHandler<GetProjectDetailsAsPerPipelineCodeAndUserRoleQuery, GetProjectDetailsByUserRole>
    {
        private readonly IProjectRepository _projectRepository;
        public GetProjectDetailsAsPerPipelineCodeAndUserRoleQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<GetProjectDetailsByUserRole> Handle(GetProjectDetailsAsPerPipelineCodeAndUserRoleQuery request, CancellationToken cancellationToken)
        {
            var result = await _projectRepository.GetProjectDetailsByPipelineCodeAndUserRole(request.PipelineCode, request.JobCode, request.UserEmail , request.ApplicationRoles);
            //var response = ProjectMapper.Mapper.Map<ProjectDetailsAsPerPipelineCodeAndUserRoleResponseDTO>(result);
            return result;
        }
    }
}
