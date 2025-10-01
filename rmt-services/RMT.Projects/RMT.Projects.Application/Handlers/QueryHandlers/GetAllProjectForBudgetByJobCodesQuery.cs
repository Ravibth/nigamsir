using MediatR;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetAllProjectForBudgetByJobCodesQuery : IRequest<List<BasicProjectDetailsRequestorResponse>>
    {
        public List<string> JobCodes { get; set; }
    }

    public class GetAllProjectForBudgetByJobCodesQueryHandler : IRequestHandler<GetAllProjectForBudgetByJobCodesQuery, List<BasicProjectDetailsRequestorResponse>>
    {
        private readonly IProjectRepository _ProjectRepo;

        public GetAllProjectForBudgetByJobCodesQueryHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<List<BasicProjectDetailsRequestorResponse>> Handle(GetAllProjectForBudgetByJobCodesQuery request, CancellationToken cancellationToken)
        {
            var result = await _ProjectRepo.GetAllProjectForBudgetByJobCodes(request.JobCodes);
            var response = ProjectMapper.Mapper.Map<List<BasicProjectDetailsRequestorResponse>>(result);
            return response;
        }
    }
}
