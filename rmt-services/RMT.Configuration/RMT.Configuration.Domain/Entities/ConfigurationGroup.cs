using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMT.Configuration.Domain.Entities
{
    public class ConfigurationGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        ////Check
        //[Required]
        [NotMapped]
        public string ConfigGroup { get; set; }

        ////Check
        //[Required]
        [NotMapped]
        public string ConfigGroupDisplay { get; set; }

        ////Check
        //[Required]
        [NotMapped]
        public string ConfigKey { get; set; }

        ////Check
        //[Required]
        [NotMapped]
        public string CongigDisplayText { get; set; }

        ////Check
        //[Required]
        [NotMapped]
        public string ValueType { get; set; } // stri , bool ,

        [Required]
        public string ConfigType { get; set; }

        public Int32 SortOrder { get; set; }

        public Int64? ConfigurationGroupMasterId { get; set; }

        //Replacement of //Check
        [ForeignKey("ConfigurationGroupMasterId")]
        public virtual ConfigurationGroupMaster? ConfigurationGroupMaster { get; set; }

        [Required]
        public bool IsAll { get; set; }
        [Required]
        public string AllValue { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime ModifiedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
        public virtual List<ProjectConfiguration>? ProjectConfigurations { get; set; }
    }
}
