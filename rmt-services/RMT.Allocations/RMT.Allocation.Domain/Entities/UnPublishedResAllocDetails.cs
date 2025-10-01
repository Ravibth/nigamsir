using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.Entities
{
    public class UnPublishedResAllocDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid? ParentPublishedResAllocDetailsId { get; set; }
        public Guid RequisitionId { get; set; }
        public string EmpEmail { get; set; }
        public string EmpName { get; set; }
        public string Description { get; set; }
        public Int64 TotalEffort { get; set; }
        public string AllocationStatus { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }       
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Int64 AllocationVersion { get; set; }
        [ForeignKey("RequisitionId")]
        public virtual Requisition Requisition { get; set; }
        public virtual List<UnPublishedResAllocSkillEntity>? Skills { get; set; }
        public virtual List<UnPublishedResAlloc> ResourceAllocations { get; set; }
        [ForeignKey("ParentPublishedResAllocDetailsId")]
        public virtual PublishedResAllocDetails? ParentResAllocDetail { get; set; }
    }
}
