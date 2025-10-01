namespace Gateway.API.Middlewares.MiddlewareHelpers.WorkflowMiddlewareHelper.DTOs
{
    public class WorkflowCreateRequestDTO
    {
        public string? name { get; set; }
        public string? module { get; set; }
        public string? sub_module { get; set; }
        public string? item_id { get; set; }
        public string? created_by { get; set; }
        public string? assigned_to { get; set; }
        public string? status { get; set; }
    }
}
