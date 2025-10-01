using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMT.Employee.Domain.Entities
{
    public class ExperienceOutsideGT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }        
        public string? project_name { get; set; }
        public string? client_name { get; set; }
        public string? industry { get; set; }
        public string? sub_industry { get; set; }
        public string? project_location { get; set; }
        public string? project_description { get; set; }
        public string? tasks_performed { get; set; }
        public string? job_start_date { get; set; }
        public string? job_end_date { get; set; }      
        
        public Int64 employee_profile_id { get; set; }
        [ForeignKey("employee_profile_id")]
        public virtual EmployeeProfile? employee_profile { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? modified_at { get; set; }
        public string? modified_by { get; set; }
        public string? created_by { get; set; }
    }
}
