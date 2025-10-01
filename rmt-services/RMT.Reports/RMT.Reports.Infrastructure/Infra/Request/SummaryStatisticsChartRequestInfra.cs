using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Infrastructure.Infra.Request
{
    public class SummaryStatisticsChartRequestInfra
    {
        public List<string>? BusinessUnit { get; set; }

        public List<string>? Competency { get; set; }

        public List<string>? Location { get; set; }
        public string? EmailId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
