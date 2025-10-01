using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class HolidayDetailsDTO
    {
        public string EmailId { get; set; }
        public string HolidayName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        

    }
}
