using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Application.Services.DTO
{
    
    public class PublishedResourceAllocationDayResponse
    {
        public Guid id { get; set; }
        public string? pipelineName { get; set; }
        public string? jobName { get; set; }
        public string emailId { get; set; }
        public string pipelineCode { get; set; }
        public string? jobCode { get; set; }
        public double ratePerHour { get; set; }
        public string? currency { get; set; }
        public Guid requisitionId { get; set; }
        public Guid? unPublishedResAllocId { get; set; }
        public Guid? publishedResAllocId { get; set; }
        public Int64 efforts { get; set; }
        public DateOnly allocationDate { get; set; }
        public string type { get; set; }

        //public Requisition? Requisition { get; set; }
    }
}
