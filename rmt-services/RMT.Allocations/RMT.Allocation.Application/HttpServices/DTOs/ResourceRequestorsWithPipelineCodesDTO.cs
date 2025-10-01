using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class ResourceRequestorsWithPipelineCodesDTO
    {
        public string PipelineCode { get; set; }
        public List<string> ResourceRequestors { get; set; }
    }
}
