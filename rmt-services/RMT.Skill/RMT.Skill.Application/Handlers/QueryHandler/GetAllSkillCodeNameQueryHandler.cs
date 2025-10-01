using MediatR;
using RMT.Skill.Application.DTOs;
using RMT.Skill.Domain.Entities;
using RMT.Skill.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.Handlers.QueryHandler
{
    public class GetAllSkillCodeNameQuery : IRequest<List<SkillCodeNameDTO>>
    {

    }
    public class GetAllSkillCodeNameQueryHandler : IRequestHandler<GetAllSkillCodeNameQuery, List<SkillCodeNameDTO>>
    {
        private readonly ISkillDataRepository _repository;
        public GetAllSkillCodeNameQueryHandler(ISkillDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SkillCodeNameDTO>> Handle(GetAllSkillCodeNameQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllSkillAsync();
            List<SkillCodeNameDTO> skillCodeList = new List<SkillCodeNameDTO>();
            foreach (var item in result)
            {
                skillCodeList.Add(new SkillCodeNameDTO
                {
                    SkillCode = item.SkillCode,
                    SkillName = item.SkillName,
                    Competency = item.Skill_Mapping.Select(m => m.Competency).ToList(),
                    CompetencyId = item.Skill_Mapping.Select(m => m.CompetencyId).ToList()
                });
            }
            return skillCodeList;
        }

    }
}
