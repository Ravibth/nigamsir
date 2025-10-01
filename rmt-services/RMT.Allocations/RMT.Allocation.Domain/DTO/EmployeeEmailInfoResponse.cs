using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class EmployeeEmailInfoResponse
    {
        public DateOnly leave_date { get; set; }
        public string employee_email { get; set; }
        public string emp_mid { get; set; }
        public int leave_hours { get; set; }
        public string leave_type { get; set; }
    }
}
