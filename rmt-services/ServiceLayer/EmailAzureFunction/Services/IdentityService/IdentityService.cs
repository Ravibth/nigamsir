using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.DTOs;
using ServiceLayer.Services.AllocationService.DTOs;
using ServiceLayer.Services.ConfigurationService;
using ServiceLayer.Services.ProjectService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static ServiceLayer.Constants.Constants;

namespace ServiceLayer.Services.IdentityService
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<NotificationSubscription> _logger;

        public IdentityService(ILogger<NotificationSubscription> logger)
        {
            _httpClient = AzureHttpClient.GetAzureHttpClient(true); ;
            _logger = logger;
        }

        public async Task<List<EmployeeInfoDTO>> GetEmployeeDetailsByEmails(List<string> emails, string token)
        {
            try
            {
                var environmentVariables = Environment.GetEnvironmentVariables();
                var baseurl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
                var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.GET_EMPLOYEES_INFO_BY_EMAIL]);
                //TODO call api
                //var baseurl = Environment.GetEnvironmentVariable("BaseGatewayUrl");
                //var path = Environment.GetEnvironmentVariable("GetEmployeesInfoByEmail");

                Dictionary<string, dynamic> queries = new()
                {
                    { "email_id", emails }
                };
                var url = Helper.UrlBuilderByQuery($"{baseurl}{path}", queries);
                var tokenSplit = token.Trim().Split(" ");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, tokenSplit[1]);

                _logger.LogInformation($"GetEmployeeDetailsByEmails-{url}");

                var apiResponse = await _httpClient.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation($"GetEmployeeDetailsByEmails-{url},-Code-{apiResponse.StatusCode},-resp-{resp}");
                    return JsonConvert.DeserializeObject<List<EmployeeInfoDTO>>(resp);
                }
                else
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation($"GetEmployeeDetailsByEmails-{url},-Code-{apiResponse.StatusCode},-resp-{resp}");
                    throw new Exception("Error fetching GetEmployeeDetailsByEmails");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetEmployeeDetailsByEmails Exception");
                throw;
            }
        }

        public async Task<UserInfoDTO> GetUserInfo(string email)
        {
            try
            {
                var environmentVariables = Environment.GetEnvironmentVariables();
                var baseurl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
                var getUserInfoFromIdentity = Convert.ToString(environmentVariables["getUserInfoFromIdentity"]);

                //_logger.LogInformation("---------------------------------------------------------");
                //_logger.LogInformation(baseurl + getUserInfoFromIdentity + $"?email_id={email}");
                var content = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");
                //var apiResponse = await _httpClient.PostAsync(baseurl + getEmployeePreferenceDetailsByEmailsPath, content);

                string url = baseurl + getUserInfoFromIdentity + $"?email_id={email}";

                _logger.LogInformation($"GetUserInfo-{url}");

                var apiResponse = await _httpClient.GetAsync(url);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation($"GetUserInfo-{url},-Code-{apiResponse.StatusCode},-resp-{resp}");
                    UserInfoDTO finalResponse = JsonConvert.DeserializeObject<UserInfoDTO>(resp);
                    return finalResponse;
                }
                else
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation($"GetUserInfo-{url},-Code-{apiResponse.StatusCode},-resp-{resp}");
                    throw new Exception("Error fetching GetUserInfo");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserInfo Exception");
                throw;
            }
        }
    }
}
