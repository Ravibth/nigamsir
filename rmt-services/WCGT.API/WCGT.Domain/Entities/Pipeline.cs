using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Domain.Entities
{
        public class Pipeline
        {
                [Key]
                public string? pipeline_id { get; set; }
                public string? pipeline_code { get; set; }
                public string? pipeline_name { get; set; }
                public string? project_code { get; set; }
                public string? project_name { get; set; }
                public string? location_id { get; set; }
                public string? expected { get; set; }
                public string? win_probablity { get; set; }
                public string? won_reason { get; set; }
                public string? job_id { get; set; }
                public string? emp_mid { get; set; }
                public string? emp_name { get; set; }
                public string? emp_location_id { get; set; }
                public string? emp_location_name { get; set; }
                public DateOnly? creation_date { get; set; }
                public DateOnly? won_date { get; set; }
                public string? recurring { get; set; }
                public string? industry_id { get; set; }
                public string? pipeline_status { get; set; }
                public string? pipeline_description { get; set; }
                public string? nrccstatus { get; set; }
                public double? finalproposedfee { get; set; }
                public double? finalproposedope { get; set; }
                public double? won_expected_recovery { get; set; }
                public string? contact_name { get; set; }
                public string? client_group_code { get; set; }
                public string? client_id { get; set; }
                public string? client_service_partner_id { get; set; }
                public Boolean? isactive { get; set; }
                public DateOnly? start_date { get; set; }
                public DateOnly? end_date { get; set; }
                public string? sub_industry_id { get; set; }
                public string? bu_id { get; set; }
                public string? offering_id { get; set; }
                public string? solution_id { get; set; }
                public DateTime? createdat { get; set; }
                public DateTime? modifiedat { get; set; }
                public string? createdby { get; set; }
                public string? modifiedby { get; set; }
                //public Double? overallbudget { get; set; } Field Not Required so deleting
                public virtual ICollection<PipelineRole>? pipeline_roles { get; set; }
        }
}
