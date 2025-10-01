using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Application.DTOs
{
    public class AllocationByEmailResponse
    {
        public Guid Id { get; set; }
        public string EmpEmail { get; set; }
        public string PipelineCode { get; set; }
        public string PipelineName { get; set; }
        public string? JobCode { get; set; }
        public string? JobId { get; set; }
        public string? JobName { get; set; }
        public Guid RequisitionId { get; set; }
        public Guid? PublishedResAllocDetailsId { get; set; }
        public Guid? UnPublishedResAllocDetailsId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Int64 Efforts { get; set; }
        public bool IsPerDayAllocation { get; set; }
        public double RatePerHour { get; set; }
        public Int64 TotalWorkingDays { get; set; }
        public string? Currency { get; set; }
        public List<ResourceAllocationDaysResponse>? ResourceAllocationDays { get; set; }
        //Use for identifying published/unpublished type
        public string Type { get; set; }
        public Requisition? Requisition { get; set; }
        public List<PublishedResAllocSkillEntity> PublishedResAllocSkillEntity { get; set; }
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
        //[DefaultValue(RequisitionTypeData.DefaultRequisitionTypeId)]
        //public Int64 RequisitionTypeId { get; set; }
        //[ForeignKey("RequisitionTypeId")]
        //public virtual RequisitionType? RequisitionType { get; set; }
        //[ForeignKey("RequisitionDemand")]
        //public virtual RequisitionDemand? demands { get; set; }
        //public virtual List<RequisitionParameters>? RequisitionParameters { get; set; }
        public virtual List<RequisitionSkill>? RequisitionSkill { get; set; }
        //public virtual List<RequisitionParameterValues>? RequisitionParameterValues { get; set; }
    }

    public class RequisitionSkill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid RequisitionId { get; set; }
        public string SkillName { get; set; }
        public string SkillCode { get; set; }
        public string Type { get; set; }
        [ForeignKey("RequisitionId")]
        public virtual Requisition Requisition { get; set; }
    }

    public class PublishedResAllocSkillEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid RequisitionId { get; set; }
        public Guid PublishedResAllocDetailsId { get; set; }
        public string SkillName { get; set; }
        public string SkillCode { get; set; }
    }

    public class ResourceAllocationDaysResponse
    {
        public Guid Id { get; set; }
        public string? PipelineName { get; set; }
        public string? JobName { get; set; }
        public string EmailId { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public double RatePerHour { get; set; }
        public string? Currency { get; set; }
        public Guid RequisitionId { get; set; }
        public Guid? UnPublishedResAllocId { get; set; }
        public Guid? PublishedResAllocId { get; set; }
        public Int64 Efforts { get; set; }
        public DateOnly AllocationDate { get; set; }
        public string Type { get; set; }

        public Requisition? Requisition { get; set; }
    }
}
