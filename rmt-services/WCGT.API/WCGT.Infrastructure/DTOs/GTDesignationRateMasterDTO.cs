using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Infrastructure.DTOs
{
    public class GTDesignationRateMasterDTO
    {
        public string grade { get; set; }

        public string CompetencyId { get; set; }

        public Double RatePerHour { get; set; }
        public Boolean isactive { get; set; }
        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }
    }
}
