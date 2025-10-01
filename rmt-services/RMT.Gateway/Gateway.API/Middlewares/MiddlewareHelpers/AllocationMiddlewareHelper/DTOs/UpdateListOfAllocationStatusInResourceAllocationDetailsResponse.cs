using System;
using System.Collections.Generic;

namespace Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper.DTOs
{
    public class UpdateListOfAllocationStatusInResourceAllocationDetailsResponse
    {
        public List<Guid> guids { get; set; }
        public string pipelineCode { get; set; }
        public string? jobCode { get; set; }
        public int allocationDeletionCount { get; set; }
        public bool? IsProjectCompetencyRefresh { get; set; }

    }
}
