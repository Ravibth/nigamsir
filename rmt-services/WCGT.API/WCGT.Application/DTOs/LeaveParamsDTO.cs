using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Application.DTOs
{
    public class LeaveParamsDTO
    {
        public List<string>? emp_emailid { get; set; }
        public List<string>? emp_mid { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
        public DateTime? created_at { get; set; }

    }
}
