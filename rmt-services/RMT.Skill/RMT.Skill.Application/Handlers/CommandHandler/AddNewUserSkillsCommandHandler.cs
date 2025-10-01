using MediatR;
using RMT.Skill.Application.Mappers;
using RMT.Skill.Application.Requests;
using RMT.Skill.Domain;
using RMT.Skill.Domain.DTOs.Responses;
using RMT.Skill.Domain.Entities;
using RMT.Skill.Domain.Repositories;
using RMT.Skill.Infrastructure;

namespace RMT.Skill.Application.Handlers.CommandHandler
{
    public class AddUpdateUserSkillsCommand : IRequest<List<AddUpdateNewUserSkill>>
    {
        public List<AddUpdateUserSkillsRequestDTO> skills { get; set; }
        public UserDecorator user { get; set; }
    }
    public class AddUpdateUserSkillsCommandHandler : IRequestHandler<AddUpdateUserSkillsCommand, List<AddUpdateNewUserSkill>>
    {
        private readonly IUserSkillRepository _userSkillRepository;
        public AddUpdateUserSkillsCommandHandler(IUserSkillRepository userSkillRepository)
        {
            _userSkillRepository = userSkillRepository;
        }
        public async Task<List<AddUpdateNewUserSkill>> Handle(AddUpdateUserSkillsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = new List<UserSkills>();
                foreach (var skill in command.skills)
                {
                    request.Add(new()
                    {
                        SkillName = skill.SkillName,
                        SkillCode = skill.SkillCode,
                        Proficiency = skill.Proficiency,
                        Status = InfrastructureConstants.UserSkillsStatus.PENDING,
                        Email = command.user?.email,
                        Name = command.user?.name,
                        EmpId = command.user?.emp_code,
                        CreatedBy = command.user?.email,
                        ModifiedBy = command.user?.email,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow,
                    });
                }
                var skillsAdded = await _userSkillRepository.AddUserSkills(request);
                List<AddUpdateNewUserSkill> response = SkillMapper.Mapper.Map<List<AddUpdateNewUserSkill>>(skillsAdded);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
