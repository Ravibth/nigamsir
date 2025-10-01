using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.HttpServices.DTO
{

    public static class AllocationType
    {
        public const string PUBLISHED = "Published";
        public const string UnPUBLISHED = "UnPublished";
    }

    public class ResourceAllocationResponse
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
      //  public List<ResourceAllocationDaysResponse>? ResourceAllocationDays { get; set; }
        //Use for identifying published/unpublished type
        public string Type { get; set; }
       // public Requisition? Requisition { get; set; }
    }
    public class ResourceAllocationDetailsResponse
    {
        public Guid Id { get; set; }
        public Guid guid { get; set; }
        public Guid RequisitionId { get; set; }
        public string EmpEmail { get; set; }
        public string PipelineCode { get; set; }
        public string PipelineName { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public string EmpName { get; set; }
        public string Description { get; set; }
        public Int64 TotalEffort { get; set; }
        public string AllocationStatus { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public DateTime? ConfirmedAllocationDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Int64? AllocationVersion { get; set; }
      //  public Requisition? Requisition { get; set; }
       // public List<ResourceAllocationSkillsResponse>? Skills { get; set; }
        public List<ResourceAllocationResponse>? ResourceAllocations { get; set; }
        public string Type { get; set; }

    }

}
