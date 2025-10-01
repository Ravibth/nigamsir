using MediatR;
using Microsoft.AspNetCore.Http;
using RMT.Skill.Application.Mappers;
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
    public class SkillUpdateCommand : IRequest<Skills>
    {
        public string SkillCode { get; set; }
        public string SkillName { get; set; }
        public string SkillCategory { get; set; }

        public string Description { get; set; }
        public string Basic { get; set; }

        public string Intermediate { get; set; }
        public string Advanced { get; set; }
        public string Expert { get; set; }
        public string CreatedBy { get; set; }
        public Boolean IsEnable { get; set; }

        public List<SkillMappingDTO> Mapping { get; set; }
    }
    public class SkillUpdateCommandHandler : IRequestHandler<SkillUpdateCommand, Skills>
    {
        private readonly ISkillDataRepository _repository;
        public SkillUpdateCommandHandler(ISkillDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<Skills> Handle(SkillUpdateCommand request, CancellationToken cancellationToken)
        {
            SkillSubmitDTO req = SkillMapper.Mapper.Map<SkillSubmitDTO>(request);

            var skillEntity = await _repository.GetSkillByCode(request.SkillCode);
            if (skillEntity == null)
            {
                throw new BadHttpRequestException("Skill Not Present.", StatusCodes.Status400BadRequest);
            }
            else
            {
                skillEntity.SkillName = request.SkillName.Trim();
                skillEntity.SkillCategory = request.SkillCategory;
                skillEntity.Basic = request.Basic;
                skillEntity.Intermediate = request.Intermediate;
                skillEntity.Advanced = request.Advanced;
                skillEntity.Expert = request.Expert;
                skillEntity.Description = request.Description;
                skillEntity.IsEnable = request.IsEnable;
                skillEntity.IsActive = true;

                var mapping = SkillMapper.Mapper.Map<List<SkillMapping>>(request.Mapping);
                foreach (var mappingItem in mapping)
                {
                    mappingItem.IsActive = true;
                    mappingItem.CreateDate = DateTime.UtcNow;
                    mappingItem.CreatedBy = request.CreatedBy;
                }

                skillEntity.Skill_Mapping = mapping;
                skillEntity.ModifieDate = DateTime.UtcNow;
                skillEntity.ModifiedBy = request.CreatedBy;
            }

            var response = await _repository.UpdateSkill(skillEntity);

            return response;

        }
    }
}
