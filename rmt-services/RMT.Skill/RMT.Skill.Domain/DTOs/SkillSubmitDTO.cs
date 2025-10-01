using RMT.Skill.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Domain.DTOs
{
    public class SkillSubmitDTO
    {
        public string? SkillCode { get; set; }
        public string SkillName { get; set; }
        public string SkillCategory { get; set; }

        public string Description { get; set; }
        public string Basic { get; set; }

        public string Intermediate { get; set; }
        public string Advanced { get; set; }
        public string Expert { get; set; }
        public string CreatedBy { get; set; }

        public Boolean? IsEnable { get; set; }
        public List<SkillMappingDTO> Mapping { get; set; }

    }
}
