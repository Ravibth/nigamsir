using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RMT.Projects.Domain.Entities
{
    public enum RoleType
    {
        Requestor,
        EngagementLeader,
        EO,
        Delegate,
        AdditionalEl
    }

    public class ProjectRoles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        public Int64 ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project? Project { get; set; }

        [EmailAddress]
        public string User { get; set; }
        public string UserName { get; set; }

        [Required]
        [EnumDataType(typeof(RoleType))]
        public string Role { get; set; }

        //public string? DelegateRole { get; set; }
        public string? DelegateUserName { get; set; }
        public string? DelegateEmail { get; set; }

        public string? Description { get; set; }
        public string? ApplicationRole { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        //[Timestamp]
        public DateTime? CreatedAt { get; set; }

        //[Timestamp]
        public DateTime? ModifiedAt { get; set; }

        [NotMapped]
        public string? ParentEmail { get; set; }
        [NotMapped]
        public string? ParentName { get; set; }

    }
}
