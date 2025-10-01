using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.DTO
{
    public class RefreshEmpProjectInterestScoreRequestDTO
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string RequisitionActionType { get; set; }
    }
}
