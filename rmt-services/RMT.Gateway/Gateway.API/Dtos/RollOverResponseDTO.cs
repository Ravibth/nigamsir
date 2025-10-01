using System;
using System.Collections.Generic;

namespace Gateway.API.Dtos
{
    public class ResourceAvailable
    {
        public long RequisitionId { get; set; }
        public string EmailId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Int64? TotalAvaibleHours { get; set; }
        public Int64 RequireWorkingHours { get; set; }
        public Boolean IsHoursAvialable { get; set; }
        public Boolean isPerDayHourAllocation { get; set; }
        public string? ErrorMsg { get; set; }
        public int? TotalWorkingHours { get; set; }
        public int? TotalWorkingDays { get; set; }

    }
    //public class ResourceAllocationDetailsResponse
    //{
    //    public Int64 Id { get; set; }
    //    public Guid Guid { get; set; }
    //    public string PipelineCode { get; set; }
    //    public string? JobCode { get; set; }
    //    public string ProjectCode { get; set; }
    //    public string? JobName { get; set; }
    //    public string? PipelineName { get; set; }//1.
    //    public string? EmpEmail { get; set; }
    //    public string? EmpName { get; set; }
    //    public Int64 RequisitionId { get; set; }
    //    public string? ClientName { get; set; }

    //    public DateTime? ConfirmedAllocationStartDate { get; set; }
    //    public DateTime? ConfirmedAllocationEndDate { get; set; }
    //    public int ConfirmedPerDayHours { get; set; }
    //    public Boolean isPerDayHourAllocation { get; set; }
    //    public Int64? ResAllocDetailsId { get; set; }
    //    //public virtual ResourceAllocationDetails? ResourceAllocationDetails { get; set; }
    //    //public virtual Requisition? Requisitions { get; set; }

    //    public string RecordType { get; set; }
    //    public int TotalWorkingDays { get; set; }
    //    public Boolean IsContinuousAllocation { get; set; }//2.
    //    public string Description { get; set; }//3.
    //    public int TotalEffort { get; set; }//4
    //    public string AllocationStatus { get; set; }
    //    public DateTime CreatedDate { get; set; }
    //    public DateTime ModifiedDate { get; set; }
    //    public string? CreatedBy { get; set; }
    //    public string? ModifiedBy { get; set; }
    //    public bool IsActive { get; set; }
    //    //public virtual List<ResourceAllocationSkills>? Skills { get; set; }

    //    //public List<ResourceAllocation> resourceAllocations { get; set; }
    //    //public List<ResourceAllocation> ResourceAllocation { get; set; }
    //}
    public class RollOverResponseDTO
    {
        public List<ResourceAvailable> InvalidAllocations { get; set; }

        public List<ResourceAllocationDetailsResponse> UpdatedAllocations { get; set; }
    }
}
