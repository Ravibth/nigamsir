using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Notification.Application.Helpers;
using RMT.Notification.Application.Responses;
using ServiceLayer.Services.ConfigurationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Notification.Application.Constants.Constants;

namespace RMT.Notification.Application.HttpServices.ProjectConfigurationService
{
    public class ProjectConfigurationService:IProjectConfigurationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ProjectConfigurationService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<List<ProjectConfigurationResponse>> GetProjectConfiguration(string expertiesName, string configurationGroup)
        {
            var baseUrl = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.PROJECT_CONFIG_BASE_URI).Value);
            var path = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.GETPROJECTCONFIG).Value);
            // string userSkillPath = _config.GetSection("MicroserviceApiSettings").GetSection(path).Value;
            Dictionary<string, dynamic> queries = new()
                {
                    { "expertiesName", expertiesName

                    },
                    {
                      "configurationGroup",configurationGroup
                    }
                };

            var url = Helper.UrlBuilderByQuery($"{baseUrl}{path}", queries);


            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var response = await apiResponse.Content.ReadAsStringAsync();
                var projectConfig = JsonConvert.DeserializeObject<List<ProjectConfigurationResponse>>(response);
                return projectConfig;
            }
            else
            {
                throw new Exception("Unable to fetch Requistion Content");
            }
        }
    }
}
