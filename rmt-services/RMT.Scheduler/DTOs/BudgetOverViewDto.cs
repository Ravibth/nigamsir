using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.DTOs
{
    public class BudgetOverviewRequest
    {
        public List<KeyValuePair<string, string>> JobCodes { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
    public class BudgetOverViewDto
    {
        public string JobCode { get; set; }
        public string PipelineCode { get; set; }
        public int TotalResourcesCount { get; set; }

        public int TotalAllocatedHours { get; set; }

        public double TotalAllocatedCost { get; set; }

        public int ConsumedHours { get; set; }
        public Double ConsumnedCost { get; set; }
        public int RemainingHours { get; set; }

        public Double RemainingCost { get; set; }
        public Double PercentageHrs { get; set; }
        public Double PercentageCost { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public double JobFee { get; set; }
        public double OriginalBudgetCost { get; set; }
        public double OriginalBudgetHrs { get; set; }
        public double BudgetedCost { get; set; }
        public double BudgetedHrs { get; set; }

    }
}
