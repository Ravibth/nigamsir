using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Projects.Application.HttpServices.DTOs;
using RMT.Projects.Application.IHttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.HttpServices
{
    public class ConfigurationHttpApi : IConfigurationHttpApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public ConfigurationHttpApi(HttpClient httpClient, IConfiguration configuration)
        {
            _config = configuration;
            _httpClient = httpClient;
        }
        public async Task<List<ConfigurationGroup>> GetProjectConfigurationByConfigGroupAndConfigType(string groupName, string configType)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ConfigurationService").Value;
            string GetConfigurationGroupByGroupNameAndConfigType = _config.GetSection("MicroserviceApiSettings").GetSection("GetConfigurationGroupByGroupNameAndConfigType").Value;
            var apiResponse = await _httpClient.GetAsync(baseurl + GetConfigurationGroupByGroupNameAndConfigType + $"?groupName={groupName}&configType={configType}");
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                var finalResponse = JsonConvert.DeserializeObject<List<ConfigurationGroup>>(resp);
                return finalResponse;
            }
            else
            {
                throw new Exception($"GetConfigurationGroupByGroupNameAndConfigType->Error Fetching:{await apiResponse.Content.ReadAsStringAsync()}");
            }
        }
    }
}
