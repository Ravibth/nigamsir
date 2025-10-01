using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs
{
    public class ChangeCodeDTO
    { 
        public string? pipelineCode { get; set; }       
        public string? jobCode { get; set; }
        public string? newJobCode { get; set; }
        public string? modifiedBy { get; set; }
        public string? newJobName { get; set; }
        public string? newPipelineCode { get; set; }
        
    }
}
