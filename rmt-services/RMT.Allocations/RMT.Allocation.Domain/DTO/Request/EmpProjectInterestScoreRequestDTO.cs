using RMT.Allocation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO.Request
{
    public class EmpProjectInterestScoreRequestDTO
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
        public string? Suggestion { get; set; }
        public List<RequisitionParameters>? RequisitionParameters { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsInterested { get; set; }
    }
}
