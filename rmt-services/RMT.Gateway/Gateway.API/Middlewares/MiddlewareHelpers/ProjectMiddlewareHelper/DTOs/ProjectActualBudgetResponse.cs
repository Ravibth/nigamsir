namespace Gateway.API.Middlewares.MiddlewareHelpers.ProjectMiddlewareHelper.DTOs
{
    public class ProjectActualBudgetResponse
    {
        public string Action { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobName { get; set; }
        public string? ProjectName { get; set; }
        public string? sender_for_notification { get; set; }
        public string? ConsumedTimesheetBudgetPct { get; set; }
    }
}
