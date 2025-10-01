using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.MarketPlace.Domain.Entities
{
    public class EmpProjectInterest
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64? ID { get; set; }

        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public string? PipelineCode { get; set; }
        public string? PipelineName { get; set; }
        public bool? IsInterested { get; set; }
        public DateTime? InterestDate { get; set; }
        public string? EmpEmail { get; set; }
        public string? EmpName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

    }

}
