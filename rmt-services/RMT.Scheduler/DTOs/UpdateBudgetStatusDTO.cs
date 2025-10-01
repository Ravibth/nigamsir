using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.DTOs
{
    public class UpdateBudgetStatusDTO
    {
        //public string ProjectCode { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string BudgetStatus { get; set; }
    }
}
