using System;
using System.Collections.Generic;

namespace Gateway.API.Dtos
{

    public class SkillsEntities
    {
        public string SkillName { get; set; }
        public string SkillCode { get; set; }
        public string? Type { get; set; }
    }

    public static class EAllocationType
    {
        public const string SYSTEM_SUGGESTED_ALLOCATION = "SYSTEM_SUGGESTED_ALLOCATION";
        public const string NAME_ALLOCATION = "NAME_ALLOCATION";
        public const string SAME_TEAM_ALLOCATION = "SAME_TEAM_ALLOCATION";
        public const string BULK_ALLOCATION = "BULK_ALLOCATION";
        public const string UPDATE_ALLOCATION = "UPDATE_ALLOCATION";
    }
    public class CommonAllocationRequestDTO
    {
        public string Email { get; set; }
        //public UserDetailsCommonDTO UserInfo { get; set; }
        //[EnumDataType(typeof(EAllocationType))]
        public string type { get; set; }
        public SkillsEntities[] Skills { get; set; }
        public bool Available { get; set; }
        //public object Meta { get; set; }
        //public bool Interested { get; set; }
        //public List<FormValuesForAllocationDTO> Allocations { get; set; }
        //public Requisition? Requisition { get; set; }
        public Guid? RequisitionId { get; set; }
        public string? Description { get; set; }
        //public bool ShowDescription { get; set; }
        public bool IsContinuousAllocation { get; set; }
        public int TotalEfforts { get; set; }
        public bool isPreviouslyDraft { get; set; }
        //public ProjectDTO ProjectInfo { get; set; }
    }
}
