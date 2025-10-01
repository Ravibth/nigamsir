using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMT.Allocation.Domain.Entities
{
    public class RequisitionStatuses
    {
        public const string PENDING = "Pending";
        public const string ALLOCATED = "Allocated";
        public const string REJECTED = "Rejected";
        //public const string ONGOING = "Ongoing";
        //public const string PENDING_APPROVAL = "Pending Approval";
        public const string APPROVED = "Approved";
        //public const string DRAFT = "Draft";
        public const string RELEASED = "Released";
        public const string SUSPENDED = "Suspended";

    }
    public class Requisition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid RequisitionDemand { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobName { get; set; }
        public string? ClientName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        [Range(Int64.MinValue, Int64.MaxValue)]
        public Int64 EffortsPerDay { get; set; }
        public Int64 TotalHours { get; set; }
        public string RequisitionStatus { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public string Description { get; set; }
        public bool IsPerDayHourAllocation { get; set; }
        public string BusinessUnit { get; set; }
        public string CompetencyId { get; set; }
        public string Competency { get; set; }
        public string Offerings { get; set; } = "1";
        public string Solutions { get; set; } = "1";

        public string OfferingsId { get; set; } = "1";
        public string SolutionsId { get; set; } = "1";

        public string BUId { get; set; } = "1";

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        [DefaultValue(RequisitionTypeData.DefaultRequisitionTypeId)]
        public Int64 RequisitionTypeId { get; set; }
        [ForeignKey("RequisitionTypeId")]
        public virtual RequisitionType? RequisitionType { get; set; }
        [ForeignKey("RequisitionDemand")]
        public virtual RequisitionDemand? demands { get; set; }
        public virtual List<RequisitionParameters>? RequisitionParameters { get; set; }
        public virtual List<RequisitionSkill>? RequisitionSkill { get; set; }
        public virtual List<RequisitionParameterValues>? RequisitionParameterValues { get; set; }
    }
}
