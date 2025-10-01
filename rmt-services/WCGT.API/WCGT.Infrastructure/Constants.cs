using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Infrastructure
{
    public class Constants
    {
        public const string connectionString = "WCGTDB";
        public const string TempClientTable = "tempclients";
        public const string TargetClientTable = "Clients";

        public const string TempPipelineTable = "temppipelines";
        public const string TargetPipelineTable = "Pipeline";

        public const string TempJobsTable = "tempjobs";
        public const string TargetJobsTable = "Jobs";

        public const string TempEmployeeTable = "tempemployee";
        public const string TargetEmployeeTable = "Employee";

        public const string TempHolidayTable = "tempholidays";
        public const string TargetHolidayTable = "Holidays";

        public const string ABSCONDER = "absconder";
        public const string VOLUNTARY = "voluntary";
        public const string INVOLUNTARY = "involuntary";
        
        public const string sp_getuserwithleavesholidaysandavailability = "sp_getuserwithleavesholidaysandavailability";


        public static class UserRoles
        {
            public const string Employee = "Employee";
            public const string ResourceRequestor = "ResourceRequestor";
            public const string Delegate = "Delegate";
            public const string Admin = "Admin";
            public const string CEOCOO = "CEOCOO";
            public const string Reviewer = "Reviewer";
            public const string Leaders = "Leaders";
            public const string SystemAdmin = "SystemAdmin";
            public const string AdditionalEl = "AdditionalEl";
            public const string AdditionalDelegate = "AdditionalDelegate";
            public const string EngagementLeader = "EngagementLeader";
        }
        public const string TimesheetSP = "sp_timesheet_view";
        public const string ResourceTimesheetSP = "sp_resource_timesheet_view";
    }

    public static class Extension
    {
        public static string ToLowerCase(this string? str)
        {
            if (str == null)
            {
                return string.Empty;
            }   
            else
            {
                return str.Trim().ToLower();
            }
        }
    }
}
