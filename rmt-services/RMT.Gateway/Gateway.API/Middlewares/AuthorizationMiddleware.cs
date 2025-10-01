using Gateway.API.Dtos;
using Gateway.API.Helpers;
using Gateway.API.Helpers.HttpServices;
using Gateway.API.Helpers.IHttpServices;
using Gateway.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ocelot.Authorization;
using Ocelot.Middleware;
using Polly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using static Gateway.API.ServiceLayerHelper.Constants.Constants;

namespace Gateway.API.Middlewares
{
    /// <summary>
    /// AuthorizationMiddleware1
    /// </summary>
    public class AuthorizationMiddleware1 : OcelotPipelineConfiguration
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        private readonly List<ResourcePermissionMapping> resourcePermissionMapping;

        /// <summary>
        /// AuthorizationMiddleware1
        /// </summary>
        /// <param name="config"></param>
        /// <param name="resourcePermissionMapping"></param>
        /// <param name="logger"></param>
        public AuthorizationMiddleware1(IConfiguration config, List<ResourcePermissionMapping> resourcePermissionMapping, ILogger logger)
        {
            _config = config;
            _logger = logger;
            this.resourcePermissionMapping = resourcePermissionMapping;

            try
            {
                PreAuthorizationMiddleware = async (ctx, next) =>
                {
                    await InvokePreAuthorizationMiddleware(ctx, next);
                };

                AuthorizationMiddleware = async (ctx, next) =>
                {
                    await InvokeAuthorizationMiddleware(ctx, next);

                    //ctx.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate"; // HTTP 1.1.

                    //ctx.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
                    //ctx.Response.Headers.Append("Pragma", "no-cache"); // HTTP 1.0.
                    //ctx.Response.Headers.Append("Expires", "0"); // Proxies.

                    //ctx.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate"; // HTTP 1.1.
                    //ctx.Response.Headers.Pragma = "no-cache"; // HTTP 1.0.
                    //ctx.Response.Headers.Expires = "0"; // Proxies.

                    //ctx.Items.DownstreamResponse().Headers.Append(new Header("Cache-Control", new List<string>() { "no-cache, no-store, must-revalidate" })); // HTTP 1.1.
                    //ctx.Items.DownstreamResponse().Headers.Append(new Header("Pragma", new List<string>() { "no-cache" })); // HTTP 1.0.
                    //ctx.Items.DownstreamResponse().Headers.Append(new Header("Expires", new List<string>() { "0" })); // Proxies.
                    //ctx.Items.DownstreamResponse().Headers.Append(new Header("X-Content-Type-Options", new List<string>() { "nosniff" })); // Proxies.
                    //ctx.Items.DownstreamResponse().Headers.Append(new Header("Referrer-Policy", new List<string>() { "no-referrer-when-downgrade" })); // Proxies.
                    //ctx.Items.DownstreamResponse().Headers.Append(new Header("Content-Security-Policy", new List<string>() { "default-src 'self'; script-src 'self' 'unsafe-inline'" })); // Proxies.
                    //ctx.Items.DownstreamResponse().Headers.Append(new Header("Strict-Transport-Security", new List<string>() { "max-age=31536000; includeSubDomains" })); // Proxies.
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// InvokePreAuthorizationMiddleware
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokePreAuthorizationMiddleware(HttpContext context, System.Func<Task> next)
        {
            try
            {
                _logger.LogInformation("--InvokePreAuthorizationMiddleware------Invoke--Start");
                //_logger.LogInformation("InvokePreAuthorizationMiddleware");
                var userAccessor = context.RequestServices.GetService<IUserAccessor>();
                var identityHttpServices = context.RequestServices.GetService<IIdentityHttpServices>();
                var user = ((DefaultHttpContext)context)?.User;
                await EnrichClaim(context, user, userAccessor, identityHttpServices);
                if (context.Response.StatusCode != 401)
                {
                    _logger.LogInformation("--InvokePreAuthorizationMiddleware--Invoke--End");
                    await next.Invoke();
                    _logger.LogInformation("--InvokePreAuthorizationMiddleware--next Invoked");
                }
                else
                {
                    throw new UnauthorizedAccessException(Constants.InValidTokenMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "--InvokePreAuthorizationMiddleware--Invoke--Failed");
                if (ex.Message == Constants.UnAuthorized)
                {
                    throw new AuthenticationException(Constants.UnAuthorized);
                }
                throw;
            }
        }

        /// <summary>
        /// EnrichClaim
        /// </summary>
        /// <param name="context"></param>
        /// <param name="claims"></param>
        /// <param name="userAccessor"></param>
        /// <param name="identityHttpServices"></param>
        /// <returns></returns>
        private async Task EnrichClaim(HttpContext context, ClaimsPrincipal claims, IUserAccessor userAccessor, IIdentityHttpServices identityHttpServices)
        {
            var email = userAccessor.GetEmail();

            if (String.IsNullOrEmpty(email))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(Constants.InValidTokenMessage);
                return;
            }

            UserInfoDTO userInfo = await identityHttpServices.GetUserInfo(email);

            if (userInfo != null && string.Compare(userInfo.uemail_id?.Trim(), email?.Trim(), true) == 0)
            {
                //UserInfoV4DTO userV4Info = await identityHttpServices.GetUserV4Info(email);
                //List<ModulePermissionDTO> userModulePermissions = await identityHttpServices.GetUserModulePermissions(email);
                _logger.LogInformation("userInfo: {UserInfo}", JsonConvert.SerializeObject(userInfo).ToString());

                context.Items.DownstreamRequest().Headers.Add(Constants.UserInfoCustomHeader, JsonConvert.SerializeObject(userInfo).ToString());
                //context.Items.DownstreamRequest().Headers.Add(Constants.UserInfoV4CustomHeader, JsonConvert.SerializeObject(userV4Info).ToString());
                //context.Items.DownstreamRequest().Headers.Add(Constants.UserModulePermissionsCustomHeader, JsonConvert.SerializeObject(userModulePermissions).ToString());

                bool useSystemAdminPermissions = false;
                //Superadmin Related checks
                if (userInfo != null && userInfo.roles != null)
                {
                    useSystemAdminPermissions = userInfo.roles.Contains(Constants.SystemAdmin);
                }

                if (useSystemAdminPermissions != true)
                {
                    await GetProjectRoleWiseModulePermission(context, userInfo, identityHttpServices);
                }

                var listOfClaims = new List<Claim>
                {
                    new(Constants.UserRoleClaimKey, String.Join(",", userInfo.roles))
                };
                claims.AddIdentity(new ClaimsIdentity(listOfClaims));
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(Constants.InValidTokenMessage);
                return;
            }
        }

        /// <summary>
        /// GetProjectRoleWiseModulePermission
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userObject"></param>
        /// <param name="identityHttpServices"></param>
        /// <returns></returns>
        private async Task GetProjectRoleWiseModulePermission(HttpContext context, UserInfoDTO userObject, IIdentityHttpServices identityHttpServices)
        {
            bool checkProjectPermissionMappingFlag = Convert.ToBoolean(_config.GetSection("MicroserviceApiSettings").GetSection("CheckProjectPermissionMappingfromJson").Value);

            _logger.LogInformation($"--AuthorizationMiddleware1--GetProjectRoleWiseModulePermission--Start--CheckProjectPermissionMappingfromJson-{checkProjectPermissionMappingFlag}");

            string projectServicePrefix = string.Join("", "/", Constants.ProjectGatewayPath, Constants.ProjectSwitchCasePath).ToLower();

            string pipelineCodeKey = "pipelinecode";
            string jobCodeKey = "jobcode";

            try
            {
                var resourcePermissionMappingFromJson = this.GetPermissionForMatchedPath(context.Request.Path, context.Request.Method);

                bool allowAccess = false;

                if (
                        checkProjectPermissionMappingFlag == true
                        && resourcePermissionMappingFromJson != null
                        && resourcePermissionMappingFromJson.CheckProjectPermission == true
                        && resourcePermissionMappingFromJson.AdditionalAllowedRoles != null
                        && resourcePermissionMappingFromJson.AdditionalAllowedRoles.Count > 0
                )
                {
                    if (userObject.roles != null && userObject.roles.ToArray().Intersect(resourcePermissionMappingFromJson.AdditionalAllowedRoles, StringComparer.OrdinalIgnoreCase).Any())
                    {
                        string pipelineCodeValue, jobCodeValue;
                        getPipelineCodeAndJobCodeValues(context, pipelineCodeKey, jobCodeKey, out pipelineCodeValue, out jobCodeValue);
                        if (!string.IsNullOrEmpty(pipelineCodeValue))
                        {
                            var projectHttpServices = context.RequestServices.GetService<IProjectHttpService>();
                            var wcgtHttpServices = context.RequestServices.GetService<IWcgtHttpServices>();

                            jobCodeValue = (jobCodeValue.ToLower() == "null" || jobCodeValue.ToLower() == "undefined") ? null : jobCodeValue;

                            jobCodeValue = string.IsNullOrEmpty(jobCodeValue) ? null : jobCodeValue;

                            Task<ProjectInfoDTO> projectInfo = projectHttpServices.GetProjectDetailsByPipelineCode(pipelineCodeValue, jobCodeValue);
                            Task<GTBUExpertiesGroupDTO> gTBUExpertiesGroup = wcgtHttpServices.GetBUTreeMappingListByMID(userObject.employee_id);
                            Task<List<CompetencyMasterDTO>> competencyMasters = wcgtHttpServices.GetCompetencyMasterByMid(userObject.employee_id);
                            //compretency leader mid


                            await Task.WhenAll(projectInfo, gTBUExpertiesGroup, competencyMasters);

                            if (
                                gTBUExpertiesGroup.Result.BU.Values.Any(m => m.ToLower() == (!String.IsNullOrEmpty(projectInfo.Result.bu) ? projectInfo.Result.bu.ToLower() : ""))
                                || gTBUExpertiesGroup.Result.Offerings.Values.Any(m => m.ToLower() == (!String.IsNullOrEmpty(projectInfo.Result.Offerings) ? projectInfo.Result.Offerings.ToLower() : ""))//Recheck
                                || gTBUExpertiesGroup.Result.Solutions.Values.Any(m => m.ToLower() == (!String.IsNullOrEmpty(projectInfo.Result.Solutions) ? projectInfo.Result.Solutions.ToLower() : ""))//Recheck
                                || competencyMasters.Result.Count > 0 && projectInfo.Result.ProjectCompetencies.Any(x => competencyMasters.Result.Any(t => t.CompetencyId == x.CompetencyId))
                            )
                            {
                                allowAccess = true;
                            }
                        }
                    }
                }

                if (!allowAccess && checkProjectPermissionMappingFlag == true && resourcePermissionMappingFromJson != null && resourcePermissionMappingFromJson.CheckProjectPermission == true)
                //if (checkProjectPermissionMappingFlag)
                {
                    string pipelineCodeValue, jobCodeValue;
                    getPipelineCodeAndJobCodeValues(context, pipelineCodeKey, jobCodeKey, out pipelineCodeValue, out jobCodeValue);

                    _logger.LogInformation($"--AuthorizationMiddleware1--GetProjectRoleWiseModulePermission--Start-pipelineCodeValue-{pipelineCodeValue}--jobCodeValue-{jobCodeValue}");

                    //get project permission only when atleaset pipelinecode is found in request query or body
                    if (!string.IsNullOrEmpty(pipelineCodeValue))
                    {
                        var projectHttpServices = context.RequestServices.GetService<IProjectHttpService>();

                        jobCodeValue = (jobCodeValue.ToLower() == "null" || jobCodeValue.ToLower() == "undefined") ? null : jobCodeValue;

                        jobCodeValue = string.IsNullOrEmpty(jobCodeValue) ? null : jobCodeValue;

                        PipelineCodeAndRolesDto pipelineCodeRoleDto =
                            new PipelineCodeAndRolesDto() { pipelineCode = pipelineCodeValue, jobCode = jobCodeValue, roles = new List<string>() };

                        _logger.LogInformation($"--AuthorizationMiddleware1--GetProjectRoleWiseModulePermission--GetAllProjectRolesByCodes-Input--{JsonConvert.SerializeObject(pipelineCodeRoleDto)}");

                        List<ProjectRolesResponseDTO> projectRoleResponse = await projectHttpServices.GetAllProjectRolesByCodes(pipelineCodeRoleDto);
                        //List<RoleEmailsByPipelineCodeResponse> projectRoleResponse = await projectHttpServices.GetRolesEmailByPipelineCodesAndRoles(pipelineCodeRoleDto);
                        if (projectRoleResponse != null && projectRoleResponse.Count > 0)
                        {
                            _logger.LogInformation($"--AuthorizationMiddleware1--GetProjectRoleWiseModulePermission--GetAllProjectRolesByCodes-Response--{JsonConvert.SerializeObject(projectRoleResponse)}");

                            var validRoles = projectRoleResponse.Where(x => !string.IsNullOrEmpty(x.ApplicationRole) && x.User.ToLower().Trim() == userObject.email?.ToLower().Trim()).Select(s => s.ApplicationRole).Distinct().ToList();
                            //List<string> projectRoles = projectRoleResponse[0].RolesEmails.Keys.ToList<string>();
                            List<string> projectRoles = validRoles;

                            _logger.LogInformation($"--AuthorizationMiddleware1--GetProjectRoleWiseModulePermission--GetAllProjectRolesByCodes-ProjectRolesInput--{JsonConvert.SerializeObject(projectRoles)}");

                            List<ModulePermissionDTO> userProjectModulePermissions = new List<ModulePermissionDTO>();
                            if (projectRoles != null && projectRoles.Count > 0)
                            {
                                userProjectModulePermissions = await identityHttpServices.GetUserModulePermissionsByRole(projectRoles);
                                _logger.LogInformation($"--AuthorizationMiddleware1--GetProjectRoleWiseModulePermission--GetUserModulePermissionsByRole--UserRoles-{JsonConvert.SerializeObject(projectRoles)}--Response--{JsonConvert.SerializeObject(userProjectModulePermissions)}-Project--{JsonConvert.SerializeObject(pipelineCodeRoleDto)}-");
                            }
                            else
                            {
                                _logger.LogInformation($"--AuthorizationMiddleware1--GetProjectRoleWiseModulePermission--GetUserModulePermissionsByRole--UserRoles-{JsonConvert.SerializeObject(projectRoles)}--Response--No ProjectPermission-Project--{JsonConvert.SerializeObject(pipelineCodeRoleDto)}-");

                            }
                            context.Items.DownstreamRequest().Headers.Add(Constants.ProjectModulePermissionsCustomHeader, JsonConvert.SerializeObject(userProjectModulePermissions).ToString());

                            _logger.LogInformation($"--AuthorizationMiddleware1--GetProjectRoleWiseModulePermission--ProjectModulePermissionsCustomHeader-assigned-{JsonConvert.SerializeObject(userProjectModulePermissions)}");
                        }
                    }
                    _logger.LogInformation($"--AuthorizationMiddleware1--GetProjectRoleWiseModulePermission--End-Path-{context.Request.Path}--Method-{context.Request.Method}-pipelineCodeValue-{pipelineCodeValue}--jobCodeValue-{jobCodeValue}");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"--AuthorizationMiddleware1--GetProjectRoleWiseModulePermission--Exception-Path-{context.Request.Path}--Method-{context.Request.Method}");
                throw;
            }
        }

        public void getPipelineCodeAndJobCodeValues(HttpContext context, string pipelineCodeKey, string jobCodeKey, out string pipelineCodeValue, out string jobCodeValue)
        {
            pipelineCodeValue = string.Empty;
            jobCodeValue = string.Empty;
            var tempReq = context.Items.DownstreamRequest();

            _logger.LogInformation($"--AuthorizationMiddleware1--GetProjectRoleWiseModulePermission--Start-Path-{context.Request.Path}--Method-{context.Request.Method}");

            if (context.Request.Method == HttpMethods.Get)
            {
                var queryStringKeys = context.Request.Query.Keys;

                var kPC = queryStringKeys.Where(x => x.Replace("_", "").ToLower() == pipelineCodeKey).FirstOrDefault();
                if (kPC != null && kPC.Length > 0)
                {
                    pipelineCodeValue = context.Request.Query[kPC];
                }
                var kJC = queryStringKeys.Where(x => x.Replace("_", "").ToLower() == jobCodeKey).FirstOrDefault();
                if (kJC != null && kJC.Length > 0)
                {
                    jobCodeValue = context.Request.Query[kJC];
                }
            }
            else if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put || context.Request.Method == HttpMethods.Patch)
            {
                var request_body = context.Items.DownstreamRequest().Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(request_body))
                {
                    Dictionary<string, string> jsonTokens = new Dictionary<string, string>();

                    jsonTokens = FillToken(request_body, jsonTokens);
                    var kPC = jsonTokens.Keys.Where(x => x.Replace("_", "").ToLower() == pipelineCodeKey).FirstOrDefault();
                    if (kPC != null && kPC.Length > 0)
                    {
                        pipelineCodeValue = jsonTokens[kPC];
                    }
                    var kJC = jsonTokens.Keys.Where(x => x.Replace("_", "").ToLower() == jobCodeKey).FirstOrDefault();
                    if (kJC != null && kJC.Length > 0)
                    {
                        jobCodeValue = jsonTokens[kJC];
                    }
                }
            }
        }

        /// <summary>
        /// FillToken
        /// </summary>
        /// <param name="meta"></param>
        /// <param name="DTokens"></param>
        /// <returns></returns>
        public Dictionary<string, string> FillToken(string meta, Dictionary<string, string> DTokens)
        {
            if (Utility.CheckIsValidJsonToken(meta))
            {
                var jToken = JToken.Parse(meta);
                JObject obj = new JObject();
                if (jToken is JArray)
                {
                    JArray jsonArray = JArray.Parse(meta);
                    string jsonStr = Convert.ToString(jsonArray[0]);
                    if (Utility.CheckIsValidJsonObject(jsonStr))
                    {
                        obj = JObject.Parse(jsonStr);
                    }
                }
                else if (jToken is JObject)
                {
                    obj = JObject.Parse(meta);
                }
                //obj = JObject.Parse(meta);
                foreach (var property in obj.Properties())
                {
                    if (!DTokens.ContainsKey(property.Name.ToLower().Trim()))
                    {
                        DTokens.Add(property.Name.ToLower().Trim(), Convert.ToString(property.Value));
                    }
                }
            }
            return DTokens;
        }

        /// <summary>
        /// InvokeAuthorizationMiddleware
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAuthorizationMiddleware(HttpContext context, System.Func<Task> next)
        {
            try
            {
                _logger.LogInformation("--InvokeAuthorizationMiddleware------Invoke--Start");

                _logger.LogInformation("--InvokeAuthorizationMiddleware--RouteClaimsRequirement--Start");

                var claimsRequirement = context.Items.DownstreamRoute().RouteClaimsRequirement.Where(m => m.Key.ToLower().Equals(Constants.UserRoleClaimKey.ToLower())).Select(m => m.Value).ToList();
                var userRoles = context.User.Claims.Where(m => m.Type.ToLower().Equals(Constants.UserRoleClaimKey.ToLower())).ToList();

                var rolesAccessRequirement = new List<string>();
                foreach (var item in claimsRequirement)
                {
                    var rolesSplit = item.Split(',');
                    foreach (var role in rolesSplit)
                    {
                        rolesAccessRequirement.Add(role);
                    }

                }
                var userRolesList = new List<string>();

                foreach (var item in userRoles)
                {
                    var rolesSplit = item.Value.Split(',');
                    foreach (var role in rolesSplit)
                    {
                        userRolesList.Add(role);
                    }
                }

                if (claimsRequirement.Count > 0)
                {
                    var count = 0;
                    foreach (var role in userRolesList)
                    {
                        if (rolesAccessRequirement.Any(m => m.Trim().ToLower().Equals(role.Trim().ToLower())))
                        {
                            count++;
                            break;
                        }
                    }
                    if (count == 0)
                    {
                        _logger.LogInformation("--InvokeAuthorizationMiddleware--Authorization Failed Custom");
                        context.Items.SetError(new UnauthorizedError("Authorization Failed Custom"));
                    }
                }
                _logger.LogInformation("--InvokeAuthorizationMiddleware--RouteClaimsRequirement--End");

                HttpStatusCode statusCode = InvokeRolePermissionMiddleware(context);

                _logger.LogInformation($"--InvokeRolePermissionMiddleware--ResponseStatusCode {statusCode}");

                if (statusCode == HttpStatusCode.OK)
                {
                    _logger.LogInformation("--InvokeAuthorizationMiddleware--next Invoked-Start");
                    await next.Invoke();
                    _logger.LogInformation("--InvokeAuthorizationMiddleware--next Invoked-End");
                }
                else
                {
                    _logger.LogInformation($"--InvokeAuthorizationMiddleware--next will not Invoked, as RolePermission not allowed");
                    return;
                }

                _logger.LogInformation("--InvokeAuthorizationMiddleware------Invoke--End");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "--InvokeAuthorizationMiddleware--Invoke--Failed");
                //throw;
            }

        }

