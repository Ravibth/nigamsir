using System;

namespace ServiceLayer.Services.NotificationService.KeyValuePairHelper.MarketPlaceHelper.DTOs
{
    public class MarkePlaceEmpInterestRequest
    {
        public string? createdBy { get; set; }
        public string empEmail { get; set; }
        public Int64? empMID { get; set; }
        public string? empName { get; set; }
        public DateTime? interestDate { get; set; }
        public bool? isActive { get; set; }
        public string? isInterested { get; set; }
        public string? modifiedBy { get; set; }
        public string? pipelineCode { get; set; }
        public string? pipelineName { get; set; }
    }
}
