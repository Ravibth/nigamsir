using MediatR;
using RMT.Skill.Domain.DTOs;
using RMT.Skill.Domain.Entities;
using RMT.Skill.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.Handlers.CommandHandler
{
    public class UpdateUserSkillStatusAfterWorkflowCommand : IRequest<List<UserSkills>>
    {
        public List<UpdateUserSkillStatusAfterWorkflowRequestDTO> updatedActions {  get; set; }
    }
    public class UpdateUserSkillStatusAfterWorkflowCommandHandler : IRequestHandler<UpdateUserSkillStatusAfterWorkflowCommand, List<UserSkills>>
    {
        private readonly IUserSkillRepository _userSkillRepository;
        public UpdateUserSkillStatusAfterWorkflowCommandHandler(IUserSkillRepository userSkillRepository)
        {
            _userSkillRepository = userSkillRepository;
        }
        public async Task<List<UserSkills>> Handle(UpdateUserSkillStatusAfterWorkflowCommand request, CancellationToken cancellationToken)
        {

            return await _userSkillRepository.UpdateUserSkillStatusAfterWorkflow(request.updatedActions);
        }
    }
}
