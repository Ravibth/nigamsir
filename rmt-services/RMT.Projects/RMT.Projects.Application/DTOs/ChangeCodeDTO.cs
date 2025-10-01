using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.DTOs
{
    public class ChangeCodeDTO
    {
        public string? pipelineCode { get; set; }
        public string? jobCode { get; set; }
        public string? newJobCode { get; set; }
        public string? modifiedBy { get; set; }
        public string? newJobName { get; set; }
        public string? newPipelineCode { get; set; }
        public List<ChangeCodeResponseUser>? NewMovedAlloactions { get; set; }
    }

     public class ChangeCodeResponseUser
    {
        public Guid Guid { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string AllocationStatus { get; set; }

    }
}
