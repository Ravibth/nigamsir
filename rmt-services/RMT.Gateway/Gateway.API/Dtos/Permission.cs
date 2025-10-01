namespace Gateway.API.Dtos
{
    /// <summary>
    /// Permissions.
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// Gets or sets BannerAccess.
        /// </summary>
        public string BannerAccess { get; set; }

        /// <summary>
        /// Gets or sets ModuleName.
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets PermissionName.
        /// </summary>
        public string PermissionName { get; set; }
    }
}
