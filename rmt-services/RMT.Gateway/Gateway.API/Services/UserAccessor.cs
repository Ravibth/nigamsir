// <copyright file="UserAccessor.cs" company="RMT">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Gateway.API.Services
{
    using Gateway.API.Dtos;
    using Kros.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Ocelot.Middleware;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security;
    using System.Security.Authentication;

    /// <summary>
    /// User Accessor.
    /// </summary>
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccessor"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">Http Context Accessor.</param>
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;

            var stream = this.GetToken();
            if (stream != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var tokenValue = handler.ReadToken(stream) as JwtSecurityToken;
                this.Headers = new Dictionary<string, string>();
                foreach (var item in tokenValue.Payload)
                {
                    string value = Convert.ToString(item.Value);
                    this.Headers.Add(item.Key, value);
                }
                var customSystemClaim = GetCustomSystemClaimHeader();
                if (customSystemClaim != null)
                {
                    this.Headers.Add(Constants.ICustomHeaderSystem, customSystemClaim);
                }
                if (this.Headers == null)
                {
                    throw new AuthenticationException("Un-Authorized");
                }
            }
        }

        /// <summary>
        /// Gets jwt token.
        /// </summary>
        /// <returns>jwt token string.</returns>
        public string GetToken()
        {
            string token = null;
            string authHeader = this.httpContextAccessor.HttpContext.Request.Headers[Constants.Authorization];
            if (authHeader != null)
            {
                token = authHeader.Split(Constants.SeperatorWhiteSpace)[1];
            }

            return token;
        }

        /// <summary>
        /// Get Custom Claim added only for system access like in scheduler
        /// </summary>
        /// <returns></returns>
        public string GetCustomSystemClaimHeader()
        {
            string customSystemClaim = this.httpContextAccessor.HttpContext.Request.Headers[Constants.ICustomHeaderSystem];
            return customSystemClaim;
        }

        /// <summary>
        /// Gets Id of the loggedin user.
        /// </summary>
        /// <returns>User Id.</returns>
        public string GetUserId()
        {
            var token = this.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(this.GetToken());
            return jwtSecurityToken.Claims.First(claim => claim.Type == Constants.ClaimObjectId).Value;
        }

        /// <summary>
        /// Gets email of the loggedin user.
        /// </summary>
        /// <returns>Email Id.</returns>
        public string GetEmail()
        {
            if (this.Headers != null)
            {
                if (this.Headers.ContainsKey(Constants.ClaimPreferredUsername))
                {
                    return this.Headers[Constants.ClaimPreferredUsername];
                }
                else if (this.Headers.ContainsKey(Constants.ClaimUniqueUsername))
                {
                    return this.Headers[Constants.ClaimUniqueUsername];

                }
                else if (this.Headers.ContainsKey(Constants.ICustomHeaderSystem))
                {
                    return this.Headers[Constants.ICustomHeaderSystem];
                }
                else if (this.Headers.ContainsKey(Constants.ClaimApplicationClientId))
                {
                    return string.Format("{0}{1}", this.Headers[Constants.ClaimApplicationClientId], "@rms.com");
                }
                else if (this.Headers.ContainsKey(Constants.ClaimPreferredAppname))
                {
                    return string.Format("{0}{1}", this.Headers[Constants.ClaimPreferredAppname], "@rms.com");
                }
                else
                {
                    return string.Empty;//
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// Get Name of user
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            if (this.Headers != null)
            {
                if (this.Headers.ContainsKey(Constants.ClaimName))
                {
                    return this.Headers[Constants.ClaimName];
                }
                else if (this.Headers.ContainsKey(Constants.ClaimApplicationClientId))
                {
                    return this.Headers[Constants.ClaimApplicationClientId];
                }
                else if (this.Headers.ContainsKey(Constants.ClaimPreferredAppname))
                {
                    return this.Headers[Constants.ClaimPreferredAppname];
                }
                else
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets Permissions of the loggedin user.
        /// </summary>
        /// <returns>List of permissions.</returns>
        public List<ModulePermissionDTO> GetPermissions(HttpContext context, List<string> userRoles, ILogger _logger = null)
        {
            List<ModulePermissionDTO> response;
            IEnumerable<string> jsonStr = null;

            bool useSystemAdminPermissions = false;
            //Superadmin Related checks 
            useSystemAdminPermissions = userRoles.Contains(Constants.SystemAdmin);

            if (useSystemAdminPermissions != true)
            {
                //If Project wise permission exist use same anot Application wise permission
                bool flagProjectPermission = context.Items.DownstreamRequest().Headers.TryGetValues(Constants.ProjectModulePermissionsCustomHeader, out jsonStr);

                if (_logger != null)
                {
                    _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--ProjectPermission-{flagProjectPermission}--ProjectModulePermissionsCustomHeader--{JsonConvert.SerializeObject(jsonStr)}");
                }
                if (flagProjectPermission && jsonStr != null && jsonStr.Any() && !string.IsNullOrWhiteSpace(jsonStr.FirstOrDefault()))
                {
                    response = JsonConvert.DeserializeObject<List<ModulePermissionDTO>>(jsonStr.FirstOrDefault());
                    return response;
                }
            }

            jsonStr = null;
            //If Project wise permission does not exist use Application wise permission
            bool flagUserPermission = context.Items.DownstreamRequest().Headers.TryGetValues(Constants.UserInfoCustomHeader, out jsonStr);

            if (_logger != null)
            {
                _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--UserPermission-{flagUserPermission}--UserInfoCustomHeader--{JsonConvert.SerializeObject(jsonStr)}");
            }

            if (flagUserPermission && jsonStr != null && jsonStr.Any() && !string.IsNullOrWhiteSpace(jsonStr.FirstOrDefault()))
            {
                UserInfoDTO userInfoTemp = JsonConvert.DeserializeObject<UserInfoDTO>(jsonStr.FirstOrDefault());
                response = userInfoTemp?.app_permissions;
                return response;
            }

            return null;
        }

        /// <summary>
        /// Gets roles of the loggedin user.
        /// </summary>
        /// <returns>List of roles.</returns>
        public List<string> GetRoles(HttpContext context)
        {
            var roles = new List<string>();
            if (this.Headers != null)
            {
                IEnumerable<string> jsonStr;

                bool flag = context.Items.DownstreamRequest().Headers.TryGetValues(Constants.UserInfoCustomHeader, out jsonStr);
                if (flag && jsonStr != null && jsonStr.Any() && !string.IsNullOrWhiteSpace(jsonStr.FirstOrDefault()))
                {
                    var obj = JsonConvert.DeserializeObject<UserInfoDTO>(jsonStr.FirstOrDefault());
                    if (obj.roles != null)
                    {
                        roles = obj.roles.ToList();
                    }
                }

            }
            return roles;
        }

    }
}
