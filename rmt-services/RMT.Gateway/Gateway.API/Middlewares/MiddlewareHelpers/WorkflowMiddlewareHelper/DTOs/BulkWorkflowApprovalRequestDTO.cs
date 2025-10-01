namespace Gateway.API.Middlewares.MiddlewareHelpers.WorkflowMiddlewareHelper.DTOs
{
    public class BulkWorkflowApprovalRequestDTO
    {
        public string workflow_id { get; set; }
        public string item_id { get; set; }
        public string workflow_task_id { get; set; }
        public string assigned_to { get; set; }
        public string status { get; set; }
        public string remarks { get; set; }
        public string workflow_task_title { get; set; }
    }
}