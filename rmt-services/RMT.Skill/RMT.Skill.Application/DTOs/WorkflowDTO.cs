using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.DTOs
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
        public DateTime? created_at { get; set; }
        public string? updated_by { get; set; }
        public string? entity_type { get; set; }
        public dynamic? entity_meta_data { get; set; }
        public DateTime? updated_at { get; set; }
        public bool? is_active { get; set; }
        public List<WorkflowTaskModelDTO>? task_list { get; set; }
        //public List<WorkflowHistoryModel>? history { get; set; }
        public string? parent_id { get; set; }
    }

    public class WorkflowTaskModelDTO
    {
        public string? id { get; set; }
        public string? assigned_to { get; set; }
        public string? assigned_to_userName { get; set; }
        public string? comment { get; set; }
        public DateTime? created_at { get; set; }
        public string? created_by { get; set; }
        public string? description { get; set; }
        public DateTime? due_date { get; set; }
        public string? proxy_approval_by { get; set; }
        public string? status { get; set; }
        public string? title { get; set; }
        public string? type { get; set; }
        public DateTime? updated_at { get; set; }
        public string? updated_by { get; set; }
        public List<WorkflowDTO>? workflow { get; set; }
        public string? workflow_id { get; set; }
    }
}
