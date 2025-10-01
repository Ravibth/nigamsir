using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Application.DTO.Response
{

    public class SummaryStatisticsChartResponseDto
    {

        public double TotalProjectCount { get; set; }
        public double FutureAllocationHrs { get; set; }//YtdAllocationHrs
        public double JobAllocationsHrs { get; set; }
        public double PipelineAllocationsHrs { get; set; }
        public double TotalCapacityHrs { get; set; }
        public double TotalCapacityCost { get; set; }
        public double TotalAllocatedHrs { get; set; }
        public double TotalAllocatedCost { get; set; }
        public double TotalActualHrs { get; set; }
        public double TotalActualCost { get; set; }
        public double ChargeableAllocatedHrs { get; set; }
        public double ChargeableAllocatedCost { get; set; }
        public double NonChargeableAllocatedHrs { get; set; }
        public double NonChargeableAllocatedCost { get; set; }

        public double FutureChargeableAllocatedHrs { get; set; }
        public double FutureChargeableAllocatedCost { get; set; }
        public double FutureNonChargeableAllocatedHrs { get; set; }
        public double FutureNonChargeableAllocatedCost { get; set; }

        public double ChargeAbleActualHrs { get; set; }
        public double ChargeAbleActualCost { get; set; }
        public double NonChargeableActualHrs { get; set; }
        public double NonChargeableActualCost { get; set; }

        public double ChargeableAllocatedHrsCurrent { get; set; }
        public double ChargeableAllocatedCostCurrent { get; set; }

        public double NonChargeableAllocatedHrsCurrent { get; set; }
        public double NonChargeableAllocatedCostCurrent { get; set; }

        public double ChargeAbleActualHrsPrevious { get; set; }
        public double ChargeAbleActualCostPrevious { get; set; }

        public double NonChargeableActualHrsPrevious { get; set; }
        public double NonChargeableActualCostPrevious { get; set; }


        public double TotalAllocatedHrsCurrent { get; set; }
        public double TotalAllocatedCostCurrent { get; set; }

        public double TotalCapacityHrsCurrent { get; set; }
        public double TotalCapacityCostCurrent { get; set; }

        public double TotalActualHrsPrevious { get; set; }
        public double TotalActualCostPrevious { get; set; }

        public double TotalCapacityHrsPrevious { get; set; }
        public double TotalCapacityCostPrevious { get; set; }

        public List<SummaryStatisticsChartDataDto> SummaryStatisticsData { get; set; }

    }

    public class SummaryStatisticsChartDataDto
    {
        public string date { get; set; }
        public string expertise { get; set; }//Recheck
        public string business_unit { get; set; }
        public string sme_group_name { get; set; }//Recheck

        public string? BUId { get; set; }

        public string? Offerings { get; set; }
        public string? Solutions { get; set; }

        public string? OfferingsId { get; set; }
        public string? SolutionsId { get; set; }

        public double actual_log_hours { get; set; }
        public double capacity { get; set; }
        public double allocation_hours { get; set; }
        //public string chargable { get; set; }
        public double allocated_chargable_hr { get; set; }
        public double allocated_chargable_cost { get; set; }
        public double allocated_non_chargable_hr { get; set; }
        public double allocated_non_chargable_cost { get; set; }
        public double job_chargeable_hours { get; set; }
        public double job_non_chargeable_hours { get; set; }
        public double job_chargeable_cost { get; set; }
        public double job_non_chargeable_cost { get; set; }
        //public string job_chargeable { get; set; }
        //public string job_non_chargeable { get; set; }
        public double availability { get; set; }
        public float allocated_cost { get; set; }
        public float actual_cost { get; set; }
        public string email_id { get; set; }
        public float capacity_cost { get; set; }
        public double availability_cost { get; set; }
        public string location { get; set; }
        public string department { get; set; }
        public string designation_name { get; set; }
        public string grade { get; set; }
        public string? pipeline_code { get; set; }
        public int pipeline_code_count { get; set; }
        public string? job_code { get; set; }
        public int job_code_count { get; set; }

        //public DateOnly? working_date { get; set; }
        //public DateOnly? allocation_date { get; set; }

    }
}
