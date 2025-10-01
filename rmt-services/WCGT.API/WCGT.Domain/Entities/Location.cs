using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Domain.Entities
{
    public class Location
    {
        [Key]
        public string location_id { get; set; }
        public string? location_mid { get; set; }
        public string? location_name { get; set; }
        public string? region_name { get; set; }
        public Boolean? isactive { get; set; }
        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }
    }
}
