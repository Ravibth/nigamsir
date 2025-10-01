using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Infrastructure.DTOs.Base;

namespace WCGT.Application.Responses
{
    public class ProjectListResponse : GTProjectBaseDTO, IEntityBase, IResponseBase
    {
        public Boolean isfailed { get; set; }
        public string? failed_message { get; set; }
        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }
    }
}
