using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Domain.DTOs
{
    public class MandetorySkillRequestDTO
    {
        public string? Competency { get; set; }
        public string? CompetencyId { get; set; }
        public string Designation { get; set; }
    }
}
