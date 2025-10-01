using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMT.Allocation.Domain.ConstantsDomain;

namespace RMT.Allocation.Domain.Entities
{
    public class ResourceAllocationHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        public Guid ResourceAllocationDetailGuid { get; set; }

        //[Required]
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
        //[Required]
        //public string? ProjectCode { get; set; }
        public string? JobName { get; set; }
        //[Required]
        public string? PipelineName { get; set; }
        public string? EmpEmail { get; set; }
        public string? EmpName { get; set; }
        public string? ClientName { get; set; }
        public DateTime? ConfirmedAllocationStartDate { get; set; }
        public DateTime? ConfirmedAllocationEndDate { get; set; }
        //[Required]
        public int ConfirmedPerDayHours { get; set; }
        public Boolean isPerDayHourAllocation { get; set; }
        public Int64? ResAllocDetailsId { get; set; }

        public virtual ResourceAllocationDetails? ResourceAllocationDetails { get; set; }
        public Int64 RequisitionId { get; set; }
        [ForeignKey("RequisitionId")]
        public virtual Requisition? Requisitions { get; set; }
        //[Required]         
        [EnumDataType(typeof(EAllocationStatus))]
        public string? AllocationStatus { get; set; }
        [EnumDataType(typeof(EAllocationRecordType))]
        public string? RecordType { get; set; }
        public int TotalWorkingDays { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        //[Timestamp]
        public DateTime ModifiedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? SuspendedAt { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public bool isPublish { get; set; }
        //public virtual List<ResourceAllocationSkillEntity>? Skills { get; set; }
    }
}
