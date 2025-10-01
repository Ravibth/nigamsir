using Azure.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.service.Configurations.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static RMT.Scheduler.Constants.Constant;

namespace RMT.Scheduler.service.Configurations
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly HttpClient _httpClient;

        public ConfigurationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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