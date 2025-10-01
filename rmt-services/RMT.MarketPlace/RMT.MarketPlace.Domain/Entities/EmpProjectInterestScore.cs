using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Domain.Entities
{
    public class EmpProjectInterestScore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? ID { get; set; }
        public Int64? EmpProjectInterestId { get; set; }
        [ForeignKey("EmpProjectInterestId")]
        public virtual EmpProjectInterest? EmpProjectInterest { get; set; }
        public string? RequisitionId { get; set; }
        public string? RequisitionDesignation { get; set; }
        public string? RequisitionGrade { get; set; }
        public string? RequisionScore { get; set; }
        public string? Suggestion { get; set; }
        public string? RequisitionParameters { get; set; }

        public string? RequisitionBU { get; set; }
        public string? RequisitionOfferings { get; set; }
        public string? RequisitionSolutions { get; set; }
        public string? RequisitionCompetency { get; set; }

        [NotMapped]
        public bool? IsInterested { get; set; }
        [NotMapped]
        public string? EmpName { get; set; }
        [NotMapped]
        public string? EmpEmail { get; set; }

        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
