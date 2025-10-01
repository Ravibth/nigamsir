using System.Collections.Generic;

namespace Gateway.API.Dtos
{
    /// <summary>
    /// ResourcePermissionMapping.
    /// </summary>
    public class ResourcePermissionMapping
    {
        /// <summary>
        /// Gets or sets Path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets HttpMethods.
        /// </summary>
        public List<string> Method { get; set; }

        /// <summary>
        /// Check Project Permission
        /// </summary>
        public bool? CheckProjectPermission { get; set; }

        /// <summary>
        /// Gets or sets AdditionalAllowedRoles.
        /// </summary>
        public List<string>? AdditionalAllowedRoles { get; set; }

        /// <summary>
        /// Gets or sets ModuleActionMapping.
        /// </summary>
        public List<ModuleActionMapping> ModuleActionMapping { get; set; }

    }

    /// <summary>
    /// ModuleActionMapping.
    /// </summary>
    public class ModuleActionMapping
    {
        /// <summary>
        /// Gets or sets Module.
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// Gets or sets Action.
        /// </summary>
        public string Action { get; set; }
    }
}
