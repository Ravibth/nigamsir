using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Infrastructure.DTOs.Base
{
    public interface IResponseBase
    {
        public Boolean isfailed { get; set; }
        public string? failed_message { get; set; }
    }
}
