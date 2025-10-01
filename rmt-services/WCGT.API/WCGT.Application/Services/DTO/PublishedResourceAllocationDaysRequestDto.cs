using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Application.Services.DTO
{
    public class PublishedResourceAllocationDaysRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> EmpEmail { get; set; }
    }
}
