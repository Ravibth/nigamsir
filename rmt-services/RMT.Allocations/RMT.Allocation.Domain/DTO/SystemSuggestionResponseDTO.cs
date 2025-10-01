using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class AllocationObj
    {
        public DateOnly startDate { get; set; }
        public DateOnly endDate { get; set; }
        public Int64 effort { get; set; }
        public Boolean isPerDayHourAllocation { get; set; }
    }
    public class SystemSuggestionResponseDTO
    {
        public string? empName { get; set; }
        public string? email { get; set; }
        public string? designation { get; set; }
        public string? grade { get; set; }
        public string? competency { get; set; }
        public string? competencyId { get; set; }
        public string? location { get; set; }
        public string? supercoach { get; set; }
        public string? score { get; set; }
        public object? score_breakup { get; set; }
        //public ResourceAllocation[]? allocations { get; set; }
        public string? revenue_unit { get; set; }
        public string? business_unit { get; set; }
        public string? sub_industry { get; set; }
        public string? industry { get; set; }
        public SkillsEntities[]? skill { get; set; }
        public bool? interested { get; set; }
        public bool? available { get; set; }
    }
}
