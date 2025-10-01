using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class BudgetOverviewDto
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
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Double JobFee { get; set; }
        public Double OriginalBudgetCost { get; set; }
        public Double OriginalBudgetHrs { get; set; }
        public Double BudgetedCost { get; set; }
        public Double BudgetedHrs { get; set; }
    }
}
