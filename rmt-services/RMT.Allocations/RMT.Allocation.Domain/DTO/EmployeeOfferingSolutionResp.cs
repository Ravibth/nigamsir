using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class EmployeeOfferingSolutionResp
    {
        public string emp_mid { get; set; }
        public List<string> offerings { get; set; }
        public List<string> solutions { get; set; }
    }
}
