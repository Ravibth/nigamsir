using Gateway.API.Dtos;
using Gateway.API.Helpers.IHttpServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gateway.API.Helpers.HttpServices
{
    public class ConfigurationHttpService : IConfigurationHttpService
    {
        //private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly ILogger<ConfigurationHttpService> _logger;
        public ConfigurationHttpService(HttpClient httpClient, IConfiguration config, ILogger<ConfigurationHttpService> logger)
        {
            //_httpClient = httpClient;
            _config = config;
            _logger = logger;
        }
        /// <summary>
        /// NOT_IN_USE
        /// </summary>
        /// <param name="expertiesName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<ConfigInfoDTO>> GetConfigurationByExpertiesNameAndGroupName(string expertiesName, string groupName)
        {

            HttpClient _httpClient = new();
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ConfigurationService").Value;
            string GetConfigurationByExpertiesNameAndGroupName = _config.GetSection("MicroserviceApiSettings").GetSection("GetConfigurationByExpertiesNameAndGroupName").Value;
            var apiResponse = await _httpClient.GetAsync(baseurl + GetConfigurationByExpertiesNameAndGroupName + $"?expertiesName={expertiesName}&configurationGroup={groupName}");

            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                var finalResponse = JsonConvert.DeserializeObject<List<ConfigInfoDTO>>(resp);
                return finalResponse;
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                _logger.LogInformation($"Error in Fetching Workflow Service {resp}");
                throw new Exception($"GetConfigurationByExpertiesNameAndGroupName->Error Fetching:{resp}");
            }
        }
        /// <summary>
        /// NOT_IN_USE
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<ConfigurationGroup>> GetConfigurationGroupByGroupNameAndConfigType(string groupName, string configType)
        {
            HttpClient _httpClient = new();
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
                var resp = await apiResponse.Content.ReadAsStringAsync();
                _logger.LogInformation($"Error in Fetching Workflow Service {resp}");
                throw new Exception($"GetConfigurationGroupByGroupNameAndConfigType->Error Fetching:{resp}");
            }
        }
        public async Task<List<ConfigurationGroup>> GetConfigurationByConfigGroupConfigKeyAndConfigType(string groupName, string configKey, string configType)
        {
            HttpClient _httpClient = new();
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ConfigurationService").Value;
            string GetConfigurationByConfigGroupConfigKeyAndConfigType = _config.GetSection("MicroserviceApiSettings").GetSection("GetConfigurationByConfigGroupConfigKeyAndConfigType").Value;
            var apiResponse = await _httpClient.GetAsync(baseurl + GetConfigurationByConfigGroupConfigKeyAndConfigType + $"?groupName={groupName}&configKey={configKey}&configType={configType}");
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                var finalResponse = JsonConvert.DeserializeObject<List<ConfigurationGroup>>(resp);
                return finalResponse;
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                _logger.LogInformation($"Error in Fetching Workflow Service {resp}");
                throw new Exception($"GetConfigurationByConfigGroupConfigKeyAndConfigType->Error Fetching:{resp}");
            }
        }
    }
}
