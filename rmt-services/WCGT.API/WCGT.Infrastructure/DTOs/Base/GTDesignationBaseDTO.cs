using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Infrastructure.DTOs.Base
{
    public class GTDesignationBaseDTO
    {
        public string designation_id { get; set; }
        public string designation_name { get; set; }
        public string grade { get; set; }
        public string? description { get; set; }
        public bool isactive { get; set; }

    }
}
