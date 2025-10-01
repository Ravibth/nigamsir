using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.AllocationService.DTOs
{
    public class ResourceAllocationDetailsResponse
    {
        public Int64 Id { get; set; }
        public Guid Guid { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string ProjectCode { get; set; }
        public string? JobName { get; set; }
        public string? PipelineName { get; set; }
        public string? EmpEmail { get; set; }
        public string? EmpName { get; set; }
        public Int64 RequisitionId { get; set; }
        public string? ClientName { get; set; }
        public DateTime? ConfirmedAllocationStartDate { get; set; }
        public DateTime? ConfirmedAllocationEndDate { get; set; }
        public int ConfirmedPerDayHours { get; set; }
        public Boolean isPerDayHourAllocation { get; set; }
        public Int64? ResAllocDetailsId { get; set; }
        public string RecordType { get; set; }
        public int TotalWorkingDays { get; set; }
        public Boolean IsContinuousAllocation { get; set; }
        public string Description { get; set; }
        public int TotalEffort { get; set; }
        public string AllocationStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime? AllocationStartDate { get; set; }
        public DateTime? AllocationEndDate { get; set; }

    }
}
