using RMT.Allocation.Domain.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class EmployeeMasterDTO
    {
        public string empName { get; set; }
        public string email { get; set; }
        public string designation { get; set; }
        public string location { get; set; }
        public string supercoach { get; set; }
        public string revenue_unit { get; set; }//Recheck
        public string? business_unit { get; set; }
        public string? supercoach_name { get; set; }
        public string? co_supercoach_name { get; set; }
        public string? supercoach_mid { get; set; }
        public string? co_supercoach_mid { get; set; }
        public string? employee_id { get; set; }
        public string? uemail_id { get; set; }
        public string? competency { get; set; }
        public string? competencyId { get; set; }
        public string? grade { get; set; }

        public string? sub_industry { get; set; }
        public string industry { get; set; }
        public SkillsEntities[] skill { get; set; }
    }
}
