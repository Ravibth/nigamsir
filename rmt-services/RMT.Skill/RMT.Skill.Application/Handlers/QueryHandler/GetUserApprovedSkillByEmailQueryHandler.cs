using MediatR;
using RMT.Skill.Domain.DTOs;
using RMT.Skill.Domain.Entities;
using RMT.Skill.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.Handlers.QueryHandler
{
    public class GetUserApprovedSkillByEmailQuery : IRequest<List<UserSkillsResponseWithSkillDTO>>
    {
        public List<string> emails { get; set; }
    }
    public class GetUserApprovedSkillByEmailQueryHandler : IRequestHandler<GetUserApprovedSkillByEmailQuery, List<UserSkillsResponseWithSkillDTO>>
    {

        private readonly IUserSkillRepository _userSkillRepository;
        public GetUserApprovedSkillByEmailQueryHandler(IUserSkillRepository userSkillRepository)
        {
            _userSkillRepository = userSkillRepository;
        }
        public async Task<List<UserSkillsResponseWithSkillDTO>> Handle(GetUserApprovedSkillByEmailQuery query, CancellationToken cancellationToken)
        {
            return await _userSkillRepository.GetUserApprovedSkillsByEmail(query.emails);
        }

    }
}
