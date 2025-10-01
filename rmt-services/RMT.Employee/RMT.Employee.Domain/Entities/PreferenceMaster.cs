using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMT.Employee.Domain.Entities
{
    public class PreferenceMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 PreferenceMasterId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public int SortOrder { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime ModifiedAt { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string ModifiedBy { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
