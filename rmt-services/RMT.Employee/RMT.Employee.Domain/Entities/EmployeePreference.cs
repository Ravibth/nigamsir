using Newtonsoft.Json;
using RMT.Employee.Domain.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMT.Employee.Domain.Entities
{
    public class EmployeePreference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        [Required]
        public string EmployeeEmail { get; set; }
        [Required]
        public string Category { get; set; }
        [Column(TypeName = "jsonb")]
        public string PreferenceInfo { get; set; } = "{}";
        [Required]
        public int PreferenceOrder { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [NotMapped]
        public PreferenceDetails PreferenceDetails 
        { 
            get => string.IsNullOrEmpty(PreferenceInfo) ? new PreferenceDetails() : JsonConvert.DeserializeObject<PreferenceDetails>(PreferenceInfo) ; 
            set => PreferenceInfo = JsonConvert.SerializeObject(value); 
        }
    }
}
