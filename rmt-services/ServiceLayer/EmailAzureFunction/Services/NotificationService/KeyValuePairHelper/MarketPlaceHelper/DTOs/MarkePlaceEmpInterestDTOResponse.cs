using System;

namespace ServiceLayer.Services.NotificationService.KeyValuePairHelper.MarketPlaceHelper.DTOs
{
    public class MarkePlaceEmpInterestDTOResponse
    {
        public Int64? Id { get; set; }
        public string? ProjectCode { get; set; }
        public string? ProjectName { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public string? PipelineCode { get; set; }
        public string? PipelineName { get; set; }
        public bool? IsInterested { get; set; }
        public DateTime? InterestDate { get; set; }
        public Int64? EmpMID { get; set; }
        public string? EmpEmail { get; set; }
        public string? EmpName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public Int64 NoOfInterested { get; set; }

    }
}
