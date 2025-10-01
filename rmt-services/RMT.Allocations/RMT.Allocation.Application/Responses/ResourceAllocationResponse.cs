//using RMT.Allocation.Domain.Entities;

//namespace RMT.Allocation.Application.Responses
//{
//    public class ResourceAllocationResponse
//    {
//        public Int64 Id { get; set; }
//        public Guid Guid { get; set; }
//        public string PipelineCode { get; set; }
//        public string PipelineName { get; set; }
//        public string JobCode { get; set; }
//        //public string ProjectCode { get; set; }
//        public string JobName { get; set; }
//        public string EmpEmail { get; set; }
//        public string EmpName { get; set; }
//        public string? ClientName { get; set; }
//        public DateTime? ConfirmedAllocationStartDate { get; set; }
//        public DateTime? ConfirmedAllocationEndDate { get; set; }
//        public int ConfirmedPerDayHours { get; set; }
//        public Boolean isPerDayHourAllocation { get; set; }
//        public Int64? ResAllocDetailsId { get; set; }
//        public virtual ResourceAllocationDetails? ResourceAllocationDetails { get; set; }
//        public Int64 RequisitionId { get; set; }
//        public virtual Requisition? Requisitions { get; set; }
//        public string AllocationStatus { get; set; }
//        public string RecordType { get; set; }
//        public int TotalWorkingDays { get; set; }
//        public virtual List<ResourceAllocationSkillEntity>? Skills { get; set; }
//        public DateTime CreatedDate { get; set; }
//        public DateTime ModifiedDate { get; set; }
//        public string CreatedBy { get; set; }
//        public string ModifiedBy { get; set; }
//        public bool IsActive { get; set; }
//        public List<ResourceAllocation> ResourceAllocation { get; set; }

//    }
//}
