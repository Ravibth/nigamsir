using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.Entities
{
    public class AllocationDayResourceGroup
    {   
        public string? empemail { get; set; }
        public string? empname { get; set; }
        public Double? totaltime { get; set; }
        public Double? cost { get; set; }
        [NotMapped]
        public string? PipelineCode { get; set; }
        [NotMapped]
        public string? JobCode { get; set; }
        [NotMapped]
        public Double? TotalActualEfforts { get; set; }
    }
}
