using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class GetUserLeaveHolidayWithUserMasterRequestDTOForSystemSuggestion
    {
        public string designation { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
    }
}
