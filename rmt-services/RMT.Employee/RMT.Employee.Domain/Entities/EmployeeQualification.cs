using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Domain.Entities
{
    public class EmployeeQualification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }
        public string qualification_type { get; set; }
        public string qualification { get; set; }
        public string institute_location_name { get; set; }
        public string month_year_of_passing { get; set; }
        public string area_of_specialisation { get; set; }
        public string employee_mid { get; set; }
        public bool is_published { get; set; }
        public Int64 employee_profile_id { get; set; }
        [ForeignKey("employee_profile_id")]
        public virtual EmployeeProfile? employee_profile { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? modified_at { get; set; }
        public string? modified_by { get; set; }
        public string created_by { get; set; }
    }
}
