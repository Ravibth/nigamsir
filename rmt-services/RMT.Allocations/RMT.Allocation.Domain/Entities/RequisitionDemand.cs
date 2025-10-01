using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.Entities
{
    public class RequisitionDemand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Int64 TotalDemands { get; set; }
        public Int64 PendingDemands { get; set; }
        public Boolean AllResourcesHaveSameDetails { get; set; }
        public virtual List<Requisition>? Requisitions { get; set; }
        public RequisitionDemand()
        {
            Id = Guid.NewGuid();
        }
    }
}
