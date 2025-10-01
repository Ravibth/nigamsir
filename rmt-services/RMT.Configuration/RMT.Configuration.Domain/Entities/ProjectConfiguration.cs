using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMT.Configuration.Domain.Entities
{
    public class ProjectConfiguration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        [Required]
        public Int64 ConfigId { get; set; }
        [Required]
        public string AttributeName { get; set; }
        [Required]
        public string AttributeValue { get; set; } //
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
        [Required]
        public DateTime ModifiedAt { get; set; }

        [ForeignKey("ConfigId")]
        public virtual ConfigurationGroup ConfigurationGroup { get; set; }
    }
}
