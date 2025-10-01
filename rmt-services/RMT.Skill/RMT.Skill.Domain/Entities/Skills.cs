using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Domain.Entities
{
    public class Skills
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? Skill_Id { get; set; }

        public string SkillCode { get; set; }
        public string SkillName { get; set; }
        public string SkillCategory { get; set; }

        public string Description { get; set; }
        public string Basic { get; set; }

        public string Intermediate { get; set; }
        public string Advanced { get; set; }
        public string Expert { get; set; }

        public virtual List<SkillMapping> Skill_Mapping { get; set; }
        public Boolean? IsActive { get; set; }
        public Boolean? IsEnable { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifieDate { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }
    }
}
