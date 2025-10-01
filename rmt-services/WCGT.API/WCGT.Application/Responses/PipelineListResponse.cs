using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Domain.Entities;
using WCGT.Infrastructure.DTOs;
using WCGT.Infrastructure.DTOs.Base;

namespace WCGT.Application.Responses
{

    public class PipelineListResponse : GTPipelineBaseDTO, IEntityBase, IResponseBase
    {
        //public virtual ICollection<GTPipelineRoleDTO>? pipeline_roles { get; set; }
        public Boolean isfailed { get; set; }
        public string? failed_message { get; set; }
        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }
    }
}
