using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class UpdateListOfAllocationDetailsStatusResponse
    {
        public List<Guid> Guids { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public bool? IsProjectCompetencyRefresh { get; set; }
        public int AllocationDeletionCount { get; set; }
        public List<string> EmployeeListForSuperCoach { get; set; }
    }
}
