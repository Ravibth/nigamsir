namespace RMT.Scheduler.DTOs
{
    public class UpdateWorkflowDto
    {
        public string? workflow_id { get; set; }
        public string? item_id { get; set; }
        public string? workflow_task_id { get; set; }
        public string? assigned_to { get; set; }
        public string? status { get; set; }
        public string? remarks { get; set; }
        public string? workflow_task_title { get; set; }
        public string? comment { get; set; }
        public string? type { get; set; }
        public string? workflow_table_status { get; set; }
        public string? workflow_table_outcome { get; set; }
        public string? parent_id { get; set; }
        public string? updated_by { get; set; }
        public string? service_line { get; set; }
        public string? proxy_approval_by { get; set; }
        public string? next_step { get; set; }
        public string? MyProperty { get; set; }

    }
}
