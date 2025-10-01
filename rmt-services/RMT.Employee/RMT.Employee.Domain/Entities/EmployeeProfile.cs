using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMT.Employee.Domain.DTOs;

namespace RMT.Employee.Domain.Entities
{
    public class EmployeeProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 id { get; set; }
        public string employee_name { get; set; }
        public string employee_email { get; set; }
        public string employee_mid { get; set; }
        public string employee_code { get; set; }
        public string? designation { get; set; }
        public string business_unit { get; set; }
        public string competency { get; set; }
        public string? supercoach_email { get; set; }
        public string? supercoach_mid { get; set; }
        public string? supercoach_name { get; set; }
        public string? co_supercoach_email { get; set; }
        public string? co_supercoach_mid { get; set; }
        public string? co_supercoach_name { get; set; }
        public string? location { get; set; }
        public string? present_work_location { get; set; }
        public string? linkedin_url { get; set; }
        public string? employee_type { get; set; }
        public double? year_of_experience { get; set; }
        public string? about_me { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? modified_at { get; set; }
        public string? modified_by { get; set; }
        public string created_by { get; set; }
        public virtual ICollection<EmployeeQualification>? employee_qualification { get; set; }
        public virtual ICollection<EmployeeLanguage>? employee_language { get; set; }
        public virtual ICollection<EmployeeWorkExprerience>? employee_work_experience { get; set; }
        public virtual ICollection<EmployeeProjectExperience>? employee_project_experience { get; set; }
        public virtual ICollection<ExperienceOutsideGT>? experience_outside_gt  { get; set; }
    }
}
