using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Application.DTO.Request
{
    public class ScheduledVsActualVarianceChartRequestDto
    {
        public List<string>? BusinessUnit { get; set; }

        public List<string>? Competency { get; set; }
        public List<string>? Offering { get; set; }
        public List<string>? Solution { get; set; }

        public List<string>? Location { get; set; }
        public List<string>? Designation { get; set; }
        public string? EmailId { get; set; }
        public string ReportType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
