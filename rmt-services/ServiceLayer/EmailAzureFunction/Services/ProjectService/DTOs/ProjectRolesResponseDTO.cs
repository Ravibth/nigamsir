using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.ProjectService.DTOs
{
    public class ProjectRolesResponseDTO
    {
        public Int64? Id { get; set; }
        public Int64? ProjectId { get; set; }

        //public Project? Project { get; set; }

        public string? User { get; set; }
        public string? UserName { get; set; }

        public string? Role { get; set; }
        public string? ApplicationRole { get; set; }
        public string? Description { get; set; }
        public string? DelegateUserName { get; set; }
        public string? DelegateEmail { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}
