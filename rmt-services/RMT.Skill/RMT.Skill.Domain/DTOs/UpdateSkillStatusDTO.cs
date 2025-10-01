using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Domain.DTOs
{
    public class UpdateSkillStatusDTO
    {
        public string SkillCode{ get; set; }
        //public string SkillName { get; set; }
        public Boolean IsEnable {  get; set; }
        public string ModifyBy { get; set; }
    }
}
