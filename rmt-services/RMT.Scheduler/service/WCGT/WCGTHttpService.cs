using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RMT.Scheduler.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RMT.Scheduler.service.Configurations.DTO;
using System.IO;

namespace RMT.Scheduler.service.WCGT
{
    public class WCGTHttpService : IWCGTHttpService
    {
        private readonly ITokenService _tokenService;
        public WCGTHttpService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public async Task<List<GTJobDTO>> GetJobsListFromWCGT()
        {
            var envVarDictionary = Environment.GetEnvironmentVariables();
            var GetGatewayBaseUrl = Convert.ToString(envVarDictionary[Constants.Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
            var JobListFromWCGT = Convert.ToString(envVarDictionary[Constants.Constant.EnvAppSettingConstants.GET_JOB_LIST_FROM_WCGT]);
            var jobListUrl = GetGatewayBaseUrl + JobListFromWCGT;
            var _httpClient = await _tokenService.GetCustomHttpClient();
            var jobsResult = await _httpClient.GetAsync(jobListUrl);
            if (jobsResult.IsSuccessStatusCode)
            {
                var jobsResponse = await jobsResult.Content.ReadAsStringAsync();
                var jobsList = JsonConvert.DeserializeObject<List<GTJobDTO>>(jobsResponse);
                return jobsList;
            }
            else
            {
                throw new Exception("Unable to fetch Jobcodes Content");
            }
        }

        public async Task<List<string>> GetProjectBudgetByModifiedDateRange(string currentToken, DateTime startDate, DateTime endDate, ILogger _logger)
        {
            //GetProjectBudgetByModifiedDateRange?startDate=2024-11-25%2014%3A30%3A24.753769&endDate=2024-11-25%2014%3A30%3A24.753769' 
            using (var client = AzureHttpClient.GetAzureHttpClient(true))
            {
                var environmentVaribles = Environment.GetEnvironmentVariables();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentToken);
                client.DefaultRequestHeaders.Add("Accept", "Application/JSON");
                var gateway = Convert.ToString(environmentVaribles[Constants.Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
                var apiPath = Convert.ToString(environmentVaribles[Constants.Constant.EnvAppSettingConstants.GET_PROJECT_BUDGET_BY_MODIFIED_DATERANGE]);

                Dictionary<string, dynamic> queries = new()
                {
                    {"startDate", startDate.ToString(Constants.Constant.BudgetSchedulerDateFormat) },
                    {"endDate" , endDate.ToString(Constants.Constant.BudgetSchedulerDateFormat) }
                };

                var fullPath = Helper.UrlBuilderByQuery($"{gateway}{apiPath}", queries);

                var response = await client.GetAsync(fullPath);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("--GetProjectBudgetByModifiedDateRange--Response Success-{0}", result);
                    List<string> projectList = JsonConvert.DeserializeObject<List<string>>(result);
                    return projectList;
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("--GetProjectBudgetByModifiedDateRange--Response Failed-{0}", result);
                    return new();
                }
            }
        }

    }
}
