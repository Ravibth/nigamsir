using MediatR;
using RMT.Skill.Domain.Entities;
using RMT.Skill.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.Handlers.QueryHandler
{
    public class GetAllSkillsBySkillCodeQuery:IRequest<List<Skills>>
    {
        public List<string> SkillCodes { get; set; }
    }
    public class GetAllSkillsBySkillCodeQueryHandler : IRequestHandler<GetAllSkillsBySkillCodeQuery, List<Skills>>
    {
        public readonly ISkillDataRepository _skillDataRepository;
        public GetAllSkillsBySkillCodeQueryHandler(ISkillDataRepository skillDataRepository)
        {
            _skillDataRepository = skillDataRepository;
        }
        public async Task<List<Skills>> Handle(GetAllSkillsBySkillCodeQuery request, CancellationToken cancellationToken)
        {
            var skillsList = await _skillDataRepository.GetAllSkillsByNameOrCode(request.SkillCodes);
            return skillsList;
        }
    }
}
