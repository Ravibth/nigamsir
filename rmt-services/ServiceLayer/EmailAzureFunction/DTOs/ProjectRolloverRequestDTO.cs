using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class ProjectRolloverRequestDTO
    {
        public string? Message { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public DateTime PipelineStartDate { get; set; }
        public DateTime PipelineOldStartDate { get; set; }
        public List<AllocationRolloverUserResponseDTO>? ProjectAllocations { get; set; }
    }

    public class AllocationRolloverUserResponseDTO
    {
        public Guid Id { get; set; }
        public Guid RequisitionId { get; set; }

        public Guid Guid { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public RollOverAllocationStatuses RollOverAllocaionStatus { get; set; }

        public DateOnly OldAllocationStartDate { get; set; }

        public DateOnly OldAllocationEndDate { get; set; }

        public DateOnly NewAllocationStartDate { get; set; }

        public DateOnly NewAllocationEndDate { get; set; }

    }

    public enum RollOverAllocationStatuses
    {
        None,
        OldCompletedNewStarted,
        OldInProgressNewStarted,
        OldInProgressNewReleased,
        OldCompletedNewDraft,
        OldDraftNoAction
    }

}
