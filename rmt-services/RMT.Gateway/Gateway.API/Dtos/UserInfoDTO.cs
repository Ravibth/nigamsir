using System.Collections.Generic;

namespace Gateway.API.Dtos
{
    /// <summary>
    /// UserInfoDTO
    /// </summary>
    public class UserInfoDTO
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
        public List<ModulePermissionDTO>? app_permissions { get; set; }
        public string? competency { get; set; }
        public string? competencyId { get; set; }

    }

    public class SupercoachDelegate
    {
        public string id { get; set; }
        public string supercoach_mid { get; set; }
        public string? allocation_delegate_name { get; set; }
        public string? allocation_delegate_mid { get; set; }
        public string? allocation_delegate_email { get; set; }
        public string? skill_delegate_name { get; set; }
        public string? skill_delegate_mid { get; set; }
        public string? skill_delegate_email { get; set; }
    }

    /// <summary>
    /// UserInfoV4DTO
    /// </summary>
    public class UserInfoV4DTO
    {
        public int? id { get; set; }
        public string? role_ids { get; set; }
        public string? email_id { get; set; }
        public string? name { get; set; }
        public string? fname { get; set; }
        public string? lname { get; set; }
        public string? designation { get; set; }
        public string? service_line { get; set; }
        public object? roles { get; set; }
        public bool? status { get; set; }
        public bool? is_existing { get; set; }
        public string[]? role_list { get; set; }
        public string? uemail_id { get; set; }
        public string? competency { get; set; }
        public string? competencyId { get; set; }
        public string? supercoach_name { get; set; }
        public string? co_supercoach_name { get; set; }
        public string? supercoach_mid { get; set; }
        public string? co_supercoach_mid { get; set; }
        public string? employee_id { get; set; }
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
