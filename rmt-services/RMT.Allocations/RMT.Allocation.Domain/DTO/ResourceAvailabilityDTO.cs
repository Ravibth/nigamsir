using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class ResourceAvailabilityDTO
    {
        public string email { get; set; }
        public int available_effort { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }

        public int leaves { get; set; }
       
        public int total_required_hours { get; set; }
      
    }
}
