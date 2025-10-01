using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class ProjectRequisitionAllocation
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public int RequisitionCount { get; set; }
        public int AllocationCount { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
