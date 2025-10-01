using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.DTO
{
    public class SkillsEntities
    {
        public string SkillName { get; set; }
        public string SkillCode { get; set; }
        public string? Type { get; set; }
    }

    public class ScoreBreakupDTO
    {
        public Int64 value { get; set; }
        public string parameter { get; set; }
        public string matched_type { get; set; }
        public string matching_value { get; set; }
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
        public ScoreBreakupDTO[]? score_breakup { get; set; }
        public string? revenue_unit { get; set; }
        public string? business_unit { get; set; }
        public string? sub_industry { get; set; }
        public string? industry { get; set; }
        public SkillsEntities[]? skill { get; set; }
        public bool? interested { get; set; }
        public bool? available { get; set; }
    }
}
