using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Infrastructure.DTOs.Base
{
    public class GTLocationBaseDTO
    {
        public string location_id { get; set; }
        public string? location_mid { get; set; }
        public string location_name { get; set; }
        public string? region_name { get; set; }
        public bool isactive { get; set; }

    }
}
