using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Responses
{
    public class AllocationWithLeavesAndResourceRequestorsResponse
    {
        public string EmployeeEmail { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
        public string PipelineCode { get; set; }
        public string JobCode { get; set; }
        public List<string> ResourceRequestors { get; set; }
    }
}
