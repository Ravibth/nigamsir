using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class SkillCodeNameDTO
    {
        public string SkillCode { get; set; }
        public string SkillName { get; set; }
        public List<string>? CompetencyId { get; set; }
        public List<string>? Competency { get; set; }
    }
}
