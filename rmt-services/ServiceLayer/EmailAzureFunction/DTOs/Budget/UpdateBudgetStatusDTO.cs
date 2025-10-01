using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DTOs.Budget
{
    public class UpdateBudgetStatusDTO
    {
        //public string ProjectCode { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string BudgetStatus { get; set; }
    }
}
