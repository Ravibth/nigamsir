using MediatR;
using RMT.MarketPlace.Application.DTO;
using RMT.MarketPlace.Application.IHttpServices;
using RMT.MarketPlace.Domain.Repositories;
using RMT.MarketPlace.Infrastructure.Repositories;
using RMT.Projects.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.Handlers.QueryHandlers
{
    public class GetListOfUsersInterestedByPipelineCodeQuery : IRequest<List<string>>
    {
        public string pipelineCode { get; set; }
        public string? jobCode { get; set; }
    }

    public class GetListOfUsersInterestedByPipelineCodeQueryHandler : IRequestHandler<GetListOfUsersInterestedByPipelineCodeQuery, List<string>>
    {
        private readonly IMarketPlaceRepository _repository;

        public GetListOfUsersInterestedByPipelineCodeQueryHandler(IMarketPlaceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<string>> Handle(GetListOfUsersInterestedByPipelineCodeQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetListOfUsersInterestedByPipelineCode(request.pipelineCode, request.jobCode);

        }
    }
}
