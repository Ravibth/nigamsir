using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Infrastructure.DTOs.Base
{
    public class GTHolidayBaseDTO
    {
        public string holiday_name { get; set; }
        public string? holiday_type { get; set; }
        public string location_id { get; set; }
        public string location_name { get; set; }
        public DateOnly holiday_date { get; set; }
        public DateOnly? cr_date { get; set; }
        public bool isactive { get; set; }
    }
}
