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
    public class GetSkillByNameQuery : IRequest<Skills>
    {
        public string SkillName { get; set; }
    }
    public class GetSkillByNameQueryHandler : IRequestHandler<GetSkillByNameQuery, Skills>
    {
        private readonly ISkillDataRepository _repository;
        public GetSkillByNameQueryHandler(ISkillDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<Skills> Handle(GetSkillByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetSkillByName(Convert.ToString(request.SkillName));
            return result;
        }
    }
}
