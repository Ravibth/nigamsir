using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Domain.DTOs
{
    public class CompetencyMasterDTO
    {
        public string CompetencyId { get; set; }
        public string Competency { get; set; }
        public bool IsActive { get; set; }
    }
}
