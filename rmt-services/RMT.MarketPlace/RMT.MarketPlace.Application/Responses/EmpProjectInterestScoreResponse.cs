using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.Responses
{
    public class EmpProjectInterestScoreResponse
    {
        public Int64? EmpProjectInterestId { get; set; }
        //public EmpProjectInterestScore? EmpProjectInterest { get; set; }

        public string? RequisitionId { get; set; }
        public string? RequisitionDesignation { get; set; }
        public string? RequisitionGrade { get; set; }
        public string? RequisitionBU { get; set; }
        public string? RequisitionOfferings { get; set; }
        public string? RequisitionSolutions { get; set; }
        public string? RequisitionCompetency { get; set; }

        public string? RequisionScore { get; set; }

        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
