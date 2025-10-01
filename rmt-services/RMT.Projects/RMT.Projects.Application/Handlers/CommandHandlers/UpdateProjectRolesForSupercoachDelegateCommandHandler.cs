using MediatR;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class UpdateProjectRolesForSupercoachDelegateCommand : IRequest<List<ProjectRoles>>
    {
        public string supercoach_email { get; set; }
        public string? prev_allocation_delegate_email { get; set; }
        public string? new_allocation_delegate_email { get; set; }
        public string? new_allocation_delegate_name { get; set; }
    }
    public class UpdateProjectRolesForSupercoachDelegateCommandHandler : IRequestHandler<UpdateProjectRolesForSupercoachDelegateCommand, List<ProjectRoles>>
    {
        private readonly IProjectRepository _projectRepository;
        public UpdateProjectRolesForSupercoachDelegateCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<List<ProjectRoles>> Handle(UpdateProjectRolesForSupercoachDelegateCommand request, CancellationToken cancellationToken)
        {
            var rolesResult = await _projectRepository.UpdateProjectRolesForSupercoachDelegate(request.supercoach_email,
                string.IsNullOrEmpty(request.prev_allocation_delegate_email) ? null : request.prev_allocation_delegate_email,
                string.IsNullOrEmpty(request.new_allocation_delegate_email) ? null : request.new_allocation_delegate_email,
                string.IsNullOrEmpty(request.new_allocation_delegate_name) ? null : request.new_allocation_delegate_name
                );

            return rolesResult;
        }
    }
}
