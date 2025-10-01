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
    public class GetFieldForMarketPlaceQuery : IRequest<List<FieldForMarketPlace>>
    {

    }

    public class GetFieldForMarketPlaceQueryHandler : IRequestHandler<GetFieldForMarketPlaceQuery, List<FieldForMarketPlace>>
    {
        private readonly IProjectRepository _ProjectRepo;
        public GetFieldForMarketPlaceQueryHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<List<FieldForMarketPlace>> Handle(GetFieldForMarketPlaceQuery request, CancellationToken cancellationToken)
        {
            return await _ProjectRepo.GetFieldForMarketPlace();
            //return await Task.FromResult(new List<Project>());
        }
    }
}
