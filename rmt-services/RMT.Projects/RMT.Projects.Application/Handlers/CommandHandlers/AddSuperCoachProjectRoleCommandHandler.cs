using MediatR;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Domain.DTOs.Request;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class AddSuperCoachProjectRoleCommand : IRequest<bool>
    {
        public AddSuperCoachProjectRoleRequestDTO request { get; set; }
    }
    public class AddSuperCoachProjectRoleCommandHandler : IRequestHandler<AddSuperCoachProjectRoleCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;
        public AddSuperCoachProjectRoleCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<bool> Handle(AddSuperCoachProjectRoleCommand request, CancellationToken cancellationToken)
        {
            
            await _projectRepository.AddSuperCoachRole(request.request);
            return true;
        }
    }
}
