using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.DTOs;
using ServiceLayer.DTOs.Budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static ServiceLayer.Constants.Constants;

namespace ServiceLayer.Services.ConfigurationService
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ITokenService _tokenService;

        public ConfigurationService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Dynamically replace the payload items in template
        /// </summary>
        /// <param name="template"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public string TransformMessageTemplateAccordingToPayloads(string template, List<NotificationPlaceHolderDTO> payloadKeysRequired, Dictionary<string, string> keyValuePairs)
        {
            var tempTemplate = template;
            foreach (var item in payloadKeysRequired)
            {
                var keyValue = keyValuePairs.Where((m) => m.Key.Trim().ToLower().Equals(item.name.Trim().ToLower())).FirstOrDefault().Value;
                // todo uncomment later
                //if (item.is_required != null && item.is_required == true && String.IsNullOrEmpty(keyValue))
                //{
                //    throw new Exception($"InAdequate Details, {item.name} not found");
                //}
                tempTemplate = tempTemplate.Replace($"<{item.name}>", keyValue != null ? keyValue : "");
            }
            tempTemplate = tempTemplate.Replace("\n", "<br/>");
            return tempTemplate;
        }

        public async Task<List<NotificationTemplateDTO>> GetNotificationTemplate(string[] type, string token)
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            var baseUrl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.GET_NOTIFICATION_TEMPLATE]);
            //var objValue = JsonConvert.DeserializeObject<PostNewPushNotificationDTO>(payload);
            var content = new StringContent(JsonConvert.SerializeObject(type), Encoding.UTF8, "application/json");
            string[] tokenSplit = new string[2];
            if (token.Contains("Bearer"))
            {
                tokenSplit = token.Trim().Split(" ");
            }
            else
            {
                tokenSplit[1] = token;
            }

            var urlBuilder = new UriBuilder($"{baseUrl}{path}");
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);

            foreach (var item in type)
            {
                query["type"] = item;
            }
            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();

            var _httpClient = await _tokenService.GetCustomHttpClient(tokenSplit[1]);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenSplit[1]);

            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<NotificationTemplateDTO>>(resp);
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception($"Error GetNotificationTemplate-StatusCode:{apiResponse.StatusCode},Response:{resp}");
            }
        }
        
        public async Task<Boolean> SendNotification(List<SendNotificationRequest> request, string token)
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            var baseUrl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.SEND_NOTIFICATION_TEMPLATE]);
            //var objValue = JsonConvert.DeserializeObject<PostNewPushNotificationDTO>(payload);
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            string[] tokenSplit = new string[2];
            if (token.Contains("Bearer"))
            {
                tokenSplit = token.Trim().Split(" ");
            }
            else
            {
                tokenSplit[1] = token;
            }

            var urlBuilder = new UriBuilder($"{baseUrl}{path}");
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            string url = urlBuilder.ToString();

            var _httpClient = await _tokenService.GetCustomHttpClient(tokenSplit[1]);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenSplit[1]);

            var apiResponse = await _httpClient.PostAsync(url, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return true;
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception($"Error Posting Notification-StatusCode:{apiResponse.StatusCode},Response:{resp}");
            }
        }

        public async Task<List<ProjectConfiguration>> GetConfigurationByConfigGroupType(string buOfferingName, string configurationGroup, string currentToken, ILogger _logger)
        {
            using (var client = AzureHttpClient.GetAzureHttpClient(true))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentToken);
                client.DefaultRequestHeaders.Add("Accept", "Application/JSON");
                var environmentVariables = Environment.GetEnvironmentVariables();
                var baseUrl = Convert.ToString(environmentVariables[EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
                var path = Convert.ToString(environmentVariables[EnvAppSettingConstants.GET_CONFIGURATIONS_BY_CONFIG_TYPE]);
                Dictionary<string, dynamic> queries = new()
                {
                    {"expertiesName", buOfferingName },
                    {"configurationGroup" , configurationGroup }
                };
                var url = Helper.UrlBuilderByQuery($"{baseUrl}{path}", queries);
                string json = JsonConvert.SerializeObject(queries);
                //var requestData = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                List<ProjectConfiguration> projectConfiguration = JsonConvert.DeserializeObject<List<ProjectConfiguration>>(result);
                return projectConfiguration;
                //if (response.IsSuccessStatusCode)
                //{
                //    var result = await response.Content.ReadAsStringAsync();
                //    List<ProjectConfiguration> projectConfiguration = JsonConvert.DeserializeObject<List<ProjectConfiguration>>(result);
                //    return projectConfiguration;
                //}
                //else
                //{
                //    throw new Exception("error fetching configurations->GetConfigurationByConfigGroupType");
                //}

            }
        }

    }
}
