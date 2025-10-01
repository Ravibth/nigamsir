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
    public class GetSkillCategoryMasterQuery : IRequest<List<SkillCategoryMaster>>
    {
    }
    public class GetSkillCategoryMasterQueryHandler :  IRequestHandler<GetSkillCategoryMasterQuery, List<SkillCategoryMaster>>
    {
        private readonly ISkillDataRepository _repository;
        public GetSkillCategoryMasterQueryHandler(ISkillDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SkillCategoryMaster>> Handle(GetSkillCategoryMasterQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllSkillsCategory();
            return result;
        }
    }
    
}
