using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Domain.DTOs.Request
{
    public class UpdatePublishedToMarketPlaceDTO
    {
        public bool IsPublishedToMarketPlace { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public DateTime? MarketPlaceExpirationDate { get; set; }
    }
}
