using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.DTOs
{
    public class SuspendedProjectRequest
    {
       public List<KeyValuePair<string, string>> projectCodes;
       public Boolean isSuspended;
    }

    public class TriggerInfo
    {
        public TriggerScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class TriggerScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
