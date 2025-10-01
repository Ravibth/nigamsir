using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RMT.Allocation.Domain.ConstantsDomain;

namespace RMT.Allocation.Domain.Entities
{
    public class ResourceAllocationDetailsHistory
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        public Guid ResourceAllocationDetailId { get; set; }
        [Required]
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        //[Required]
        //public string ProjectCode { get; set; }
        public string? JobName { get; set; }
        //[Required]
        public string PipelineName { get; set; }

        [Required]
        public string EmpEmail { get; set; }

        [Required]
        public string? EmpName { get; set; }

        public Int64 RequisitionId { get; set; }

        [ForeignKey("RequisitionId")]
        public virtual Requisition? Requisitions { get; set; }

        [Required]
        [EnumDataType(typeof(EAllocationRecordType))]
        public string RecordType { get; set; }

        [Required]
        public Boolean IsContinuousAllocation { get; set; }
        [Required]
        public string Description { get; set; }

        public int TotalEffort { get; set; }
        public DateTime? AllocationStartDate { get; set; }
        public DateTime? AllocationEndDate { get; set; }

        public string AllocationStatus { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        //[Timestamp]
        public DateTime ModifiedDate { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public DateTime? SuspendedAt { get; set; }

        public virtual List<ResourceAllocation>? ResourceAllocation { get; set; }

        //public virtual List<ResourceAllocationSkillEntity>? ResourceAllocationDetailsSkills { get; set; }
    }
}
