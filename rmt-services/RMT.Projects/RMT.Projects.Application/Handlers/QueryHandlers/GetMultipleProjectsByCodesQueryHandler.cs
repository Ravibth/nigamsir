using MediatR;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{

    public class GetMultipleProjectsByCodesQuery : IRequest<List<Project>>
    {
        public List<KeyValuePair<string, string>> ProjectCode { get; set; }
    }

    public class GetMultipleProjectsByCodesQueryHandler : IRequestHandler<GetMultipleProjectsByCodesQuery, List<Project>>
    {
        private readonly IProjectRepository _ProjectRepo;
        public GetMultipleProjectsByCodesQueryHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<List<Project>> Handle(GetMultipleProjectsByCodesQuery request, CancellationToken cancellationToken)
        {
            return await _ProjectRepo.GetMultipleProjectByCodes(request.ProjectCode);
            //return await Task.FromResult(new List<Project>());
        }
    }
}
