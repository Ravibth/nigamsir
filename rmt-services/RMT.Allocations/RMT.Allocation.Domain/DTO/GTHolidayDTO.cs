using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs
{
    public class GTHolidayDTO
    {
        public string? holiday_name { get; set; }
        public string? holiday_type { get; set; }
        public string? location_name { get; set; }
        public string location_id { get; set; }
        public DateTime holiday_date { get; set; }
        public bool ? is_active { get; set; }
    }
}
