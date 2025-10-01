using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RMT.Allocation.Domain.Entities
{
    public class PublishedResAlloc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid RequisitionId { get; set; }
        public Guid PublishedResAllocDetailsId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Int64 Efforts { get; set; }
        public bool IsPerDayAllocation { get; set; }
        public double RatePerHour { get; set; }
        public Int64 TotalWorkingDays { get; set; }
        [DefaultValue(ConstantsDomain.RUPEES)]
        public string? Currency { get; set; }
        [ForeignKey("RequisitionId")]
        public virtual Requisition Requisition { get; set; }
        [ForeignKey("PublishedResAllocDetailsId")]
        public virtual PublishedResAllocDetails ResourceAllocationsDetails { get; set; }
        public virtual List<PublishedResAllocDays> ResourceAllocationsDays { get; set; }
        [ForeignKey("RequisitionId")]
        public virtual PublishedResAllocSkillEntity PublishedResAllocSkillEntity { get; set; }
    }
}
