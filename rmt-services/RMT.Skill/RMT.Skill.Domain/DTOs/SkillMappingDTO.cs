using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Domain.DTOs
{
    public class SkillMappingDTO
    {
        public CompetencyMasterDTO Competency { get; set; }

        public List<string> Designation { get; set; }
    }
}
