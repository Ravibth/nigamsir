using MediatR;
using RMT.Projects.Application.DTOs;
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
    public class GetRoleEmailsByPipelineCodeQuery : IRequest<List<RoleEmailsByPipelineCodeResponse>>
    {
        public List<PipelineCodeAndRolesDto> PipelineCodeAndRolesDto { get; set; }
    }
    public class GetRoleEmailsByPipelineCodeQueryHandler : IRequestHandler<GetRoleEmailsByPipelineCodeQuery, List<RoleEmailsByPipelineCodeResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetRoleEmailsByPipelineCodeQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<List<RoleEmailsByPipelineCodeResponse>> Handle(GetRoleEmailsByPipelineCodeQuery request, CancellationToken cancellationToken)
        {
            List<RoleEmailsByPipelineCodeResponse> response = new List<RoleEmailsByPipelineCodeResponse>();
            foreach (var item in request.PipelineCodeAndRolesDto)
            {
                Dictionary<string, List<ProjectRolesView>> respResult = await _projectRepository.GetProjectRolesByPipelineCodeAndRoles(
                    new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>(item.pipelineCode, item.jobCode) }, item.roles);
                Dictionary<string, List<string>> respResultWithEmailOnly = new Dictionary<string, List<string>>();
                if (respResult != null)
                {
                    foreach (var kVp in respResult)
                    {
                        respResultWithEmailOnly[kVp.Key] = kVp.Value.Select(d => d.User).ToList();
                    }
                    RoleEmailsByPipelineCodeResponse respObj = new RoleEmailsByPipelineCodeResponse()
                    {
                        PipelineCode = item.pipelineCode,
                        JobCode = item.jobCode,
                        RolesEmails = respResultWithEmailOnly
                    };
                    response.Add(respObj);
                }
            }
            return response;
        }
    }
}
