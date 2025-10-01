using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.Entities
{
    public class RequisitionParameterValues
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid RequisitionId { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
        [ForeignKey("RequisitionId")]
        public virtual Requisition Requisition { get; set; }
    }
}
