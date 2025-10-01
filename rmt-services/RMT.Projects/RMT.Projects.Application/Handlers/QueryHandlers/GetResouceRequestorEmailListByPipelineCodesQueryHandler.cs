using MediatR;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetResouceRequestorEmailListByPipelineCodesQuery : IRequest<List<GetResouceRequestorEmailListByPipelineCodesResponse>>
    {
        public List<KeyValuePair<string, string?>> PipelineCodes { get; set; }
    }
    public class GetResouceRequestorEmailListByPipelineCodesQueryHandler : IRequestHandler<GetResouceRequestorEmailListByPipelineCodesQuery, List<GetResouceRequestorEmailListByPipelineCodesResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetResouceRequestorEmailListByPipelineCodesQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<GetResouceRequestorEmailListByPipelineCodesResponse>> Handle(GetResouceRequestorEmailListByPipelineCodesQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetRequestorEmailsListByPipelineCode(request.PipelineCodes);
            var response = projects.Select(d => new GetResouceRequestorEmailListByPipelineCodesResponse
            {
                PipelineCode = d.PipelineCode,
                //Need to check where this is used?
                ResourceRequestors = d.ProjectRoles.Select(x => x.User).ToList()
            }).ToList();
            return response;
        }
    }
}
