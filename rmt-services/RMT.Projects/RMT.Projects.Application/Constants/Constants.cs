using static RMT.Projects.Domain.Constant;

namespace RMT.Projects.Application.Constants
{


    public static class Constants
    {
        public static List<string> ResourceRequestors = new List<string>() { UserRoles.EO, UserRoles.EngagementLeader, UserRoles.ProposedEL, UserRoles.JobManager };
        public static class NotificationTemplateTypes
        {
            public const string PROJECT_DELEGATE_UPDATE_NOTIFICATION = "PROJECT_DELEGATE_UPDATE_NOTIFICATION";
            public const string PROJECT_ADDITIONAL_EL_UPDATE_NOTIFICATION = "PROJECT_ADDITIONAL_EL_UPDATE_NOTIFICATION";
            public const string PROJECT_ADDITIONAL_DELEGATE_UPDATE_NOTIFICATION = "PROJECT_ADDITIONAL_DELEGATE_UPDATE_NOTIFICATION";
            public const string PROJECT_END_DATE_UPDATE_NOTIFICATION = "PROJECT_END_DATE_UPDATE_NOTIFICATION";
            public const string PROJECT_SUSPENSION_ALLOCATION_RELEASE_NOTIFICATION = "PROJECT_SUSPENSION_ALLOCATION_RELEASE_NOTIFICATION";
            public const string JOB_INACTIVE_ALLOCATION_RELEASE_NOTIFICATION = "JOB_INACTIVE_ALLOCATION_RELEASE_NOTIFICATION";

        }
    }
}