using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Domain.DTOs
{
    public class EmpByProjectMappingRequestDto
    {
        public List<string>? Offerings { get; set; }
        public List<string>? Solutions { get; set; }
    }
}
