using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.HttpServices.DTO
{
    public class ResourceAllocationDetailsResponseForWorkflowMeta
    {
        public Guid? Id { get; set; }
        //public Guid ItemId { get; set; }
        public Guid? RequisitionId { get; set; }
        public string? EmpEmail { get; set; }
        public string? PipelineCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public string? EmpName { get; set; }
        public string? Description { get; set; }
        public Int64? TotalEffort { get; set; }
        public string? AllocationStatus { get; set; }
        public string? Grade { get; set; }
        public string? Designation { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public DateTime? ConfirmedAllocationDate { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Int64? AllocationVersion { get; set; }
        public Object? Requisition { get; set; }
        public string? Type { get; set; }
    }
    public class WorkflowDTO
    {
        public string id { get; set; }
        public string assigned_to { get; set; }
        public string assigned_to_userName { get; set; }
        public string comment { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public string description { get; set; }
        public DateTime? due_date { get; set; }
        public Object? entity_meta_data { get; set; }
        public string proxy_approval_by { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public DateTime? updated_at { get; set; }
        public string updated_by { get; set; }
        public WorkFlowModelDTO workflow { get; set; }
        public string workflow_id { get; set; }
    }
}
