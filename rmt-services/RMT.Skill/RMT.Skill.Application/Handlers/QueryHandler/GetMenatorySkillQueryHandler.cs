using MediatR;
using RMT.Skill.Application.DTOs;
using RMT.Skill.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.Handlers.QueryHandler
{
    public class GetMenatorySkillQuery : IRequest<List<SkillCodeNameDTO>>
    {
        public string? Competency { get; set; }
        public string? CompetencyId { get; set; }
        public string Designation { get; set; }
    }

    public class GetMenatorySkillQueryHandler : IRequestHandler<GetMenatorySkillQuery, List<SkillCodeNameDTO>>
    {

        private readonly ISkillDataRepository _repository;
        public GetMenatorySkillQueryHandler(ISkillDataRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<SkillCodeNameDTO>> Handle(GetMenatorySkillQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetManatorySkillAsync(request.Competency, request.CompetencyId, request.Designation);
            List<SkillCodeNameDTO> skillCodeList = new List<SkillCodeNameDTO>();
            foreach (var item in result)
            {
                if (item.Skill != null)
                {
                    skillCodeList.Add(new SkillCodeNameDTO
                    {
                        SkillCode = item.Skill.SkillCode
                        ,
                        SkillName = item.Skill.SkillName
                        ,
                        Competency = item.Skill.Skill_Mapping.Select(m => m.Competency).ToList()
                        ,
                        CompetencyId = item.Skill.Skill_Mapping.Select(m => m.CompetencyId).ToList()
                    });

                }
            }

            return skillCodeList;
        }
    }
}
