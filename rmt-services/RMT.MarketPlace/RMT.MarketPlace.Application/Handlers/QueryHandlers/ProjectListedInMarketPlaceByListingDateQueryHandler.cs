using AutoMapper;
using MediatR;
using RMT.MarketPlace.Application.DTO;
using RMT.MarketPlace.Domain.Entities;
using RMT.MarketPlace.Domain.Repositories;
using RMT.Projects.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.Handlers.QueryHandlers
{
    public class ProjectListedInMarketPlaceByListingDateQuery : IRequest<List<MarketPlaceProjectDetailDTO>>
    {
        public string? MarketPlacePublishDate { get; set; }
    }
    public class ProjectListedInMarketPlaceByListingDateQueryHandler : IRequestHandler<ProjectListedInMarketPlaceByListingDateQuery, List<MarketPlaceProjectDetailDTO>>
    {
        private readonly IMarketPlaceRepository _marketPlaceRepository;
        private readonly IMapper _mapper;
        public ProjectListedInMarketPlaceByListingDateQueryHandler(IMarketPlaceRepository marketPlaceRepository, IMapper mapper)
        {
            _marketPlaceRepository = marketPlaceRepository;
            _mapper = mapper;
        }
        public async Task<List<MarketPlaceProjectDetailDTO>> Handle(ProjectListedInMarketPlaceByListingDateQuery request, CancellationToken cancellationToken)
        {
            string dateTimeFormat = "dd-MM-yyyy";

            var tempDate = DateTime.ParseExact(request.MarketPlacePublishDate, dateTimeFormat, CultureInfo.InvariantCulture);
            var date = DateOnly.FromDateTime(tempDate);
            List<MarketPlaceProjectDetail> marketPlaceDetails = await _marketPlaceRepository.MarketPlaceProjectListByParams(date);
            List<MarketPlaceProjectDetailDTO> result = MarketPlaceMapper.Mapper.Map<List<MarketPlaceProjectDetailDTO>>(marketPlaceDetails);
            return result;
        }
    }
}
