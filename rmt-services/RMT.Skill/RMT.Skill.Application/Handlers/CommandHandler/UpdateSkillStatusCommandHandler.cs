using MediatR;
using Microsoft.AspNetCore.Http;
using RMT.Skill.Application.Mappers;
using RMT.Skill.Domain.Entities;
using RMT.Skill.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.Handlers.CommandHandler
{
    public class SkillStatusUpdateCommand : IRequest<Boolean>
    {
        public string SkillCode { get; set; }
        //public string SkillName { get; set; }
        public Boolean IsEnable { get; set; }

        public string ModifyBy { get; set; }
    }

    public class UpdateSkillStatusCommandHandler : IRequestHandler<SkillStatusUpdateCommand, Boolean>
    {
        private readonly ISkillDataRepository _repository;
        private readonly IUserSkillRepository _userRepo;
        public UpdateSkillStatusCommandHandler(ISkillDataRepository repository, IUserSkillRepository userSkillRepository)
        {
            _repository = repository;
            _userRepo= userSkillRepository;
        }
        public async Task<Boolean> Handle(SkillStatusUpdateCommand request, CancellationToken cancellationToken)
        {
            var skillEntity = await _repository.GetSkillByCode(request.SkillCode);
            if (skillEntity == null)
            {
                throw new BadHttpRequestException("Skill Not Present.", StatusCodes.Status400BadRequest);
            }
            else
            {
                skillEntity.IsEnable = request.IsEnable;
                skillEntity.IsActive = true;
                skillEntity.ModifieDate = DateTime.UtcNow;
                skillEntity.ModifiedBy = request.ModifyBy;
            }
            var response = await _repository.UpdateSkill(skillEntity);
            if (response != null)
            {
                await _userRepo.DeactiveUserSkill(request.SkillCode, request.IsEnable);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
