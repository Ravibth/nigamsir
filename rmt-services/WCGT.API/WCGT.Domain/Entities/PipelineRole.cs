using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Domain.Entities
{
    //public enum PipelineUserRoles
    //{
    //    enquiry_owner = 0,
    //    finding_partner2 = 1,
    //    finding_partner3 = 2,
    //}

    public class PipelineRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? id { get; set; }

        public string? pipeline_id { get; set; }

        [ForeignKey("pipeline_id")]
        public virtual Pipeline? pipeline { get; set; }

        public string? user_mid { get; set; }
        public string? user_empname { get; set; }
        public string? user_emailid { get; set; }

        //[EnumDataType(typeof(PipelineUserRoles))]
        public string? user_role { get; set; }

        public Boolean? isactive { get; set; }

        public DateTime? createdat { get; set; }

        public DateTime? modifiedat { get; set; }

        public string? createdby { get; set; }

        public string? modifiedby { get; set; }
    }
}
