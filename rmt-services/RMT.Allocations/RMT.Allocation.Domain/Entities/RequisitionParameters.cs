using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.Entities
{
    public class RequisitionParameters
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid RequisitionId { get; set; }
        public string Category { get; set; }
        public Int64 RequisitionWeight { get; set; }
        [Required]
        public bool IsChecked { get; set; }
        [ForeignKey("RequisitionId")]
        public virtual Requisition Requisition { get; set; }

    }
}