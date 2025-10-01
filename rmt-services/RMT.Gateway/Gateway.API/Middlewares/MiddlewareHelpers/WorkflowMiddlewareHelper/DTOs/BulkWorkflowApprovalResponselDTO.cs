using System.Collections.Generic;

namespace Gateway.API.Middlewares.MiddlewareHelpers.WorkflowMiddlewareHelper.DTOs
{
    public class BulkWorkflowApprovalResponselDTO
    {
        public BulkWorkflowApprovalRequestDTO requestPayload { get; set; }
        public WorkflowDTO workflowResult { get; set; }
        public string[] actions { get; set; }
        public List<WorkflowTaskDTO> result { get; set; }
        public dynamic error { get; set; }
        public bool isError { get; set; }

    }
}
