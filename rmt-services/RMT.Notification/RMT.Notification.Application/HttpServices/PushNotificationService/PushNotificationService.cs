using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RMT.Notification.Application.HttpServices.DTO;
using ServiceLayer.Services.ConfigurationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static RMT.Notification.Application.Constants.Constants;

namespace ServiceLayer.Services.PushNotificationService
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationService _configurationService;
        private readonly IConfiguration _configuration;
        //private readonly ILogger _logger;

        public PushNotificationService(HttpClient httpClient, IConfigurationService configurationService , IConfiguration configuration)
        {
            this._httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task PostNewPushNotification(List<PostNewPushNotificationDTO> payload, string token)
        {
            foreach (var item in payload)
            {
                //var environmentVarible = Environment.GetEnvironmentVariables();
                var baseUrl = Convert.ToString( _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.BASE_GATEWAY_URL).Value);
                var postNewNotificationService = Convert.ToString(_configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.POST_NEW_NOTIFICATION_SERVICE).Value);
                var url = baseUrl + postNewNotificationService;

                var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                var tokenSplit = token.Trim().Split(" ");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenSplit[1]);

                var apiResponse = await _httpClient.PostAsync(url, content);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                }
                else
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    throw new Exception($"Error Posting Notification {resp}");
                }
            }
        }
    }
}
