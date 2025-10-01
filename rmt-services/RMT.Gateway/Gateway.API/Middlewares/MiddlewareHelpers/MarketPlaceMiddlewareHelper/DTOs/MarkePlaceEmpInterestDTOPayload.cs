using Microsoft.VisualBasic;
using System;

namespace Gateway.API.Middlewares.MiddlewareHelpers.MarketPlaceMiddlewareHelper.DTOs
{
    public class MarkePlaceEmpInterestDTOPayload
    {
        public string? createdBy { get; set; }
        public string empEmail { get; set; }
        public Int64? empMID { get; set; }
        public string? empName { get; set; }
        public DateTime? interestDate { get; set; }
        public bool? isActive { get; set; }
        public bool? isInterested { get; set; }
        public string? modifiedBy { get; set; }
        public string? pipelineCode { get; set; }
        public string? pipelineName { get; set; }
    }
}
