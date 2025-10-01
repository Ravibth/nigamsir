using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Application.DTOs
{
    public class UserSkillsResponseWithSkillDTO: UserSkills
    {
        public Skills? skill { get; set; }
    }
}
