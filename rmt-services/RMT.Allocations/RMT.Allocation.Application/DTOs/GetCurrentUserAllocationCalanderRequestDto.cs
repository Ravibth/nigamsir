using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs
{
    public class GetCurrentUserAllocationCalanderFilter
    {
        public List<string>? PipelineCodes { get; set; }
    }
    public class GetCurrentUserAllocationCalanderRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public GetCurrentUserAllocationCalanderFilter FilterParameters { get; set; }
    }
}
