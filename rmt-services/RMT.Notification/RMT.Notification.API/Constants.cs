namespace RMT.Notification.API
{
    public static class Constants
    {
        public const string GetLoggedInUserAllNotifications = "GetLoggedInUserAllNotifications";
        public const string FoundNewNotification = "FoundNewNotification";
        public const string JoinMasterGroup = "JoinMasterGroup";
        public const string LeaveMasterGroup = "LeaveMasterGroup";
        public const string GetLoggedInUserAllNotificationsCount = "GetLoggedInUserAllNotificationsCount";
        public const string NotificationCount = "NotificationCount";
        public enum AllocationStatus
        {
            Open = 0,
            InProgress = 1,
            Completed = 3
        }


        /// <summary>
        /// UserDecorator
        /// </summary>
        public class UserDecorator
        {
            public int? id { get; set; }
            public string? email { get; set; }
            public string? name { get; set; }
            public string? emp_code { get; set; }
            public string? designation { get; set; }
            public string? service_line { get; set; }
            public string[]? roles { get; set; }
            public string? role { get; set; }
            public string? supercoach_name { get; set; }
            public string? co_supercoach_name { get; set; }
            public string? supercoach_mid { get; set; }
            public string? co_supercoach_mid { get; set; }
            public string? employee_id { get; set; }
            public string? uemail_id { get; set; }
            public string? competency { get; set; }
            public string? competencyId { get; set; }
            public string? business_unit { get; set; }
            public string? location { get; set; }
            public string? grade { get; set; }
            public List<ModulePermissionDTO>? app_permissions { get; set; }
        }

        /// <summary>
        /// ModulePermissionDTO
        /// </summary>
        public class ModulePermissionDTO
        {
            public string module_name { get; set; }
            public int module_id { get; set; }
            public PermissionLevel permissions { get; set; }
            public bool is_assigned { get; set; }
        }

        /// <summary>
        /// Permission
        /// </summary>
        public class PermissionLevel
        {
            public bool read { get; set; }
            public bool update { get; set; }
            public bool create { get; set; }
            public bool delete { get; set; }
            public bool approve { get; set; }
        }
    }
}
