using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class EmployeePrefDTO
    {
        public string Name { get; set; }
        public string Category { get; set; }

    }
    public class EmployeePreferencesByEmailDTO
    {
        public string Email { get; set; }

        public List<EmployeePrefDTO> EmployeePreference { get; set; }

    }
}
