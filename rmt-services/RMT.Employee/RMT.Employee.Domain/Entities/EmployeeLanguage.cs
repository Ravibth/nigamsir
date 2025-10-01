using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RMT.Employee.Domain.Entities
{
    public class EmployeeLanguage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        public string employee_mid { get; set; }
        public string language_name { get; set; }
        public string? read { get; set; }
        public string? write { get; set; }
        public string? speak { get; set; }
        public Int64 employee_profile_id { get; set; }
        [ForeignKey("employee_profile_id")]
        public virtual EmployeeProfile? employee_profile { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? modified_at { get; set; }
        public string? modified_by { get; set; }
        [Required]
        public string created_by { get; set; }
    }
}
