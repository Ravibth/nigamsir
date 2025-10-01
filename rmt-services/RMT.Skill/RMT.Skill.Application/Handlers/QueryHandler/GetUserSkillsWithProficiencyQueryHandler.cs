using MediatR;
using RMT.Skill.Application.Mappers;
using RMT.Skill.Domain.Entities;
using RMT.Skill.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.Handlers.QueryHandler
{
    public class GetUserSkillsWithProficiencyQuery : IRequest<List<UserSkills>>
    {
        public string email { get; set; }
    }

    public class GetUserSkillsWithProficiencyQueryHandler : IRequestHandler<GetUserSkillsWithProficiencyQuery, List<UserSkills>>
    {
        private readonly IUserSkillRepository _userSkillRepository;
        public GetUserSkillsWithProficiencyQueryHandler(IUserSkillRepository userSkillRepository)
        {
            _userSkillRepository = userSkillRepository;
        }
        public async Task<List<UserSkills>> Handle(GetUserSkillsWithProficiencyQuery request, CancellationToken cancellationToken)
        {
            var response = await _userSkillRepository.GetUserApprovedSkillsByEmail(new List<string> { request.email });
            var mapped = SkillMapper.Mapper.Map<List<UserSkills>>(response);
            return mapped.OrderBy(m => m.SkillName).ToList();
        }
    }
}
