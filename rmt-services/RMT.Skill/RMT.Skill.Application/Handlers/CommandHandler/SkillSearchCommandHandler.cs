using MediatR;
using RMT.Skill.Application.Mappers;
using RMT.Skill.Domain.DTOs;
using RMT.Skill.Domain.DTOs.Request;
using RMT.Skill.Domain.Entities;
using RMT.Skill.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace RMT.Skill.Application.Handlers.CommandHandler
{
    public class SkillSearchCommand : IRequest<List<UserSkills>>
    {
        public List<string> SkillName { get; set; } 
    }
    public class SkillSearchCommandHandler : IRequestHandler<SkillSearchCommand, List<UserSkills>>
    {
        private readonly ISkillDataRepository _repository;
        public SkillSearchCommandHandler(ISkillDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserSkills>> Handle(SkillSearchCommand request, CancellationToken cancellationToken)
        {
           // SkillSubmitDTO req = SkillMapper.Mapper.Map<List<SearchSKills>>(request.SkillName);
            var response = await _repository.SearchSkillByNameOrCode(request.SkillName);           
            return response;

        }

    }
}
