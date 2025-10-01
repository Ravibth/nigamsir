using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Domain.DTOs
{
    public class EmployeeIndustryExperience
    {
        public string industry_name { get; set; }
        public string industry_id { get; set; }
        public string sub_industry_name { get; set; }
        public string sub_industry_id { get; set; }
        public string? year_of_experience { get; set; }
        public string? description { get; set; }
    }
}
