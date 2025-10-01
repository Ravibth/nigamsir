using Gateway.API.Dtos;
using Gateway.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ocelot.Authorization;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.Middlewares
{
    /// <summary>
    /// RoutePermissionMiddleware class for authotrization check 
    /// </summary>
    public class RoutePermissionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly List<ResourcePermissionMapping> resourcePermissionMapping;

        private readonly IConfiguration _config;
        private ILogger<RoutePermissionMiddleware> _logger;

        /// <summary>
        /// RoutePermissionMiddleware constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="resourcePermissionMapping"></param>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        public RoutePermissionMiddleware(RequestDelegate next, List<ResourcePermissionMapping> resourcePermissionMapping, IConfiguration config, ILogger<RoutePermissionMiddleware> logger)
        {
            this._config = config;
            this._logger = logger;
            this.next = next;
            this.resourcePermissionMapping = resourcePermissionMapping;
        }

        /// <summary>
        /// Invoke Method
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userAccessor"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, IUserAccessor userAccessor)
        {
            _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--Start--");

            bool checkPermissionMapping = Convert.ToBoolean(_config.GetSection("MicroserviceApiSettings").GetSection("CheckPermissionMappingFromJson").Value);

            _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--Request.Path-{context.Request.Path}--CheckPermissionMappingFromJson-{checkPermissionMapping}--");

            if (checkPermissionMapping)
            {
                var resourcePermissionMappingFromJson = this.GetPermissionForMatchedPath(context.Request.Path, context.Request.Method);

                if (resourcePermissionMappingFromJson == null)
                {
                    _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--ResourcePermissionMappingFromJson not found--");

                    this.ThrowUnAuthorizedError(context, "ResourcePermissionMappingFromJson not found");
                    return;

                    //context.Response.StatusCode = 404; // Not Found.
                    //await context.Response.WriteAsync(Constants.NotFound);
                    //throw new AuthenticationException(Constants.NotFound);
                }
                else//resourcePermissionMappingFromJson is not null
                {
                    if (!resourcePermissionMappingFromJson.ModuleActionMapping.Select(x => x.Module).Contains(Constants.Anonymous))
                    {
                        if (userAccessor.GetToken() == null)
                        {
                            _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--User Token is null--");

                            this.ThrowUnAuthorizedError(context, "User Token is null");
                            return;

                            //context.Response.StatusCode = 401; // UnAuthorized.
                            //await context.Response.WriteAsync(Constants.UnAuthorized);
                            //throw new AuthenticationException(Constants.UnAuthorized);
                        }
                    }
                }

                var _headers = context.Items.DownstreamRequest().Headers;

                var userRoles = userAccessor.GetRoles(context);

                if (!(resourcePermissionMappingFromJson.ModuleActionMapping.Select(x => x.Action).Contains(Constants.All)
                    || userRoles.Contains(Constants.SystemAdmin)))
                {
                    _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--Checking Api permission--");

                    var userPermissionList = userAccessor.GetPermissions(context);
                    var userPermissionAssignedList = userPermissionList.Where(x => x.is_assigned == true).ToList();
                    bool permissionAllowed = userPermissionAssignedList.Any(
                        userPermissions =>
                        userPermissions.is_assigned == true &&
                        resourcePermissionMappingFromJson.ModuleActionMapping.Any(
                            permissionRequired => permissionRequired.Module.ToLower() == Constants.Anonymous.ToLower() ||
                                (
                                    permissionRequired.Module.ToLower() == userPermissions.module_name.ToLower() &&
                                    (
                                        permissionRequired.Action.ToLower() == Constants.PermissionLevelValues.read.ToLower() && userPermissions.permissions.read ||
                                        permissionRequired.Action.ToLower() == Constants.PermissionLevelValues.update.ToLower() && userPermissions.permissions.update ||
                                        permissionRequired.Action.ToLower() == Constants.PermissionLevelValues.create.ToLower() && userPermissions.permissions.create ||
                                        permissionRequired.Action.ToLower() == Constants.PermissionLevelValues.delete.ToLower() && userPermissions.permissions.delete ||
                                        permissionRequired.Action.ToLower() == Constants.PermissionLevelValues.approve.ToLower() && userPermissions.permissions.approve
                                    )
                                )
                            )
                        );

                    _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--PermissionAllowed--{permissionAllowed}");

                    if (!permissionAllowed)
                    {
                        _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--Permission not allowed in Json--");

                        this.ThrowUnAuthorizedError(context, "Permission not allowed in Json");
                        return;

                        //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        //await context.Response.WriteAsync(Constants.UnAuthorized);//throw new AuthenticationException(Constants.UnAuthorized);
                    }
                }
            }

            _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--End--");

            await this.next(context);

        }

        /// <summary>
        /// ThrowUnAuthorizedError
        /// </summary>
        /// <param name="context"></param>
        /// <param name="msg"></param>
        private void ThrowUnAuthorizedError(HttpContext context, string msg)
        {
            _logger.LogInformation($"--RoutePermissionMiddleware--ThrowUnAuthorizedError--Api access not allowed--");
            context.Items.SetError(new UnauthorizedError($"Authorization Failed - {msg}"));
        }

        /// <summary>
        /// GetPermissionForMatchedPath
        /// </summary>
        /// <param name="requestPath"></param>
        /// <param name="requestMethod"></param>
        /// <returns></returns>
        private ResourcePermissionMapping GetPermissionForMatchedPath(string requestPath, string requestMethod)
        {
            ResourcePermissionMapping matchedPermission = null;

            var queryStringStartIndex = requestPath.IndexOf(Constants.SeperatorQuestion);
            if (queryStringStartIndex != -1)
            {
                requestPath = requestPath.Substring(0, queryStringStartIndex);
            }

            string startsWithPath = string.Empty;

            var backSlashLastIndex = requestPath.LastIndexOf(Constants.SeperatorBackSlash);
            if (backSlashLastIndex != -1)
            {
                startsWithPath = requestPath.Substring(0, backSlashLastIndex);
            }

            var requestPathArr = requestPath.Split(Constants.SeperatorBackSlash);
            requestPathArr = requestPathArr.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            List<ResourcePermissionMapping> matchedMappings = this.resourcePermissionMapping;

            matchedMappings = matchedMappings.Where(x => x.Method.Any(m => m.ToLower() == requestMethod.ToLower())).ToList();

            matchedMappings = matchedMappings.Where(x => x.Path.StartsWith(startsWithPath, StringComparison.CurrentCultureIgnoreCase)).ToList();

            foreach (var permission in matchedMappings)
            {
                bool pathMatchFound = false;

                var permissionPath = permission.Path;
                var permissionPathArr = permission.Path.Split(Constants.SeperatorBackSlash);
                permissionPathArr = permissionPathArr.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

                if (permissionPathArr.Length == requestPathArr.Length)
                {
                    pathMatchFound = true;
                    for (int i = 0; i < permissionPathArr.Length; i++)
                    {
                        if (permissionPathArr[i] != Constants.CatchAll && permissionPathArr[i].ToLower() != requestPathArr[i].ToLower())
                        {
                            pathMatchFound = false;
                            break;
                        }
                    }

                    if (pathMatchFound)
                    {
                        matchedPermission = permission;
                        break;
                    }
                }
            }

            return matchedPermission;
        }
    }
}