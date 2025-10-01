using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.HttpServices.DTO
{
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
