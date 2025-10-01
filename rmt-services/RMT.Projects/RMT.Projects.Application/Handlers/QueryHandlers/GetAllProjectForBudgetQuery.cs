using MediatR;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetAllProjectForBudgetQuery : IRequest<List<BasicProjectDetailsRequestorResponse>>
    {
    }

    public class GetAllProjectForBudgetQueryHandler : IRequestHandler<GetAllProjectForBudgetQuery, List<BasicProjectDetailsRequestorResponse>>
    {
        private readonly IProjectRepository _ProjectRepo;

        public GetAllProjectForBudgetQueryHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<List<BasicProjectDetailsRequestorResponse>> Handle(GetAllProjectForBudgetQuery request, CancellationToken cancellationToken)
        {
            var result = await _ProjectRepo.GetAllProjectForBudget();
            var response = ProjectMapper.Mapper.Map<List<BasicProjectDetailsRequestorResponse>>(result);
            return response;
        }
    }
}
