using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Responses
{
    public class GetCurrentUserAllocationCalanderFilterOptionsResponse
    {
        public List<PipelineCodes> PipelineCodes { get; set; }
        public List<JobCodes> JobCodes { get; set; }
    }

    public class PipelineCodes {
      public string PipelineName { get; set; }
      public string PipelineCode { get; set; }

    }

    public class JobCodes
    {
        public string JobName { get; set; }
        public string JobCode { get; set; }

    }


}
