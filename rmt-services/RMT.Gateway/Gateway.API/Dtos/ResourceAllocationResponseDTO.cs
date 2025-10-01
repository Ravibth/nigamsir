using Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper.DTOs;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Gateway.API.Dtos
{
    public static class AllocationType
    {
        public const string PUBLISHED = "Published";
        public const string UnPUBLISHED = "UnPublished";
    }

    public class ResourceAllocationSkillsResponse
    {
        public Guid Id { get; set; }
        public Guid RequisitionId { get; set; }
        public Guid? UnPublishedResAllocDetailsId { get; set; }
        public Guid? PublishedResAllocDetailsId { get; set; }
        public string SkillName { get; set; }
        public string SkillCode { get; set; }
        public RequisitionDTO? Requisition { get; set; }
        public ResourceAllocationDetailsResponse? ResAllocDetails { get; set; }
        public string Type { get; set; }

    }
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

        public RequisitionDTO? Requisition { get; set; }
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
        public List<ResourceAllocationDaysResponse>? ResourceAllocationDays { get; set; }
        public string Type { get; set; }
        public RequisitionDTO? Requisition { get; set; }
    }
    public class ResourceAllocationDetailsResponse
    {
        public Guid Id { get; set; }
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
        public RequisitionDTO? Requisition { get; set; }
        public List<ResourceAllocationSkillsResponse>? Skills { get; set; }
        public List<ResourceAllocationResponse>? ResourceAllocations { get; set; }
        public string Type { get; set; }

    }


    public class ResourceAllocationDetailsResponseForWorkflowMeta
    {
        public Guid Id { get; set; }
        //public Guid ItemId { get; set; }
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
        public string Grade { get; set; }
        public string Designation { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public DateTime? ConfirmedAllocationDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Int64? AllocationVersion { get; set; }
        public RequisitionDTO? Requisition { get; set; }
        public string Type { get; set; }
    }

    //public class ResourceAllocationResponseDTO
    //{
    //    public Int64 Id { get; set; }
    //    public Guid Guid { get; set; }
    //    public string PipelineName { get; set; }
    //    public string PipelineCode { get; set; }
    //    public string JobCode { get; set; }
    //    public string ProjectCode { get; set; }
    //    public string JobName { get; set; }
    //    public string EmpEmail { get; set; }
    //    public string EmpName { get; set; }
    //    public string? ClientName { get; set; }
    //    public DateTime? ConfirmedAllocationStartDate { get; set; }
    //    public DateTime? ConfirmedAllocationEndDate { get; set; }
    //    public int? ConfirmedPerDayHours { get; set; }
    //    public Boolean isPerDayHourAllocation { get; set; }
    //    public Int64? ResAllocDetailsId { get; set; }
    //    //public virtual ResourceAllocationDetails? ResourceAllocationDetails { get; set; }
    //    public Int64 RequisitionId { get; set; }
    //    //public virtual Requisition? Requisitions { get; set; }
    //    public string AllocationStatus { get; set; }
    //    public string RecordType { get; set; }
    //    public int? TotalWorkingDays { get; set; }
    //    //public virtual List<ResourceAllocationSkills>? Skills { get; set; }
    //    public DateTime? CreatedDate { get; set; }
    //    public DateTime? ModifiedDate { get; set; }
    //    public string? CreatedBy { get; set; }
    //    public string? ModifiedBy { get; set; }
    //    public bool? IsActive { get; set; }
    //    //public List<ResourceAllocation> ResourceAllocation { get; set; }
    //    //public ResourceAllocationDetails? ResourceAllocationDetails { get; set; }
    //    public RequisitionDTO? Requisitions { get; set; }
    //    //public virtual List<ResourceAllocationSkills>? Skills { get; set; }
    //    //public List<ResourceAllocation> ResourceAllocation { get; set; }


    //}
}
