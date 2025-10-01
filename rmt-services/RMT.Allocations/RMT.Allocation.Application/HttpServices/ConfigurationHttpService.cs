using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.IHttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RMT.Allocation.Application.HttpServices
{
    public class ConfigurationHttpService : IConfigurationHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ConfigurationHttpService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<ConfigInfoDTO>> GetConfigurationByExpertiesNameAndGroupName(string keySelector, string groupName)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ConfigurationService").Value;
            string GetConfigurationByExpertiesNameAndGroupName = _config.GetSection("MicroserviceApiSettings").GetSection("GetConfigurationByExpertiesNameAndGroupName").Value;
            var apiResponse = await _httpClient.GetAsync(baseurl + GetConfigurationByExpertiesNameAndGroupName + $"?expertiesName={HttpUtility.UrlEncode(keySelector)}&configurationGroup={groupName}");
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                var finalResponse = JsonConvert.DeserializeObject<List<ConfigInfoDTO>>(resp);
                return finalResponse;
            }
            else
            {
                throw new Exception("Error Fetching GetConfigurationByExpertiesNameAndGroupName");
            }
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
