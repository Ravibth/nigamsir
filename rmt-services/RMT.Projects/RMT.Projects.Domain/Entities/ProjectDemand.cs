using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMT.Projects.Domain.Entities
{
    public class ProjectDemand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        public Int64 ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project? Project { get; set; }

        public string Designation { get; set; }

        public int NoOfResources { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        //[Timestamp]
        public DateTime? CreatedAt{ get; set; }

        //[Timestamp]
        public DateTime? ModifiedAt{ get; set; }

        public virtual ICollection<ProjectDemandSkills> ProjectDemandSkills { get; set; }
    }
}
