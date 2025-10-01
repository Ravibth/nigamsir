using RMT.Configuration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Application.DTOs.NavigationDTOs
{
    public class RoleMenuDTO
    {
        public Int64 Id { get; set; }

        public string Role { get; set; }

        public Int64 MenuId { get; set; }

        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
