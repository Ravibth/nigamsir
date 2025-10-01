using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Infrastructure
{
    public class Constants
    {
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

        public const string ConfigTypeOfferings = "OFFERINGS";

        public const string ConfigTypeExpertise = "EXPERTISE";

        public const string ConfigTypeBusinessUnit = "BUSINESS_UNIT";

        public static class ApplicationLevelSettingsKeys
        {
            public const string Weekends = "Weekends";
            public const string ResignedUserAvailabilityThreshold = "ResignedUserAvailabilityThreshold";
        }
    }
}
