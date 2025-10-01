using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceLayer.DTOs
{
    public enum RoleType
    {
        Requestor,
        EngagementLeader,
        EO,
        Delegate,
    }

    public class ProjectRoles
    {
        public Int64 Id { get; set; }
        public Int64 ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        public string User { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string? ApplicationRole { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }

    public class ProjectRolesView
    {
        public Int64 Id { get; set; }

        public Int64 ProjectId { get; set; }

        public string User { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public string? ApplicationRole { get; set; }
        public string? Description { get; set; }

        public string? ParentEmail { get; set; }

        public string? ParentName { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
