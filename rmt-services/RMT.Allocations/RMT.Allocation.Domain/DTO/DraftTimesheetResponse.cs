using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class DraftTimesheetResponse
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? JobId { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeEmail { get; set; }
        public DateOnly AllocationDate { get; set; }
        public int AllocatedHours { get; set; }
        //public string ActivityId {  get; set; }
        //public string GroupActivityId {  get; set; }
        //public string Narration {  get; set; }
    }
}
