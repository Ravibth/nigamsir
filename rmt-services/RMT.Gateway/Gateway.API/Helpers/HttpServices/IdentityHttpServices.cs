using Azure.Core;
using Gateway.API.Dtos;
using Gateway.API.Helpers.IHttpServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ocelot.Requester;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.API.Helpers.HttpServices
{
    /// <summary>
    /// IdentityHttpServices
    /// </summary>
    public class IdentityHttpServices : IIdentityHttpServices
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly ILogger<IdentityHttpServices> _logger;

        /// <summary>
        /// IdentityHttpServices constructor
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        public IdentityHttpServices(HttpClient httpClient, IConfiguration config, ILogger<IdentityHttpServices> logger)
        {
            _httpClient = httpClient;
            _config = config;
            _logger = logger;
        }

        /// <summary>
        /// GetUserInfo
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserInfoDTO> GetUserInfo(string email)
        {
            try
            {
                string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityService").Value;
                string getUserInfoFromIdentity = _config.GetSection("MicroserviceApiSettings").GetSection("getUserInfoWithPermission").Value;

                _logger.LogInformation("---------------------------------------------------------");
                _logger.LogInformation(baseurl + getUserInfoFromIdentity + $"?email_id={email}");
                var content = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");
                //var apiResponse = await _httpClient.PostAsync(baseurl + getEmployeePreferenceDetailsByEmailsPath, content);

                //HttpClient _httpClient = new();

                var apiResponse = await _httpClient.GetAsync(baseurl + getUserInfoFromIdentity + $"?email_id={email}");

                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    UserInfoDTO finalResponse = JsonConvert.DeserializeObject<UserInfoDTO>(resp);
                    return finalResponse;
                }
                else
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation("Response:-" + resp);
                    throw new Exception("Error fetching GetUserInfo" + resp);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in GetUserInfo");
                throw;
            }
        }

        public async Task<SupercoachDelegate> GetSupercoachDelegateByUserMid(string supercoach_mid)
        {
            try
            {
                string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityService").Value;
                string path = _config.GetSection("MicroserviceApiSettings").GetSection("getSuperCoachAndDelegateBySupercoachMid").Value;

                _logger.LogInformation("---------------------------------------------------------");
                _logger.LogInformation(baseurl + path + $"?supercoach_mid={supercoach_mid}");
                HttpClient _httpClient = new();

                var apiResponse = await _httpClient.GetAsync(baseurl + path + $"?supercoach_mid={supercoach_mid}");

                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(resp))
                    {
                        return null;
                    }
                    SupercoachDelegate finalResponse = JsonConvert.DeserializeObject<SupercoachDelegate>(resp);
                    return finalResponse;
                }
                else
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation("Response:-" + resp);
                    throw new Exception("Error fetching GetUserInfo" + resp);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Exception in GetSupercoachDelegateByUserMid");
                throw;
            }
        }

        /// <summary>
        /// GetUserV4Info
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserInfoV4DTO> GetUserV4Info(string email)
        {
            try
            {
                string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityService").Value;
                string apiPath = _config.GetSection("MicroserviceApiSettings").GetSection("getUserInfoV4FromIdentity").Value + email;
                //string apiPath = "identity/user/v4/" + email;
                //_config.GetSection("MicroserviceApiSettings").GetSection("getUserInfoFromIdentity").Value;

                _logger.LogInformation(baseurl + apiPath + $"");
                HttpClient _httpClient = new();
                var apiResponse = await _httpClient.GetAsync(baseurl + apiPath + $"");

                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    UserInfoV4DTO finalResponse = JsonConvert.DeserializeObject<UserInfoV4DTO>(resp);
                    return finalResponse;
                }
                else
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation("Response:-" + resp);
                    throw new Exception("Error fetching GetUserV4Info" + resp);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in GetUserV4Info");
                throw;
            }
        }


        /// <summary>
        /// GetUserModulePermissions
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<List<ModulePermissionDTO>> GetUserModulePermissions(string email)
        {
            try
            {
                //gateway/identity/modulePermission/role?emailId=
                string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityService").Value;
                string apiPath = _config.GetSection("MicroserviceApiSettings").GetSection("GetModulePermissionsByEmail").Value + email;
                //string apiPath = "identity/modulePermission/role?emailId=" + email;
                //_config.GetSection("MicroserviceApiSettings").GetSection("getUserInfoFromIdentity").Value;

                _logger.LogInformation(baseurl + apiPath + $"");
                HttpClient _httpClient = new();
                var apiResponse = await _httpClient.GetAsync(baseurl + apiPath + $"");

                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    List<ModulePermissionDTO> finalResponse = JsonConvert.DeserializeObject<List<ModulePermissionDTO>>(resp);
                    return finalResponse;
                }
                else
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation("Response:-" + resp);
                    throw new Exception("Error fetching GetUserModulePermissions" + resp);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in GetUserModulePermissions");
                throw;
            }
        }



        /// <summary>
        /// GetUserModulePermissionsByRole
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public async Task<List<ModulePermissionDTO>> GetUserModulePermissionsByRole(List<string> roles)
        {
            try
            {
                //gateway/identity/modulePermission/role?emailId=
                string roleStr = string.Join(",", roles);
                string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityService").Value;
                string apiPath = _config.GetSection("MicroserviceApiSettings").GetSection("GetModulePermissionsByRoleNames").Value + roleStr;
                //string apiPath = "identity/modulePermission/role?roleName=" + roleStr;

                //_config.GetSection("MicroserviceApiSettings").GetSection("getUserInfoFromIdentity").Value;

                _logger.LogInformation(baseurl + apiPath + $"");
                HttpClient _httpClient = new();
                var apiResponse = await _httpClient.GetAsync(baseurl + apiPath + $"");

                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    List<ModulePermissionDTO> finalResponse = JsonConvert.DeserializeObject<List<ModulePermissionDTO>>(resp);
                    return finalResponse;
                }
                else
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation("Response:-" + resp);
                    throw new Exception("Error fetching GetUserModulePermissions" + resp);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in GetUserModulePermissions");
                throw;
            }
        }



    }
}
