using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Responses
{

    public class TimelineType
    {
        //public const string LEAVE = "leave";
        public const string FULL_DAY_LEAVE = "FULL_DAY_LEAVE";
        public const string FIRST_HALF_LEAVE = "FIRST_HALF_LEAVE";
        public const string SECOND_HALF_LEAVE = "SECOND_HALF_LEAVE";
        public const string ALLOCATION = "allocation";
        public const string AVAILABLE = "available";
        public const string HOLIDAY = "holiday";
    }
    public class TimelineDisplayText
    {
        //public const string LEAVE = "Leave";
        public const string FULL_DAY_LEAVE = "Leave";
        public const string FIRST_HALF_LEAVE = "First Half Leave";
        public const string SECOND_HALF_LEAVE = "Second Half Leave";
        public const string ALLOCATION = "Allocation";
        public const string AVAILABLE = "Available";
        public const string HOLIDAY = "Holiday";
    }

    public class UsersTimeline
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int? HoursAlotted { get; set; }
        public int? leave_hours { get; set; }
        public string timeline_type { get; set; }
        public string timeline_display_text { get; set; }
        public string pipelineCode { get; set; }

        public UsersTimelineWeeklyAllocation WeeklyBreakup { get; set; } = new UsersTimelineWeeklyAllocation();
        public int? WeeklyTotal { get; set; }

    }


    public class GetUsersTimelineResponse
    {
        public string email { get; set; }
        public List<UsersTimeline> usersTimelines { get; set; }
    }

    public class UsersTimelineWeeklyAllocation
    {
        public string Mon { get; set; }
        public string Tue { get; set; }
        public string Wed { get; set; }
        public string Thu { get; set; }
        public string Fri { get; set; }
    }
}
