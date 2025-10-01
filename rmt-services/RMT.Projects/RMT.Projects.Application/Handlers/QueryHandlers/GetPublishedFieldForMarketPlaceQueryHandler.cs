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
    public class GetPublishedFieldForMarketPlaceQuery : IRequest<List<PublishedFieldForMarketPlace>>
    {
        //public string ProjectCode { get; set; }//feb
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }

    }

    public class GetPublishedFieldForMarketPlaceQueryHandler : IRequestHandler<GetPublishedFieldForMarketPlaceQuery, List<PublishedFieldForMarketPlace>>
    {
        private readonly IProjectRepository _ProjectRepo;
        public GetPublishedFieldForMarketPlaceQueryHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<List<PublishedFieldForMarketPlace>> Handle(GetPublishedFieldForMarketPlaceQuery request, CancellationToken cancellationToken)
        {
            return await _ProjectRepo.GetPublishedFieldForMarketPlace(request.PipelineCode, request.JobCode);
        }
    }
}
