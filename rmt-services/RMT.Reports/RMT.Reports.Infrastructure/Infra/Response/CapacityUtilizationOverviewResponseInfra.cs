using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Infrastructure.Infra.Response
{
    public class CapacityUtilizationOverviewResponseInfra
    {
        public string business_unit { get; set; }
        public string competency { get; set; }

        public double actual_log_hours { get; set; }
        public double capacity { get; set; }
        public double allocation_hours { get; set; }
        public double allocated_chargable_hr { get; set; }
        public double allocated_chargable_cost { get; set; }
        public double allocated_non_chargable_hr { get; set; }
        public double allocated_non_chargable_cost { get; set; }
        public double job_chargeable_hours { get; set; }
        public double job_non_chargeable_hours { get; set; }
        public double job_chargeable_cost { get; set; }
        public double job_non_chargeable_cost { get; set; }
        public double availability { get; set; }
        public float capacity_cost { get; set; }
        public string employee_mid { get; set; }
        public string email_id { get; set; }
        public string employee_name { get; set; }
        public float actual_cost { get; set; }
        public float allocated_cost { get; set; }
        public double availability_cost { get; set; }
        public string location { get; set; }
        public string department { get; set; }
        public string designation_name { get; set; }
        public string grade { get; set; }

        public double leave_hrs { get; set; }
        public double net_leaves { get; set; }

        public double allocation_percent { get; set; }
        public double allocation_chargability_percent { get; set; }
        public double actual_chargability_percent { get; set; }

        public string supercoach_mid { get; set; }
        public string supercoach_name { get; set; }
        public string csc_mid { get; set; }
        public string csc_name { get; set; }
        public string skills { get; set; }

    }
}
