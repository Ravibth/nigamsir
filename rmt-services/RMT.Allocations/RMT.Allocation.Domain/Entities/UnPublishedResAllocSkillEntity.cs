using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.Entities
{
    public class UnPublishedResAllocSkillEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid RequisitionId { get; set; }
        public Guid UnPublishedResAllocDetailsId { get; set; }
        public string SkillName { get; set; }
        public string SkillCode { get; set; }
        [ForeignKey("RequisitionId")]
        public virtual Requisition Requisition { get; set; }
        [ForeignKey("UnPublishedResAllocDetailsId")]
        public virtual UnPublishedResAllocDetails ResAllocDetails { get; set; }
        public virtual List<UnPublishedResAllocSkillEntity> Skills { get; set; }
    }
}
