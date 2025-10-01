using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using WCGT.Domain.Entities;
using WCGT.Infrastructure.DTOs.Base;

namespace WCGT.Infrastructure.DTOs
{
    public class GTJobDTO : GTJobBaseDTO, IEntityBase
    {
        public virtual ICollection<GTJobRoleDTO>? job_roles { get; set; }

        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }
    }
}
