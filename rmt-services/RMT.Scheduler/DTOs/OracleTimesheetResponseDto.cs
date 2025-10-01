using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.DTOs
{
    public class OracleTimesheetResponseDto
    {
        public DateTime? DateLog { get; set; } = DateTime.Now;
        public Decimal? TotalTime { get; set; } = 0;
        public String? JobName { get; set; } = String.Empty;
        public String? JobCode { get; set; } = String.Empty;
        public Int16? ChargeableFlag { get; set; } = 0;
        public Int16? STATUS_FLG { get; set; } = 0;
        public String? STATUS { get; set; } = String.Empty;
        public String? Designation { get; set; } = String.Empty;
        public String? GradeName { get; set; } = String.Empty;
        public Decimal? RATE { get; set; } = 0;
        public String? EmployeeMID { get; set; } = String.Empty;
        public String? EmpName { get; set; } = String.Empty;
        public String? EmpEmail { get; set; } = String.Empty;
        public String? EmployeeBU { get; set; } = String.Empty;
        public String? EmployeeExpertise { get; set; } = String.Empty;
        public String? EmployeeSMEG { get; set; } = String.Empty;
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public String? EmployeeID { get; set; } = String.Empty;
        public String? JobID { get; set; } = String.Empty;
    }
}
