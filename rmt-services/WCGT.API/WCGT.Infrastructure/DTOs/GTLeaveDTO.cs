using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Infrastructure.DTOs.Base;

namespace WCGT.Infrastructure.DTOs
{
    public class GTLeaveDTO : GTLeaveBaseDTO, IEntityBase
    {
        public DateTime? createdat { get; set; }

        public DateTime? modifiedat { get; set; }

        public string? createdby { get; set; }

        public string? modifiedby { get; set; }
    }
}
