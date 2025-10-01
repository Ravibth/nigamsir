using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Domain.Entities
{
    public class ProjectBudget
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        public Int64 ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project? Project { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        //public string Designation { get; set; }
        public string Grade { get; set; }
        public double RatePerHour { get; set; }
        public double BudgetedHour { get; set; }
        public double OriginalRatePerHour { get; set; }
        public double OriginalBudgetedHour { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
