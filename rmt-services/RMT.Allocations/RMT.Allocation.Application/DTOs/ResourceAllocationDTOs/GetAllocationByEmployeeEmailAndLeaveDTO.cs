using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs.ResourceAllocationDTOs
{
    public class GetAllocationByEmployeeEmailAndLeaveDTO
    {
        public string EmployeeEmail { get; set; }
        public DateTime LeaveStartDate { get; set; }
        public DateTime LeaveEndDate { get; set; }
    }
}
