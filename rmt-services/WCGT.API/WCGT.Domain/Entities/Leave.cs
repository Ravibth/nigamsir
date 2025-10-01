using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Domain.Entities
{
    public class Leave
    {
        [Key]
        public string? unique_leave_id { get; set; }
        public string? leave_id { get; set; }
        public string? location_id { get; set; }

        [ForeignKey("location_id")]
        public virtual Location? location { get; set; }
        public string? location_name { get; set; }
        public DateOnly? leave_start_date { get; set; }
        public DateOnly? leave_end_date { get; set; }
        public string? start_date_half { get; set; }
        public string? end_date_half { get; set; }
        //public string? start_date_half { get; set; }
        //public string? end_date_half { get; set; }
        public double? applied_days { get; set; }
        public double? revoked_days { get; set; }
        public DateOnly? revoked_from_date { get; set; }
        public DateOnly? revoked_to_date { get; set; }
        public string? comp_name { get; set; }
        public string? leave_type_name { get; set; }
        public string? emp_mid { get; set; }

        [ForeignKey("emp_mid")]
        public virtual Employee? employee { get; set; }
        public string? emp_name { get; set; }
        ///Info:- email is to be mapped to column CorrsMail in WCGT
        public string? emp_email { get; set; }
        public string? leave_status_name { get; set; }
        public Boolean? isactive { get; set; }
        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }
    }
}
