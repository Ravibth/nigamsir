using RMT.Employee.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Application.DTOs
{
    public class EmployeeQualificationUpdate
    {
        public Int64 id { get; set; }
        public bool is_published { get; set; }
    }
    public class UpdateEmployeeProfileRequest
    {
        public string employee_email { get; set; }
        public string? about_me { get; set; }
        public string? linkedin_url { get; set; }
        public double? year_of_experience { get; set; }
        public string? present_work_location { get; set; }
        public List<EmployeeQualificationUpdate>? qualification_update { get; set; }
        public virtual List<ExperienceOutsideGT>? experience_outside_gt { get; set; }
    }
}
