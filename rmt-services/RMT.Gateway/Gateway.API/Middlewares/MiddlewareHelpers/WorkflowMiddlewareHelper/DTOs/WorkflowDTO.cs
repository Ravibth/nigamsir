using System;
using System.Collections.Generic;

namespace Gateway.API.Middlewares.MiddlewareHelpers.WorkflowMiddlewareHelper.DTOs
{
    public class WorkflowDTO
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? module { get; set; }
        public string? sub_module { get; set; }
        public string? item_id { get; set; }
        public string? outcome { get; set; }
        public string? status { get; set; }
        public string? created_by { get; set; }
        public string? updated_by { get; set; }
        public string? entity_type { get; set; }
        public dynamic? entity_meta_data { get; set; }
        public bool? is_active { get; set; }
        public string? parent_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string[]? actions { get; set; }
        public List<WorkflowTaskDTO>? task_list { get; set; }
    }
}
