using RMT.Configuration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Application.DTOs.NavigationDTOs
{
    public class ContextMenuMasterDTO
    {
        public Int64 Id { get; set; }
        public string InternalName { get; set; }
        public string DisplayName { get; set; }
        public Int32 Order { get; set; }
        public string? Description { get; set; }

        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
