using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Infrastructure.Infra.Request
{
    public class CapacityUtiliationOverviewRequestInfra
    {
        public List<string>? BusinessUnit { get; set; }

        public List<string>? Competency { get; set; }
        //public List<string>? Offering { get; set; }
        //public List<string>? Solution { get; set; }

        public List<string>? EmpMids { get; set; }
        //public bool CheckEmpMids { get; set; }

        public List<string>? Location { get; set; }
        public List<string>? Designation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
