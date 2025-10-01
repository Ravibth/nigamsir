using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLayer.DTOs
{
    public class ProjectJobCodes
    { 
        public Int64 Id { get; set; }
        public Int64 ProjectId { get; set; }
        public virtual Project? Project { get; set; }        
        public string? ProjectCode { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? JobName { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedAt{ get; set; }
        public DateTime? ModifiedAt{ get; set; }
    }
}
