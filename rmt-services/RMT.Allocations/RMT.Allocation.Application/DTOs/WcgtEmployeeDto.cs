using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs
{
    public class WcgtEmployeeDto
    {
        public string employee_mid { get; set; }
        public string? company_name { get; set; }
        public string employee_code { get; set; }
        public string first_name { get; set; }
        public string? middle_name { get; set; }
        public string? last_name { get; set; }
        public string name { get; set; }
        public string designation_id { get; set; }
        public string? department { get; set; }
        public string location_id { get; set; }
        public string? service_line_id { get; set; }
        public string email_id { get; set; }
        public DateOnly? joining_date { get; set; }
        //Info :- Reporting Partner is to be mapped to co-super coach
        public string? reporting_partner_mid { get; set; }
        //Info :- Group Head is to be mapped to super coach
        public string? group_head_mid { get; set; }
        //public string? internal_service_unit_id { get; set; }
        public string business_unit_id { get; set; }

        public string? smeg_id { get; set; }//Recheck
        public string? sme_id { get; set; }//Recheck
        public string? expertise_id { get; set; }//Recheck
        public string? CompetencyId { get; set; }
        public string? Competency { get; set; }
        public DateOnly? specical_day { get; set; }
        public DateOnly? birthday { get; set; }
        public bool isactive { get; set; }
        public string supercoach_mid { get; set; }
        public DateOnly? resignation_date { get; set; }
        public DateOnly? proposed_lwd { get; set; }
        public string? employee_status { get; set; }
        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string createdby { get; set; }
        public string modifiedby { get; set; }
    }
}
