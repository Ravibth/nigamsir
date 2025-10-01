using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO
{

    public class LeavesDTO
    {
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public int hours { get; set; }// these hours loginc to be changes
        public string leaveType { get; set; }
    }
    public class Leaves
    {
        public string email { get; set; }
        public List<LeavesDTO> leavesAllocationDTOs { get; set; }
        public int totalhours { get; set; }
    }

    public class UsersAvailability
    {
        public string email { get; set; }
        public bool available { get; set; }
    }
    public class UsersAvailabilityCheckDTO
    {
        public string[] emails { get; set; }
        public List<Leaves> leaves { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public int total_required_hours { get; set; }
        public int perday_max_effort { get; set; }

    }
}
