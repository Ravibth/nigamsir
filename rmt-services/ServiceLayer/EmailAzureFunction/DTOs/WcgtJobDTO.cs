using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs
{
    public class WcgtJobDTO
    {
        public string? job_id { get; set; }
        public string? job_code { get; set; }
        public string? pipeline_id { get; set; }
        public string? pipeline_code { get; set; }
        public string? parent_job_id { get; set; }
        public string? job_name { get; set; }
        public string? job_description { get; set; }
        public string? asst_incharge { get; set; }
        //public string? engagement_leader_empid { get; set; }
        //public string? engagement_leader_empname { get; set; }
        //public string? signing_partner_empid { get; set; }
        //public string? signing_partner_empname { get; set; }
        //public string? lead_generator_empid { get; set; }
        //public string? lead_generator_empname { get; set; }
        //public string? second_engagement_leader_empid { get; set; }
        //public string? second_engagement_leader_empname { get; set; }
        //public string? engagement_quality_reviewer_empid { get; set; }
        //public string? engagement_quality_reviewer_empname { get; set; }
        //public string? third_engagement_leader { get; set; }
        //public string? third_engagement_leader { get; set; }
        public string? entity { get; set; }
        public string? job_client { get; set; }
        public string? expertise_id { get; set; }//same as service_line_id//Recheck
        public string? smeg_id { get; set; }//same as service_line_id//Recheck
        public string? revenue_id { get; set; }//Recheck
        public string? sme_id { get; set; }//Recheck
        public string? market { get; set; }
        public string? sub_market { get; set; }
        public string? job_location_id { get; set; }
        public string? billing_currency { get; set; }
        public Boolean? is_chargeable { get; set; }
        public string? remarks { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? job_start_date { get; set; }

        public DateTime? end_date { get; set; }
        public Boolean? active { get; set; }
        public Boolean? closed_job { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? updated_date { get; set; }

        public bool isactive { get; set; }
        public double? jobBudgetValue { get; set; }
        public double? agreedJobFee { get; set; }
        public string? client_group_id { get; set; }
        public string? industry_id { get; set; }
        public string? sub_industry_id { get; set; }
        public string? recurring_type { get; set; }
        public string? bu_id { get; set; }
        public string? offering { get; set; }
        public string? offering_id { get; set; }

        public string? solution { get; set; }
        public string? solution_id { get; set; }

        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }
    }
}
