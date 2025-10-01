using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Infrastructure.DTOs.Base;

namespace WCGT.Infrastructure.DTOs
{
    public class GTPipelineDTO : GTPipelineBaseDTO, IEntityBase
    {
        public virtual ICollection<GTPipelineRoleDTO>? pipeline_roles { get; set; }
        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }
    }
}
