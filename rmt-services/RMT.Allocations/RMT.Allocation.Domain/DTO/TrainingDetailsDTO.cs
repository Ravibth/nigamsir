using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{
    public class TrainingDetailsDTO
    {
        public string EmailId { get; set; }
        public string TrainingName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Boolean IsFullDay { get; set; }  
        public int PerDayHours { get; set; }

    }
}
