using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{

    public class UserMasterList
    {
        public int? id { get; set; }
        public string? email_id { get; set; }
        public string? entity { get; set; }
        public string? name { get; set; }
        public string? emp_code { get; set; }
        public string? fname { get; set; }
        public string? lname { get; set; }
        public string? designation { get; set; }
        public string? location { get; set; }
        public string? grade { get; set; }
        public string? business_unit { get; set; }
        public string? co_supercoach_name { get; set; }
        public string? supercoach_name { get; set; }
        public string? supercoach_mid { get; set; }
        public string? co_supercoach_mid { get; set; }
        public string? employee_id { get; set; }
        public string? uemail_id { get; set; }
        public string? competency { get; set; }
        public string? competencyId { get; set; }
        public string? service_line { get; set; }
        public string? roles { get; set; }
        public string? sub_industry { get; set; }
        public string? industry { get; set; }
        public Boolean status { get; set; }
        public Boolean is_active { get; set; }
        public string? created_by { get; set; }
        public string? updated_by { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? created_at { get; set; }
    }
}
