using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.DTOs
{
    public class ProjectActualBudgetResponse
    {
        public string Action { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobName { get; set; }
        public string? ProjectName { get; set; }
        public string? sender_for_notification { get; set; }
        public string? ConsumedTimesheetBudgetPct { get; set; }
    }
}
