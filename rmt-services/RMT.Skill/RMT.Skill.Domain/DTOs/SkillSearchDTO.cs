using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Domain.DTOs
{
    public class SkillSearchDTO
    {
        public string Name { get; set; }
        public string Email { get;set; }
        public string EmpId { get; set; }
        public string BU { get; set; }
        public string Expertise { get; set; }//Recheck
        public string SMEG { get; set; }//Recheck

        public string? BUId { get; set; }

        public string? Offerings { get; set; }
        public string? Solutions { get; set; }

        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }

        public string Location { get; set; }
        public string Designation { get; set; }
        public string SkillName { get; set; }
        public string SkillCompetency { get; set; }
        public string IndustryExperience { get; set; }
        public string OverallExperience { get; set; }
        public string GTExperience { get; set; }
    }
}
