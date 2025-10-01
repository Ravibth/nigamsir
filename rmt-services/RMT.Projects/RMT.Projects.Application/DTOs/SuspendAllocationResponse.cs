using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.DTOs
{
    public class SuspendAllocationResponse
    {
        public List<KeyValuePair<string, string>> projectCodes;
        public string EmpEmail { get; set; }
    }
}
