using RMT.Allocation.Domain;
using RMT.Allocation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Infrastructure.DTOs
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
        public Requisition? Requisition { get; set; }
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

        public Requisition? Requisition { get; set; }
    }
    public class ResourceAllocationResponse
    {
        public Guid Id { get; set; }
        public string EmpEmail { get; set; }
        public string PipelineCode { get; set; }
        public string PipelineName { get; set; }
        public string? JobCode { get; set; }
        public string? JobId { get; set; }
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
        public Requisition? Requisition { get; set; }
        public List<PublishedResAllocSkillEntity>? PublishedResAllocSkillEntity { get; set; }
    }
    public class ResourceAllocationDetailsResponse
    {
        public Guid Id { get; set; }
        public Guid guid { get; set; }
        public Guid? ParentPublishedResAllocDetailsId { get; set; }
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
        public string? Designation { get; set; }
        //public string? Competency { get; set; }
        public string? Grade { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public DateTime? ConfirmedAllocationDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Int64? AllocationVersion { get; set; }
        public Requisition? Requisition { get; set; }
        public List<ResourceAllocationSkillsResponse>? Skills { get; set; }
        public List<ResourceAllocationResponse>? ResourceAllocations { get; set; }
        public string Type { get; set; }
        public bool? IsUpdated { get; set; }

    }

    public class GetResourceAllocationDetailsListByCurrentUserRoleResponse : ResourceAllocationDetailsResponse
    {
        public bool? hasPermissionToReleaseAllocation { get; set; }
        public bool? hasPermisssionToUpdateAllocation { get; set; }
        public string? Designation { get; set; }
        public string? Grade { get; set; }

        public static GetResourceAllocationDetailsListByCurrentUserRoleResponse MapToGetResourceAllocationDetailsList(ResourceAllocationDetailsResponse resourceAllocation)
        {
            return new GetResourceAllocationDetailsListByCurrentUserRoleResponse
            {
                Id = resourceAllocation.Id,
                RequisitionId = resourceAllocation.RequisitionId,
                EmpEmail = resourceAllocation.EmpEmail,
                PipelineCode = resourceAllocation.PipelineCode,
                PipelineName = resourceAllocation.PipelineName,
                JobCode = resourceAllocation.JobCode,
                JobName = resourceAllocation.JobName,
                EmpName = resourceAllocation.EmpName,
                Description = resourceAllocation.Description,
                TotalEffort = resourceAllocation.TotalEffort,
                AllocationStatus = resourceAllocation.AllocationStatus,
                StartDate = resourceAllocation.StartDate,
                EndDate = resourceAllocation.EndDate,
                ConfirmedAllocationDate = resourceAllocation.ConfirmedAllocationDate,
                IsActive = resourceAllocation.IsActive,
                CreatedBy = resourceAllocation.CreatedBy,
                ModifiedBy = resourceAllocation.ModifiedBy,
                CreatedAt = resourceAllocation.CreatedAt,
                ModifiedAt = resourceAllocation.ModifiedAt,
                AllocationVersion = resourceAllocation.AllocationVersion,
                Requisition = resourceAllocation.Requisition,
                Skills = resourceAllocation.Skills,
                Designation = resourceAllocation.Requisition.Designation,
                Grade = resourceAllocation.Requisition.Grade,
                ResourceAllocations = resourceAllocation.ResourceAllocations,
                Type = resourceAllocation.Type,
                IsUpdated = resourceAllocation.IsUpdated,
                // Additional properties specific to GetResourceAllocationDetailsListByCurrentUserRoleResponse
                hasPermissionToReleaseAllocation = null, // Set appropriate value
                hasPermisssionToUpdateAllocation = null // Set appropriate value
            };
        }

    }

    // Mapping function to convert ResourceAllocationDetailsResponse to GetResourceAllocationDetailsListByCurrentUserRoleResponse

}
