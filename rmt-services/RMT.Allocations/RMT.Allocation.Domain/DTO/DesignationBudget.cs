using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class DesignationBudget
    {
        public string JobCode { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public Double BudgetHrs { get; set; }
        public Double OriginalBudgetCost { get; set; }
        public Double OriginalBudgetHrs { get; set; }
        public Double BudgetCost { get; set; }
        public Double AllocatedCost { get; set; }
        public Double AllocatedHrs { get; set; }
        public Double TimesheetHrs { get; set; }
        public Double TimesheetCost { get; set; }
    }
}
