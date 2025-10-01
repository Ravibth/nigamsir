using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.AllocationService.DTOs
{
    public class ResourceAllocationDaysResponse
    {
        public Guid Id { get; set; }
        public string? PipelineName { get; set; }
        public string? JobName { get; set; }
        public string EmailId { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public double RatePerHour { get; set; }
        public string? Currency { get; set; }
        public Guid RequisitionId { get; set; }
        public Guid? UnPublishedResAllocId { get; set; }
        public Guid? PublishedResAllocId { get; set; }
        public Int64 Efforts { get; set; }
        public DateOnly AllocationDate { get; set; }
        public string Type { get; set; }

    }
    public class ResourceAllocationResponseDTO
    {
        public Guid Id { get; set; }
        public string EmpEmail { get; set; }
        public string PipelineCode { get; set; }
        public string PipelineName { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public Guid RequisitionId { get; set; }
        public Guid? PublishedResAllocDetailsId { get; set; }
        public Guid? UnPublishedResAllocDetailsId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Int64 Efforts { get; set; }
        public bool IsPerDayAllocation { get; set; }
        public double RatePerHour { get; set; }
        public Int64 TotalWorkingDays { get; set; }
        public string? Currency { get; set; }
        public List<ResourceAllocationDaysResponse>? ResourceAllocationDays { get; set; }
        //Use for identifying published/unpublished type
        public string Type { get; set; }
    }
}
