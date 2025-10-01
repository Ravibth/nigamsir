// <copyright file="IUserAccessor.cs" company="RMT">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Gateway.API.Services
{
    using Gateway.API.Dtos;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// User Accessor interface.
    /// </summary>
    public interface IUserAccessor
    {
        /// <summary>
        /// Gets jwt token.
        /// </summary>
        /// <returns>Jwt token string.</returns>
        string GetToken();

        /// <summary>
        /// Gets Id of the loggedin user.
        /// </summary>
        /// <returns>User Id.</returns>
        string GetUserId();

        /// <summary>
        /// Gets email of the loggedin user.
        /// </summary>
        /// <returns>email Id.</returns>
        string GetEmail();

        /// <summary>
        /// Get the name of the loggedin user
        /// </summary>
        /// <returns></returns>
        string GetName();

        /// <summary>
        /// Get the Permissions of the loggedin user
        /// </summary>
        /// <returns></returns>
        List<ModulePermissionDTO> GetPermissions(HttpContext context, List<string> userRoles, ILogger _logger = null);

        /// <summary>
        /// Get the Roles of the loggedin user
        /// </summary>
        /// <returns></returns>
        List<string> GetRoles(HttpContext context);

    }
}
