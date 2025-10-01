using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Infrastructure.Helpers
{
    public static class ConfigConstants
    {
        public static string AllocationDbConnStr = string.Empty;
        public static string WcgtDbConnStr = string.Empty;
        public static string ConfigDbConnStr = string.Empty;
        public static string ProjectDbConnStr = string.Empty;
        public static string SkillDbConnStr = string.Empty;

        public static class UserRoles
        {
            public const string Leaders = "Leaders";
            public const string CeoCoo = "CEOCOO";
            public const string SystemAdmin = "SystemAdmin";
            public const string SuperCoach = "SuperCoach";
            public const string CoachSuperCoach = "SuperCoach";
        }

        public static class MaterilizedViews
        {
            public const string employee_allocation_timesheet = "employee_allocation_timesheet";
            public const string employee_working_days = "employee_working_days";
            public const string project_budget = "project_budget";
            public const string employee_view = "employee_view";
            public const string employee_skill = "employee_skill";
        }

        public static class ReportViewType {
            public const string employeeView = "EmployeeView";
            public const string supercoachView = "SupercoachView";
            public const string coachSuperCoachView = "CoachSuperCoachView";
            public const string leaderView = "LeaderView";
        }

    }
}
