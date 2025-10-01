using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Domain.Entities
{
    public class DesignationGradeView
    {
        public string designation_id { get; set; }
        public string? designation_name { get; set; }
        public string? CompetencyName { get; set; }
        public string? CompetencyId { get; set; }
        public string? grade { get; set; }
        public string? description { get; set; }
        public Double RatePerHour { get; set; }
        public Boolean? isactive { get; set; }
        public DateTime? createdat { get; set; }
        public DateTime? modifiedat { get; set; }
        public string? createdby { get; set; }
        public string? modifiedby { get; set; }
    }
}
