using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Domain.Entities
{
    public class SkillMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? id { get; set; }


      
        public Int64? Skill_Id { get; set; }
        [ForeignKey("Skill_Id")]

        public virtual Skills? Skill { get; set; }

        //public string BusinessUnit { get; set; }

        //public string Experties { get; set; }
        public string CompetencyId { get; set; }
        public string Competency { get; set; }

        public List<string> Designation { get; set; }

        public Boolean? IsActive { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifieDate { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }
    }
}
