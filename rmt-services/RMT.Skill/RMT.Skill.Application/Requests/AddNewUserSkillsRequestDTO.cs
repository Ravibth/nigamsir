using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.Requests
{
    public class AddUpdateUserSkillsRequestDTO
    {
        public string SkillName { get; set; }
        public string SkillCode { get; set; }
        public string Proficiency { get; set; }
    }
}
