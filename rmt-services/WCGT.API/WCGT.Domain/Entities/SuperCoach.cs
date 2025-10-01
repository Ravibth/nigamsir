using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Domain.Entities
{
    public  class SuperCoach  
    {
        public string employee_mid { get; set; }
        public string? company_name { get; set; }
        public string? employee_code { get; set; }        
        public string? name { get; set; }
        public string? designation_id { get; set; }
        public string? email_id { get; set; }
        public Boolean? isactive { get; set; }
        public string? supercoach_mid { get; set; }
        public string? reporting_partner_mid { get; set; }
    }
}
