using MediatR;
using RMT.Skill.Application.Mappers;
using RMT.Skill.Domain.DTOs;
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
    public class SkillSubmitCommand : IRequest<Boolean>
    {
        public string SkillCode{ get; set; }
        public string SkillName { get; set; }
        public string SkillCategory { get; set; }

        public string Description { get; set; }
        public string Basic { get; set; }

        public string Intermediate { get; set; }
        public string Advanced { get; set; }
        public string Expert { get; set; }
        public string CreatedBy { get; set; }

        public List<SkillMappingDTO> Mapping { get; set; }
    }
    public class SkillSubmitCommandHandler : IRequestHandler<SkillSubmitCommand,Boolean>
    {
        private readonly ISkillDataRepository _repository;
        public SkillSubmitCommandHandler(ISkillDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<Boolean> Handle(SkillSubmitCommand request, CancellationToken cancellationToken)
        {
            SkillSubmitDTO req = SkillMapper.Mapper.Map<SkillSubmitDTO>(request);
  
            var skillEntity = await _repository.GetSkillByCode(request.SkillCode);
            if (skillEntity == null)
            {
                skillEntity = new Skills();
                skillEntity.CreateDate = DateTime.UtcNow;
                skillEntity.CreatedBy = request.CreatedBy;
            }
            else
            {
                skillEntity.ModifieDate = DateTime.UtcNow;
                skillEntity.ModifiedBy = request.CreatedBy;
            }
            skillEntity.SkillName = request.SkillName.Trim();
            skillEntity.SkillCategory = request.SkillCategory;
            skillEntity.Basic = request.Basic;
            skillEntity.Intermediate = request.Intermediate;
            skillEntity.Advanced = request.Advanced;
            skillEntity.Expert = request.Expert;
            skillEntity.Description = request.Description;
            skillEntity.IsActive = true;
            skillEntity.IsEnable = true;
            var mapping = SkillMapper.Mapper.Map<List<SkillMapping>>(request.Mapping);
            foreach ( var mappingItem in mapping)
            {
                mappingItem.IsActive = true;
                mappingItem.CreateDate = DateTime.UtcNow;
                mappingItem.CreatedBy = request.CreatedBy;
            }

            skillEntity.Skill_Mapping = mapping;

            var response = await _repository.AddOrUpdateSkill(skillEntity);
           
            return response;

        }

    }
}
