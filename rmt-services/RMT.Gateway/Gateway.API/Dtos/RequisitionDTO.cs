using static Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper.Constant;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Reflection;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gateway.API.Dtos
{
    public class RequisitionType
    {
        public int? Id { get; set; }
        public string Type { get; set; }
    }
    public static class RequisitionTypeData
    {
        public const string NamedAllocation = "Named Allocation";
        public const string SameTeamAllocation = "Same Team Allocation";
        public const string CreateRequisition = "Create Requisition";
        public const string RollForwardAllocation = "Roll Forward Allocation";
        public const string BulkAllocation = "Bulk Allocation";
        public const int DeafultRequisitionTypeId = 3;
    }
    public class RequistionStatuses
    {
        public const string PENDING = "Pending";
        public const string REJECTED = "Rejected";
        public const string ONGOING = "Ongoing";
        public const string PENDING_APPROVAL = "Pending Approval";
        public const string APPROVED = "Approved";
        public const string DRAFT = "Draft";
    }
    public class RequisitionDTO
    {

        public Guid Id { get; set; }
        public Guid RequisitionDemand { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobName { get; set; }
        public string? ClientName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Int64 EffortsPerDay { get; set; }
        public Int64 TotalHours { get; set; }
        public string RequisitionStatus { get; set; }
        //public string Expertise { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public string Description { get; set; }
        public bool IsPerDayHourAllocation { get; set; }
        public string BusinessUnit { get; set; }
        //public string SMEG { get; set; }
        public string CompetencyId { get; set; }
        public string Competency { get; set; }
        public string Offerings { get; set; }
        public string Solutions { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Int64 RequisitionTypeId { get; set; }
        public virtual RequisitionType RequisitionType { get; set; }
        //public virtual RequisitionDemand demands { get; set; }
        //public virtual List<RequisitionParameters> RequisitionParameters { get; set; }
        //public virtual List<RequisitionSkill> RequisitionSkill { get; set; }
        //public virtual List<RequisitionParameterValues> RequisitionParameterValues { get; set; }



        //public Int64? RequisionId { get; set; }
        //public Int64? RequisitionDemand { get; set; }
        //public string? PipelineCode { get; set; }
        //public string? JobCode { get; set; }
        //public string? ProjectCode { get; set; }
        //public string? ProjectName { get; set; }
        //public string? ClientName { get; set; }
        //public string? RequisitionDescription { get; set; }
        //public bool? IsContinuousAllocation { get; set; }
        //public DateTime? StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
        //public string? EffortsPerDay { get; set; }
        //public string? TotalHours { get; set; }
        //public string? RequisitionStatus { get; set; }
        //public string? Expertise { get; set; }
        ////public string? ExpertiseId { get; set; }
        //public string? SME { get; set; }
        //public string? Designation { get; set; }
        ////public string? DesignationId { get; set; }
        //public string? Description { get; set; }
        //public bool? isPerDayHourAllocation { get; set; }
        //public string? BU { get; set; }
        ////public string? BUId { get; set; }
        //public string? SMEG { get; set; }
        ////public string? SMEGId { get; set; }
        //public string? Industry { get; set; }
        //public string? SubIndustry { get; set; }
        ////public ICollection<ResourceAllocation> ResourceAllocations { get; set; }
        //public bool? IsActive { get; set; }
        //public string? CreatedBy { get; set; }
        //public string? ModifiedBy { get; set; }
        ////[Timestamp]
        //public DateTime? CreatedAt { get; set; }
        ////[Timestamp]
        //public DateTime? ModifiedAt { get; set; }
        //public bool? isPublish { get; set; }

        //public int? RequisitionTypeId { get; set; }

        //public virtual RequisitionType? RequisitionTypes { get; set; }
        ////public virtual List<RequisitionParameters> RequisitionParameters { get; set; }
        ////public virtual List<RequisitionDurations> RequisitionDurations { get; set; }
        ////public virtual List<RequisitionLocation> RequisitionLocation { get; set; }
        ////public virtual List<RequisitionSkill> RequisitionSkill { get; set; }
        ////public virtual RequisitionDemand? demands { get; set; }
        //public DateTime? SuspendedAt { get; set; }
    }
}
