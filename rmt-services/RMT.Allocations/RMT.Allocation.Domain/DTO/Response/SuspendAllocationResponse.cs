using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO.Response
{
    public class SuspendAllocationResponse
    {
        public string PipelineCode { get; set; }
        public string JobCode { get; set; }
        public string EmpEmail { get; set; }
    }
}
