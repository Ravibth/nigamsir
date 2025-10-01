using MediatR;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.DTOs.Response;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetProjectsRolesByEmailsQuery : IRequest<List<GetProjectRolesByEmailsResponse>>
    {
        public List<string> Emails { get; set; }
    }
    public class GetProjectsRolesByEmailsQueryHandler : IRequestHandler<GetProjectsRolesByEmailsQuery, List<GetProjectRolesByEmailsResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetProjectsRolesByEmailsQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<List<GetProjectRolesByEmailsResponse>> Handle(GetProjectsRolesByEmailsQuery request, CancellationToken cancellationToken)
        {
            List<RoleEmailsByPipelineCodeResponse> response = new List<RoleEmailsByPipelineCodeResponse>();
            List<GetProjectRolesByEmailsResponse> respResult = await _projectRepository.GetProjectRolesByEmails(request.Emails);
            return respResult;
        }
    }
}
