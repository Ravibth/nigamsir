using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.HttpServices.DTOs
{
    public class GetActivePublishedAllocationByPipeLineCodeRequestDTO
    {
        public List<KeyValuePair<string, string?>> PipelineCodes { get; set; }
    }
}
