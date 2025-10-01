using RMT.MarketPlace.Application.Responses;
using RMT.MarketPlace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Application.DTO
{
    public class EmpProjectInterestScoreDTO
    {
        public Int64? ID { get; set; }
        public Int64? EmpProjectInterestId { get; set; }
        public string? RequisitionId { get; set; }
        public string? RequisitionDesignation { get; set; }
        public string? RequisitionGrade { get; set; }
        public string? RequisitionBU { get; set; }
        public string? RequisitionOfferings { get; set; }
        public string? RequisitionSolutions { get; set; }
        public string? RequisitionCompetency { get; set; }
        public string? RequisionScore { get; set; }
        public SystemSuggestionResponseDTO? Suggestion { get; set; }
        public List<RequisitionParameters> RequisitionParameters {  get; set; }
        public bool? IsInterested { get; set; }
        public string? EmpName { get; set; }
        public string? EmpEmail { get; set; }
        public bool? IsActive { get; set; }
    }
}
