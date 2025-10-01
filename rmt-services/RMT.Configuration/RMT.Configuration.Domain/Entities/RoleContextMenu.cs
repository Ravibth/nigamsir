using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Domain.Entities
{
    public class RoleContextMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        public string Role { get; set; }

        public Int64 ContextMenuId { get; set; }

        [ForeignKey("ContextMenuId")]
        public virtual ContextMenuMaster ContextMenu { get; set; }

        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
