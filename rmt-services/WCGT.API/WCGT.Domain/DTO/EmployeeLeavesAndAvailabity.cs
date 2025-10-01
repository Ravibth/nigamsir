using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Domain.DTO
{
    public class EmployeeLeavesHolidayAndAvailabity
    {
        public string name { get; set; }
        public string employee_mid { get; set; }
        public string super_coach_mid { get; set; }
        public string co_super_coach_mid { get; set; }
        public string business_unit_id { get; set; }
        public string designation_name { get; set; }
        public string designation_id { get; set; }
        public string grade_name { get; set; }
        public string location_name { get; set; }
        public string location_id { get; set; }
        public string competencyId { get; set; }
        public string email_id { get; set; }
        public string email_id_uid { get; set; }
        public int available_hrs { get; set; }
        public int holiday_hrs { get; set; }
        public int leave_hrs { get; set; }
        public int allocation_hrs { get; set; } = 0;
        public int availability { get; set; } = 0;
        public string clientgroup { get; set; }        
        public string client { get; set; }
        public string supercoach_name { get; set; }
        public string co_supercoach_name { get; set; }
        public DateTime working_date { get; set; }
    }
}
