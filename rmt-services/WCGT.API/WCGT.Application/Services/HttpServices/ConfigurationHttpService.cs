using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Services.IHttpServices;

namespace WCGT.Application.Services.HttpServices
{
    public class ConfigurationHttpService : IConfigurationHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ConfigurationHttpService(HttpClient httpClient, IConfiguration config)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<Dictionary<string, string>> GetApplicationLevelSettings(List<string>? keys)
        {
            string configurationBaseUrl = _config.GetSection("MicroserviceApiSettings").GetSection("configurationBaseUrl").Value;
            string GetApplicationLevelSettingsUrl = _config.GetSection("MicroserviceApiSettings").GetSection("GetApplicationLevelSettings").Value;

            Dictionary<string, dynamic> queries = new()
            {
            };
            if (keys != null && keys.Count > 0)
            {
                queries.Add("keys", keys.ToList());
            }

            string finalUrl = Helper.UrlBuilderByQuery(configurationBaseUrl + GetApplicationLevelSettingsUrl, queries);

            var apiRequest = await _httpClient.GetAsync(finalUrl);
            var apiResponse = await apiRequest.Content.ReadAsStringAsync();
            if (apiRequest.IsSuccessStatusCode)
            {
                Dictionary<string, string> response = JsonConvert.DeserializeObject<Dictionary<string, string>>(apiResponse);
                return response;
            }
            else
            {
                throw new Exception("Error fetching GetApplicationLevelSettings URL:" + finalUrl + ", Response:" + apiResponse);
            }
        }
    }
}
