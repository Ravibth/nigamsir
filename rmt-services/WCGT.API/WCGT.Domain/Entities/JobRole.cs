using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Domain.Entities
{
    //public enum JobUserRoles
    //{
    //    engagement_leader_empid = 0,
    //    signing_partner_empid = 1,
    //    lead_generator_empid = 2,
    //    second_engagement_leader_empid = 3,
    //    engagement_quality_reviewer_empid = 4,
    //    third_engagement_leader = 5,
    //}

    public class JobRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? id { get; set; }

        public string? job_id { get; set; }

        [ForeignKey("job_id")]
        public virtual Job? job { get; set; }

        public string? user_mid { get; set; }
        public string? user_empname { get; set; }
        public string? user_emailid { get; set; }

        //[EnumDataType(typeof(JobUserRoles))]
        public string? user_role { get; set; }

        public Boolean? isactive { get; set; }

        public DateTime? createdat { get; set; }

        public DateTime? modifiedat { get; set; }

        public string? createdby { get; set; }

        public string? modifiedby { get; set; }
    }
}
