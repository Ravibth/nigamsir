using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Infrastructure.DTOs;

namespace RMT.Allocation.Application.DTOs
{
    public class EmployeeProject
    {
        public Guid Id { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
        //public string? ProjectCode { get; set; }
        public string? JobName { get; set; }
        public string? PipelineName { get; set; }
        public string? EmpEmail { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? EmpName { get; set; }
        public int? ConfirmedPerDayHours { get; set; }
        public string? timeline_type { get; set; }
        public string? timeline_display_text { get; set; }
        public string? Designation { get; set; }
        public string? AllocationType { get; set; }
        public string? AllocationStatus { get; set; }
        public string? RecordType { get; set; }
        public Guid RequisitionId { get; set; }
        public Guid guid { get; set; }
        public bool? isUpdated { get; set; }
        public List<ResourceAllocationSkillsResponse>? Skills { get; set; }
        //public List<ResourceAllocationSkillEntity>? Skills { get; set; }

        public UsersTimelineWeeklyAllocation WeeklyBreakup { get; set; } = new UsersTimelineWeeklyAllocation();
        public int? WeeklyTotal { get; set; }

    }
}
