using System;

namespace Gateway.API.Middlewares.MiddlewareHelpers.MarketPlaceMiddlewareHelper.DTOs
{
    public class UpdateMarketPlaceProjectDTOPayload
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
        public string? Expertise { get; set; }//Recheck
        public string? BusinessUnit { get; set; }
        public string? Smeg { get; set; }//Recheck
        public string? RevenueUnit { get; set; }//Recheck
        public string? BUId { get; set; }

        public string? Offerings { get; set; }
        public string? Solutions { get; set; }

        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }
        public string? Industry { get; set; }
        public string? Subindustry { get; set; }
    }
}
