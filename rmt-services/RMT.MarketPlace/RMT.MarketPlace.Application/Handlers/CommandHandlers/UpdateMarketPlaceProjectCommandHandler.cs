using MediatR;
using RMT.MarketPlace.Application.Responses;
using RMT.MarketPlace.Domain.Entities;
using RMT.MarketPlace.Domain.Repositories;
using RMT.Projects.Application.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.Handlers.CommandHandlers
{
    public class UpdateMarketPlaceProjectCommand : IRequest<MarketPlaceProjectDetailResponse>
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public string? PipelineName { get; set; }
        public string? ClientName { get; set; }
        public string? ClientGroup { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public bool? IsActive { get; set; }

        public string? ChargableType { get; set; }
        public string? Location { get; set; }
        public string? BusinessUnit { get; set; }

        public string? BUId { get; set; }

        public string? Offerings { get; set; }
        public string? Solutions { get; set; }

        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }

        public string? Industry { get; set; }
        public string? Subindustry { get; set; }
    }

    public class UpdateMarketPlaceProjectCommandHandler : IRequestHandler<UpdateMarketPlaceProjectCommand, MarketPlaceProjectDetailResponse>
    {
        private readonly IMarketPlaceRepository _MarketPlaceRepository;
        public UpdateMarketPlaceProjectCommandHandler(IMarketPlaceRepository MarketPlaceRepository)
        {
            _MarketPlaceRepository = MarketPlaceRepository;
        }

        public async Task<MarketPlaceProjectDetailResponse> Handle(UpdateMarketPlaceProjectCommand request, CancellationToken cancellationToken)
        {
            MarketPlaceProjectDetail entitiy = MarketPlaceMapper.Mapper.Map<MarketPlaceProjectDetail>(request);
            if (entitiy is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            MarketPlaceProjectDetail newEntity = await _MarketPlaceRepository.UpdateMarketPlaceProjectDetails(entitiy);
            MarketPlaceProjectDetailResponse ProjectResponse = MarketPlaceMapper.Mapper.Map<MarketPlaceProjectDetailResponse>(newEntity);
            return ProjectResponse;
        }
    }
}
