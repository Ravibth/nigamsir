using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Domain.Entities
{
    public class Job
    {
        [Key]
        public string? job_id { get; set; }
        public string? job_code { get; set; }
        public string? pipeline_id { get; set; }
        public string? pipeline_code { get; set; }
        public string? parent_job_id { get; set; }
        public string? job_name { get; set; }
        public string? job_description { get; set; }
        public string? asst_incharge { get; set; }
        public string? entity { get; set; }
        public string? job_client { get; set; }
        public string? market { get; set; }
        public string? sub_market { get; set; }
        public string? job_location_id { get; set; }
        public string? billing_currency { get; set; }
        public Boolean? is_chargeable { get; set; }
        public string? remarks { get; set; }
        public DateOnly? start_date { get; set; }
        public DateOnly? end_date { get; set; }
        public Boolean? closed_job { get; set; }
        public DateOnly? created_date { get; set; }
        public DateOnly? updated_date { get; set; }
        public virtual ICollection<JobRole>? job_roles { get; set; }

        public double? jobBudgetValue { get; set; }
        public double? agreedJobFee { get; set; }

        public string? client_group_id { get; set; }
        public string? industry_id { get; set; }

        public string? sub_industry_id { get; set; }
        public string? recurring_type { get; set; }
        public string? bu_id { get; set; }
        public string? offering_id { get; set; }

        public string? solution_id { get; set; }
        public string? pipeline_status { get; set; }

        public Boolean? isactive { get; set; }
        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }

    }
}
