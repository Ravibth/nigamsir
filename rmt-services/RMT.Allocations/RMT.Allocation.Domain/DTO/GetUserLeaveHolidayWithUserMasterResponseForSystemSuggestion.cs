using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class GetUserLeaveHolidayWithUserMasterResponseForSystemSuggestion
    {
        public string emp_name { get; set; }
        public string email_id { get; set; }
        public string? designation { get; set; }
        public string? competency { get; set; }
        public string? competency_id { get; set; }
        public string? grade { get; set; }
        public string? location { get; set; }
        public string? supercoach { get; set; }
        public string? business_unit { get; set; }
        public string? supercoach_name { get; set; }
        public string? co_supercoach_name { get; set; }
        public string? supercoach_mid { get; set; }
        public string? co_supercoach_mid { get; set; }
        public string? employee_id { get; set; }
        public string? uemail_id { get; set; }
        public string? sub_industry { get; set; }
        public string? industry { get; set; }
        public Int64? holiday_hours { get; set; }
        public Int64? leave_hours { get; set; }
    }
}
