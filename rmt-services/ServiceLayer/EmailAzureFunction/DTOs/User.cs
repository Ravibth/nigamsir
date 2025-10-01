using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class User
    {
        public int? id { get; set; }

        public string? role_ids { get; set; }

        public string? email_id { get; set; }

        public string? name { get; set; }

        public string? uemail_id { get; set; }

        public string? entity { get; set; }

        public string? employee_id { get; set; }

        public string? emp_code { get; set; }

        public string? fname { get; set; }

        public string? lname { get; set; }

        public string? designation { get; set; }

        public string? grade { get; set; }

        public string? location { get; set; }

        public string? region_name { get; set; }

        public string? smeg { get; set; }

        public string? expertise { get; set; }

        public string? business_unit { get; set; }

        public string? co_supercoach_name { get; set; }

        public string? supercoach_name { get; set; }

        public string? service_line { get; set; }

        public string? roles { get; set; }

        public bool? status { get; set; }

        // New fields
        public string? reporting_partner_mid { get; set; }

        public string? employee_status { get; set; }

        public DateTime? employee_resignation_date { get; set; }

        public DateTime? employee_last_working_date { get; set; }

        public string? supercoach_mid { get; set; }

        public string? co_supercoach_mid { get; set; }

        public bool? is_active { get; set; }

        public string? created_by { get; set; }

        public DateTime? created_at { get; set; }

        public string? updated_by { get; set; }

        public DateTime? updated_at { get; set; }

        public int? order { get; set; }

        public string? sub_industry { get; set; }

        public string? industry { get; set; }

        public string? competency { get; set; }

        public string? competencyId { get; set; }
    }
}
