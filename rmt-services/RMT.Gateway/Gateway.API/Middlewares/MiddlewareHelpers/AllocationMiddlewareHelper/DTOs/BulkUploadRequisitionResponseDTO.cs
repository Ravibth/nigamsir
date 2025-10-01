using System.Collections.Generic;

namespace Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper.DTOs
{
    public class BulkRequisitionDTO
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        //public string projectCode { get; set; }
    }
    public class BulkUploadRequisitionResponseDTO
    {
        //Change from dynamic to actual data
        public List<BulkRequisitionDTO> bulkRequisition { get; set; }
        public int totalNumberRequisition { get; set; }
    }
}
