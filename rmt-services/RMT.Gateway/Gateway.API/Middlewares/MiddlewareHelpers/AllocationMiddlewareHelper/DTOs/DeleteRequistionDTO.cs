using System;

namespace Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper.DTOs
{
    public class DeleteRequistionRequestDTO
    {
        public Guid Id { get; set; }
    }
    public class DeleteRequisitionResponseDTO
    {
        public bool? is_deleted { get; set; }
        public string? pipelineCode { get; set; }
        public string? jobCode { get; set; }
        public string? Type { get; set; }
    }
}
