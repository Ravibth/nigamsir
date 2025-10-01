using RMT.Allocation.Domain.DTO;

using RMT.Allocation.Domain.Entities;

namespace RMT.Allocation.Application.DTOs.ResourceAllocationDTOs
{

    //public class AllocationObj
    //{
    //    public DateTime? startDate { get; set; }
    //    public DateTime? endDate { get; set; }
    //    public int effort { get; set; }
    //}
    public class CreateResourceAllocationDTO
    {
        //public Int64 Id { get; set; }
        public string PipelineCode { get; set; }
        public string JobCode { get; set; }
        //public string ProjectCode { get; set; }
        public string JobName { get; set; }
        public string EmpEmail { get; set; }
        public string EmpName { get; set; }
        public string? PipelineName { get; set; }
        public DateTime? ConfirmedAllocationStartDate { get; set; }
        public DateTime? ConfirmedAllocationEndDate { get; set; }
        public Int64? ConfirmedPerDayHours { get; set; }

        public Int64 RequisitionId { get; set; }

        public string? AllocationStatus { get; set; }
        public string RecordType { get; set; }

        //public DateTime? CreatedDate { get; set; }
        //public DateTime? ModifiedDate { get; set; }
        //public string? CreatedBy { get; set; }
        //public string? ModifiedBy { get; set; }
        public bool? IsActive { get; set; } = true;
        public AllocationObj[] allocation { get; set; }
        public int TotalEffort { get; set; }
        public bool IsContinuousAllocation { get; set; }
    }
}
