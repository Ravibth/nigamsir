using MediatR;
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
    public class GetMembersOfAllProjectsOfUserQuery : IRequest<List<GetMembersOfAllProjectsOfUserResponse>>
    {
        public List<string> usersEmail { get; set; }
        public List<string>? projectRoles { get; set; }
    }
    public class GetMembersOfAllProjectsOfUserQueryHandler : IRequestHandler<GetMembersOfAllProjectsOfUserQuery, List<GetMembersOfAllProjectsOfUserResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetMembersOfAllProjectsOfUserQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<List<GetMembersOfAllProjectsOfUserResponse>> Handle(GetMembersOfAllProjectsOfUserQuery request, CancellationToken cancellationToken)
        {
            //need to chekc if this used or not
            var response = await _projectRepository.GetMembersOfAllProjectsOfUser(request.usersEmail, request.projectRoles);
            return response;
        }
    }
}
