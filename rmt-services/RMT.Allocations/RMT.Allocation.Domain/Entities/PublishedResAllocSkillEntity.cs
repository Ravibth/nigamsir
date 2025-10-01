using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.Entities
{
    public class PublishedResAllocSkillEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid RequisitionId { get; set; }
        public Guid PublishedResAllocDetailsId { get; set; }
        public string SkillName { get; set; }
        public string SkillCode { get; set; }
        [ForeignKey("RequisitionId")]
        public virtual Requisition Requisition { get; set; }
        [ForeignKey("PublishedResAllocDetailsId")]
        public virtual PublishedResAllocDetails ResAllocDetails { get; set; }
    }
}
