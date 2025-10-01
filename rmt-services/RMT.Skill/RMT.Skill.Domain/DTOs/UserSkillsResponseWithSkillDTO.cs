using RMT.Skill.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Domain.DTOs
{
    public class UserSkillsResponseWithSkillDTO : UserSkills
    {
        public Skills? skill { get; set; }
    }
}
