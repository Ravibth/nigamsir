namespace RMT.Scheduler.DTOs
{
    public class WorkflowRequest
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? module { get; set; }
        public string? sub_module { get; set; }
        public string? item_id { get; set; }
        public string? outcome { get; set; }
        public string? status { get; set; }
        public string? created_by { get; set; }
        public string? created_at { get; set; }
        public string? updated_by { get; set; }
        public string? updated_at { get; set; }
        public bool? is_active { get; set; }
        public string? parent_id { get; set; }
    }
}
