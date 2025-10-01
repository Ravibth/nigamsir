using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Responses
{
    public class DeleteRequisitionResponse
    {
        public bool? is_deleted { get; set; }
        public string? pipelineCode { get; set; }
        public string? jobCode { get; set; }
        public string? Type { get; set; }
    }
}
