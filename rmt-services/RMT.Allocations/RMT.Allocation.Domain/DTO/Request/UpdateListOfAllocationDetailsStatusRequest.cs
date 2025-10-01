using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO.Request
{
    public class UpdateListOfAllocationDetailsStatusRequest
    {
        public Guid guid { get; set; }
        public string AllocationStatus { get; set; }
        public string? WorkflowModule { get; set; }
        public string? WorkflowSubModule { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
