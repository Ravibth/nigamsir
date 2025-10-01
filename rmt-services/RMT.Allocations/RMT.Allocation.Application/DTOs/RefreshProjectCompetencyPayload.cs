using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs
{
    public class RefreshProjectCompetencyPayload
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
    }
}
