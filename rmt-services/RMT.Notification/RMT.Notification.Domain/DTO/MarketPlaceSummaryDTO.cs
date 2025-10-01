using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Domain.DTO
{
    public class MarketPlaceSummaryDTO
    {
        public string PipelineCode { get; set; }
        public string PipelineName { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public int NoOfInterest { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ExitDate { get; set; }

    }
}
