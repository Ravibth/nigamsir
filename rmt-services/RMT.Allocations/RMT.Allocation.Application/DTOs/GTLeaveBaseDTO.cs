using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class GTLeaveBaseDTO
    {
        public string? leave_id { get; set; }
        public string? location_id { get; set; }
        public string? location_name { get; set; }
        public DateTime leave_start_date { get; set; }
        public DateTime leave_end_date { get; set; }
        public double? applied_days { get; set; }
        public double? revoked_days { get; set; }
        public DateOnly? revoked_from_date { get; set; }
        public DateOnly? revoked_to_date { get; set; }
        public string? comp_name { get; set; }
        public string? leave_type_name { get; set; }
        public string? emp_mid { get; set; }
        public string? emp_name { get; set; }
        ///Info:- email is to be mapped to column CorrsMail in WCGT
        public string? emp_email { get; set; }
        public string? leave_status_name { get; set; }


        public bool? isactive { get; set; }
    }
}
