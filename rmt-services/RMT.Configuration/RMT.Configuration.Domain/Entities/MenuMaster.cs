using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Domain.Entities
{
    public class MenuMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        public string InternalName { get; set; }
        public string DisplayName { get; set; }
        public string? ParentId { get; set; }
        public Int32 Order { get; set; }
        public string? MenuType { get; set; }
        public string Path { get; set; }
        public string? Description { get; set; }
        public bool Is_Expandable { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }

        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool? IsDisplay { get; set; }

    }
}