        /// <summary>
        /// Invoke RolePermissionMiddleware
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public HttpStatusCode InvokeRolePermissionMiddleware(HttpContext context)
        {
            HttpStatusCode statusCode;
            try
            {
                statusCode = HttpStatusCode.OK;

                _logger.LogInformation($"--InvokeRolePermissionMiddleware--Invoke--Start--");
                _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--Start--");

                var userAccessor = context.RequestServices.GetService<IUserAccessor>();

                string userToken = userAccessor.GetToken();

                if (!string.IsNullOrEmpty(userToken))
                {
                    bool checkPermissionMapping = Convert.ToBoolean(_config.GetSection("MicroserviceApiSettings").GetSection("CheckPermissionMappingFromJson").Value);

                    _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--Request.Path-{context.Request.Path}--CheckPermissionMappingFromJson-{checkPermissionMapping}--");

                    if (checkPermissionMapping)
                    {
                        var resourcePermissionMappingFromJson = this.GetPermissionForMatchedPath(context.Request.Path, context.Request.Method);

                        if (resourcePermissionMappingFromJson == null)
                        {
                            _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--ResourcePermissionMappingFromJson not found--");

                            this.ThrowUnAuthorizedError(context, "ResourcePermissionMappingFromJson not found");
                            statusCode = HttpStatusCode.Forbidden;
                            return statusCode;

                            //context.Response.StatusCode = 404; // Not Found.
                            //await context.Response.WriteAsync(Constants.NotFound);
                            //throw new AuthenticationException(Constants.NotFound);
                        }
                        else//resourcePermissionMappingFromJson is not null
                        {
                            if (!resourcePermissionMappingFromJson.ModuleActionMapping.Select(x => x.Module).Contains(Constants.Anonymous))
                            {
                                if (string.IsNullOrEmpty(userToken))
                                {
                                    _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--User Token is null or empty--");

                                    this.ThrowUnAuthorizedError(context, "User Token is null");
                                    statusCode = HttpStatusCode.Forbidden;
                                    return statusCode;

                                    //context.Response.StatusCode = 401; // UnAuthorized.
                                    //await context.Response.WriteAsync(Constants.UnAuthorized);
                                    //throw new AuthenticationException(Constants.UnAuthorized);
                                }
                            }
                        }

                        //var _headers = context.Items.DownstreamRequest().Headers;

                        var userRoles = userAccessor.GetRoles(context);

                        bool mappingWithAllAction = resourcePermissionMappingFromJson.ModuleActionMapping.Select(x => x.Action).Contains(Constants.All);

                        if (!(mappingWithAllAction || userRoles.Contains(Constants.SystemAdmin) || userRoles.Contains(Constants.Admin) || userRoles.Contains(Constants.CEOCOO)))
                        {
                            _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--Checking Api permission--");

                            var userPermissionList = userAccessor.GetPermissions(context, userRoles, _logger);

                            _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--userPermissionList--{JsonConvert.SerializeObject(userPermissionList)}");

                            _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--requiredPermissionList--{JsonConvert.SerializeObject(resourcePermissionMappingFromJson)}");

                            var userPermissionAssignedList = userPermissionList.Where(x => x.is_assigned == true).ToList();
                            bool permissionAllowed = userPermissionAssignedList.Any(
                                userPermissions =>
                                resourcePermissionMappingFromJson.ModuleActionMapping.Any(
                                    permissionRequired => permissionRequired.Module == Constants.Anonymous ||
                                        (
                                            permissionRequired.Module == userPermissions.module_name &&
                                            (
                                                (permissionRequired.Action == Constants.PermissionLevelValues.read && userPermissions.permissions.read) ||
                                                (permissionRequired.Action == Constants.PermissionLevelValues.update && userPermissions.permissions.update) ||
                                                (permissionRequired.Action == Constants.PermissionLevelValues.create && userPermissions.permissions.create) ||
                                                (permissionRequired.Action == Constants.PermissionLevelValues.delete && userPermissions.permissions.delete) ||
                                                (permissionRequired.Action == Constants.PermissionLevelValues.approve && userPermissions.permissions.approve)
                                            )
                                        )
                                    )
                                );

                            _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--{context.Request.Path}--PermissionAllowed--{permissionAllowed}");

                            if (!permissionAllowed)
                            {
                                _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--Permission not allowed in Json--");

                                this.ThrowUnAuthorizedError(context, "Permission not allowed in Json");
                                statusCode = HttpStatusCode.Forbidden;
                                return statusCode;

                                //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                //await context.Response.WriteAsync(Constants.UnAuthorized);//throw new AuthenticationException(Constants.UnAuthorized);
                            }
                        }
                    }

                    _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--End--");
                }
                else
                {
                    statusCode = HttpStatusCode.Unauthorized;
                    _logger.LogInformation($"--RoutePermissionMiddleware--Invoke--UserAuthorization Token is null or empty--");
                }

                return statusCode;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "--InvokeRolePermissionMiddleware--Invoke--Failed");
                statusCode = HttpStatusCode.InternalServerError;
                return statusCode;
            }

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
