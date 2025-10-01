namespace Gateway.API.Middlewares.MiddlewareHelpers.MarketPlaceMiddlewareHelper.DTOs
{
    public class RefreshEmpProjectInterestScoreRequestDTO
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string RequisitionActionType { get; set; }
    }
}
