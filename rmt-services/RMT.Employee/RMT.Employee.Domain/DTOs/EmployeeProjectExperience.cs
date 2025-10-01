using System.ComponentModel.DataAnnotations.Schema;

namespace RMT.Employee.Domain.DTOs
{
    [NotMapped]
    public class EmployeeProjectExperience
    {
        public int Id { get; set; }
        public string job_name { get; set; } = string.Empty;
        public string client_group { get; set; } = string.Empty;
        public string client_name { get; set; } = string.Empty;
        public string business_unit { get; set; } = string.Empty;
        public string Offering { get; set; } = string.Empty;
        public string Solution { get; set; } = string.Empty;
        public string industry { get; set; } = string.Empty;
        public string sub_industry { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string job_start_date { get; set; } = string.Empty;
        public string job_end_date { get; set; } = string.Empty;
        public string primary_el { get; set; } = string.Empty;
        public string csp { get; set; } = string.Empty;
        public string project_type { get; set; } = string.Empty;
        public string project_description { get; set; } = string.Empty;
        public string task_description { get; set; } = string.Empty;
        public string skills_utilized { get; set; } = string.Empty;
        public double actual_hours { get; set; }
    }
}
