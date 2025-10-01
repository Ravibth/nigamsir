using MediatR;
using RMT.MarketPlace.Application.DTO;
using RMT.MarketPlace.Domain.DTOs.Response;
using RMT.MarketPlace.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.Handlers.QueryHandlers
{
    public class GetMarketPlaceProjectDetailsIntrestQuery : IRequest<List<MarketPlaceProjectDetaillsIntrestDTO>>
    {

    }
    public class GetMarketPlaceProjectDetailsIntrestQueryHandler : IRequestHandler<GetMarketPlaceProjectDetailsIntrestQuery, List<MarketPlaceProjectDetaillsIntrestDTO>>
    {
        private readonly IMarketPlaceRepository _repository;

        public GetMarketPlaceProjectDetailsIntrestQueryHandler(IMarketPlaceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MarketPlaceProjectDetaillsIntrestDTO>> Handle(GetMarketPlaceProjectDetailsIntrestQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetMarketPlaceProjectDetailsIntrest();

        }
    }
}
