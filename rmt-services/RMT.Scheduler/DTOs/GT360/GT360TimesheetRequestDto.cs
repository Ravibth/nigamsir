using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.DTOs.GT360
{
    public class GT360TimesheetRequestDto
    {
        public string sClientID { get; set; }
        public string sProjectID { get; set; }
        public string sActivityID { get; set; }
        public string sHours { get; set; }
        public string sMinuts { get; set; }
        public string sNarration { get; set; }
        public string sDate { get; set; }
        public string sEmpID { get; set; }
        public string sToken { get; set; }

    }
}
