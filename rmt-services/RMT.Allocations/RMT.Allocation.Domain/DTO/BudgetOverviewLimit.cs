using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class BudgetOverviewLimit
    {
        public bool IsBudgetValueCross {  get; set; }
        public bool IsTimesheetHoursCross { get; set; }
    }
}
