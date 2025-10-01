using MediatR;
using RMT.Skill.Application.Handlers.CommandHandler;
using RMT.Skill.Domain.Entities;
using RMT.Skill.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.Handlers.QueryHandler
{
    public class GetAllSkillsQuery : IRequest<List<Skills>>
    {

    }
    public class GetAllSkillQueryHandler : IRequestHandler<GetAllSkillsQuery, List<Skills>>
    {
        private readonly ISkillDataRepository _repository;
        public GetAllSkillQueryHandler(ISkillDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Skills>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllSkillAsync();
            return result;
        }

    }
}

