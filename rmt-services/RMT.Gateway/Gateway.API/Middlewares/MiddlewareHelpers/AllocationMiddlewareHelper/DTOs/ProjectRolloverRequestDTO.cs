using System;

namespace Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper.DTOs
{
    public class ProjectRolloverRequestDTO
    {
        public string? Message { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public DateTime PipelineStartDate { get; set; }
        public DateTime PipelineOldStartDate { get; set; }
    }
}
