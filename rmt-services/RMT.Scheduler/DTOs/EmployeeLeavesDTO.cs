using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.DTOs
{
    public class EmployeeLeavesDTO
    {
        public string? leave_id { get; set; }
        public string? location_id { get; set; }
        public string? location_name { get; set; }
        public DateOnly? leave_start_date { get; set; }
        public DateOnly? leave_end_date { get; set; }
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
        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }
    }
}
