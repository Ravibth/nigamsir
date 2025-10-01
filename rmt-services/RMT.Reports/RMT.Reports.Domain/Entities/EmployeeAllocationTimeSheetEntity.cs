


namespace RMT.Reports.Domain.Entities
{
    public class EmployeeAllocationTimeSheetEntity
    {
        public string? employee_mid { get; set; }

        public string? employee_code { get; set; }

        public string? employee_name { get; set; }
        public string? email_id { get; set; }

        public string? department { get; set; }

        public string? location { get; set; }

        public DateOnly? working_date { get; set; }

        public string? designation_name { get; set; }

        public string? business_unit { get; set; }

        public string? expertise { get; set; }//Recheck

        public string? sme_group_name { get; set; }//Recheck

        public string? BUId { get; set; }

        public string? Offerings { get; set; }
        public string? Solutions { get; set; }

        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }

        public string? service_line { get; set; }

        public DateOnly? allocation_date { get; set; }

        public int? allocation_hours { get; set; }

        public string? pipeline_code { get; set; }

        public string? job_code { get; set; }

        public int? actual_log_hours { get; set; }


        public int? job_chargeable { get; set; }

        public int? job_non_chargeable { get; set; }

        public int? capacity { get; set; }

    }
}