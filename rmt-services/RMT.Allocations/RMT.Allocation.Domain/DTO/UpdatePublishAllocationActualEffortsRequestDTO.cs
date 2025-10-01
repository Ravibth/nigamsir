using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs.ResourceAllocationDTOs
{
    public class UpdatePublishAllocationActualEffortsRequestDTO
    {
        public DateTime? DateLog { get; set; }
        public Decimal? TotalTime { get; set; }
        public String? JobName { get; set; }
        public String? JobCode { get; set; }
        public Int16? ChargeableFlag { get; set; }
        public Int16? STATUS_FLG { get; set; }
        public String? STATUS { get; set; }
        public String? Designation { get; set; }
        public String? GradeName { get; set; }
        public Decimal? RATE { get; set; }
        public String? EmployeeMID { get; set; }
        public String? EmpName { get; set; }
        public String? EmpEmail { get; set; }
        public String? EmployeeBU { get; set; }
        public String? EmployeeExpertise { get; set; }
        public String? EmployeeSMEG { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public String? EmployeeID { get; set; }
        public String? JobID { get; set; }
    }
}
