using MediatR;
using RMT.MarketPlace.Application.DTO;
using RMT.MarketPlace.Application.IHttpServices;
using RMT.MarketPlace.Application.Mappers;
using RMT.MarketPlace.Domain.Entities;
using RMT.MarketPlace.Domain.Repositories;
using RMT.Projects.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RMT.MarketPlace.Application.Handlers.QueryHandlers
{
    public class GetAllMarketPlaceFiltersQuery : IRequest<MarketPlaceFiltersDTO>
    {

    }

    public class GetAllMarketPlaceFiltersQueryHandler : IRequestHandler<GetAllMarketPlaceFiltersQuery, MarketPlaceFiltersDTO>
    {
        private readonly IMarketPlaceRepository _Repo;

        public GetAllMarketPlaceFiltersQueryHandler(IMarketPlaceRepository repository)
        {
            _Repo = repository;
        }

        public async Task<MarketPlaceFiltersDTO> Handle(GetAllMarketPlaceFiltersQuery request, CancellationToken cancellationToken)
        {
            var resultBU = await _Repo.GetAllMarketPlaceBU();
            var resultOfferings = await _Repo.GetAllMarketPlaceOfferings();
            var resultSolutions = await _Repo.GetAllMarketPlaceSolutions();
            var resultIndustry = await _Repo.GetAllMarketPlaceIndustry();
            var resultSubIndustry = await _Repo.GetAllMarketPlaceSubIndustry();
            var resultLocation = await _Repo.GetAllMarketPlaceLocation();

            MarketPlaceFiltersDTO responseObj = new MarketPlaceFiltersDTO();

            responseObj.buFiltervalue = resultBU;
            responseObj.offeringsFiltervalue= resultOfferings;
            responseObj.solutionsFiltervalue = resultSolutions;
            responseObj.industryFiltervalue = resultIndustry;
            responseObj.subIndustryFiltervalue = resultSubIndustry;
            responseObj.locationFiltervalue = resultLocation;

            return responseObj;

        }
    }
}










