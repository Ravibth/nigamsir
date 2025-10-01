using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs
{
    public class EmployeePerDayTimelineForEmployeePortfolioDto
    {
        public string EmployeeEmail { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeName { get; set; }
        public int AllocationHours { get; set; }
        public int AvailableHours { get; set; }
        public int LeaveHours { get; set; }

    }
}
