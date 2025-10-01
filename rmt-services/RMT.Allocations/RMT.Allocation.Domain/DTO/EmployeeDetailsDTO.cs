using RMT.Allocation.Domain.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class EmployeeDetailsDTO
    {
        public string empName { get; set; }
        public string email { get; set; }
        public string designation { get; set; }
        public string grade { get; set; }
        public string location { get; set; }
        public string? business_unit { get; set; }

        public string? supercoach_name { get; set; }
        public string? co_supercoach_name { get; set; }
        public string? supercoach_mid { get; set; }
        public string? co_supercoach_mid { get; set; }
        public string? employee_id { get; set; }
        public string? uemail_id { get; set; }
        public string? competency { get; set; }
        public string? competencyId { get; set; }

        public string Offerings { get; set; }
        public string Solutions { get; set; }
        public string OfferingsId { get; set; }
        public string SolutionsId { get; set; }
        public string? BUId { get; set; }

        public string supercoach { get; set; }
        public string revenue_unit { get; set; }//Recheck
        public string sub_industry { get; set; }
        public string industry { get; set; }
        public string[] offerings { get; set; }
        public string[] solutions { get; set; }
        public SkillsEntities[] skill { get; set; }
        public bool interested { get; set; }
        public string[] pref_location { get; set; }
        public string[] pref_skill { get; set; }
        public string[] pref_industry { get; set; }
        public string[] pref_sub_industry { get; set; }
        public string[] pref_business_unit { get; set; }
        public string[] pref_revenue_unit { get; set; }
        public string[] pref_offerings { get; set; }
        public string[] pref_solutions { get; set; }
        public Int64 leaves { get; set; }
        public DateOnly? last_available_day { get; set; }
        public bool? available { get; set; }
    }
}
