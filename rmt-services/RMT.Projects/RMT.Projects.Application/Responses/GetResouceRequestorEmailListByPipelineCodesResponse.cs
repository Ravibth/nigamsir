using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Responses
{
    public class GetResouceRequestorEmailListByPipelineCodesResponse
    {
        public string PipelineCode { get; set; }
        public List<string> ResourceRequestors { get; set; }
    }
}
