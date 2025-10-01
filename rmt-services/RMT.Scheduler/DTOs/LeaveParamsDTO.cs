using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.DTOs
{
    public class LeaveParamsDTO
    {
        public List<string>? emp_emailid { get; set; }
        public DateTime? created_at { get; set; }
    }
}
