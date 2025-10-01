using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class WorkflowTaskDTO
    {
        public Guid Id { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedToUserName { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string ProxyApprovalBy { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid WorkflowId { get; set; }
        public WorkflowEntity Workflow { get; set; }
    }

    public class WorkflowEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Module { get; set; }
        public string SubModule { get; set; }
        public Guid ItemId { get; set; }
        public string Outcome { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string EntityType { get; set; }
        [JsonProperty("entity_meta_data")]
        public EntityMetaData EntityMetaData { get; set; }
        public bool IsActive { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class EntityMetaData
    {
        public Guid Id { get; set; }
        public Guid RequisitionId { get; set; }
        public string EmpEmail { get; set; }
        public string PipelineCode { get; set; }
        public string PipelineName { get; set; }
        public string JobCode { get; set; }
        public string JobName { get; set; }
        public string EmpName { get; set; }
        public string Description { get; set; }
        public int TotalEffort { get; set; }
        public string AllocationStatus { get; set; }
        public string Grade { get; set; }
        public string Designation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ConfirmedAllocationDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int AllocationVersion { get; set; }
        public Requisition Requisition { get; set; }
        public string Type { get; set; }
    }

    public class Requisition
    {
        public Guid Id { get; set; }
        public Guid RequisitionDemand { get; set; }
        public string PipelineCode { get; set; }
        public string JobCode { get; set; }
        public string PipelineName { get; set; }
        public string JobName { get; set; }
        public string ClientName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EffortsPerDay { get; set; }
        public int TotalHours { get; set; }
        public string RequisitionStatus { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public string Description { get; set; }
        public bool IsPerDayHourAllocation { get; set; }
        public string BusinessUnit { get; set; }
        public string CompetencyId { get; set; }
        public string Competency { get; set; }
        public string Offerings { get; set; }
        public string Solutions { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int RequisitionTypeId { get; set; }
        public RequisitionType RequisitionType { get; set; }
    }

    public class RequisitionType
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }

}
