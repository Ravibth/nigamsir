using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMT.Projects.Domain.Entities;

namespace RMT.Projects.Domain.Repositories
{
    public class ProjectRequisitionAllocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project? Project { get; set; }
        //public string ProjectCode { get; set; }//feb
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public int RequisitionCount { get; set; }
        public int AllocationCount { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
