using MediatR;
using RMT.MarketPlace.Application.DTO;
using RMT.MarketPlace.Application.IHttpServices;
using RMT.MarketPlace.Domain.Entities;
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
    public class GetProjectDetailsByPipelineCodeQuery : IRequest<MarketPlaceProjectDetail>
    {
        public string pipelineCode { get; set; }
        public string? jobCode { get; set; }
    }

    public class GetProjectDetailsByPipelineCodeQueryHandler : IRequestHandler<GetProjectDetailsByPipelineCodeQuery, MarketPlaceProjectDetail>
    {
        private readonly IMarketPlaceRepository _repository;

        public GetProjectDetailsByPipelineCodeQueryHandler(IMarketPlaceRepository repository)
        {
            _repository = repository;
        }

        public async Task<MarketPlaceProjectDetail> Handle(GetProjectDetailsByPipelineCodeQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetMarketPlaceProjectDetails(request.pipelineCode, request.jobCode);

        }
    }
}
