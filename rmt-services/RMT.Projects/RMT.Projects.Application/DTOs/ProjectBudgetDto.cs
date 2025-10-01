using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.DTOs
{
    public class ProjectBudgetDto
    {
        public string? JobCode { get; set; }
        public string PipelineCode { get; set; }
        public string Designation { get; set; }
        public string Grade { get; set; }
        public double RatePerHour { get; set; }
        public double BudgetedHour { get; set; }
        public double OriginalRatePerHour { get; set; }
        public double OriginalBudgetedHour { get; set; }
    }
}
