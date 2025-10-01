using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.DTOs
{
    public class GetOfferingSolutionsByJobCodeResponseDTO
    {
        public string Solution { get; set; }
        public string SolutionId { get; set; }
        public string Offering { get; set; }
        public string OfferingId { get; set; }
        public string JobCode { get; set; }
    }
}
