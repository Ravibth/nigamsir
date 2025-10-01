using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Domain.Entities
{
    public class ConfigurationGroupMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [Required]
        public string ConfigGroup { get; set; }

        [Required]
        public string ConfigGroupDisplay { get; set; }

        [Required]
        public string ConfigKey { get; set; }

        [Required]
        public string CongigDisplayText { get; set; }

        [Required]
        public string ValueType { get; set; } // stri , bool ,

        //[Required]
        //public string ConfigType { get; set; }

        [Required]
        public string DefaultValue { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsActive { get; set; }

        public virtual ICollection<ConfigurationGroup> ConfigurationGroups { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime ModifiedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string ModifiedBy { get; set; }

    }
}
