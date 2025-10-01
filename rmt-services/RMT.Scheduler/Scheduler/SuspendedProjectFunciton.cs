using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using RMT.Scheduler.Constants;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.service;

namespace RMT.Scheduler.Scheduler
{
    /// <summary>
    /// SuspendedProjectFunciton > This is compltee but moved to wcgt so not in use anymore
    /// to update the project status when a project is suspended in WCGT db 
    /// </summary>
    public class SuspendedProjectFunciton
    {

        //private readonly ILogger _logger;
        private readonly ITokenService _tokenService;
        public SuspendedProjectFunciton(ITokenService tokenService)
        {
            //_logger = loggerFactory.CreateLogger<SuspendedProjectFunciton>();
            _tokenService = tokenService;
        }

        //Not in Use Anymore
        [FunctionName("SuspendedProjectFunciton")]
        public async Task RunAsync([TimerTrigger("%SuspendedProjectSchedulerTriggerTime%")] TimerInfo myTimer, ILogger _logger)
        {
            _logger.LogInformation("Azure Function SuspendedProjectFunciton Started");
            try
            {
                string currentToken = await _tokenService.GetToken();
                var projectList = await GetSuspendProjectsList(currentToken, _logger);
                if (projectList.Count > 0)
                {
                    _logger.LogInformation($"Projects Suspended Start, Count- {projectList.Count}");

                    var response = await SuspendProjects(projectList, currentToken, _logger);

                    _logger.LogInformation($"Projects Suspended End ");
                }
                else
                {
                    _logger.LogInformation($"No Project in Suspended List");
                }
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            _logger.LogInformation("Azure Function SuspendedProjectFunciton Completed");
        }

        public static async Task<List<string>> GetSuspendProjectsList(string currentToken, ILogger _logger)
        {
            using (var client = AzureHttpClient.GetAzureHttpClient(true))
            {
                var environmentVaribles = Environment.GetEnvironmentVariables();
                var GetPipelineListFromWCGT = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.GET_PIPELINE_LIST_FROM_WCGT]);
                var gateway = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentToken);
                client.DefaultRequestHeaders.Add("Accept", "Application/JSON");

                _logger.LogInformation("--SuspendedProjectFunciton--GetSuspendProjectsList--URL-{0}", gateway + GetPipelineListFromWCGT);
                //todo change path from appsettings
                var response = await client.GetAsync(gateway + GetPipelineListFromWCGT);

                var result = await response.Content.ReadAsStringAsync();
                List<GTPipelineDTO> projectList = JsonConvert.DeserializeObject<List<GTPipelineDTO>>(result);
                List<string> suspendList = new List<string>();
                if (projectList != null && projectList.Count > 0)
                {
                    foreach (var item in projectList)
                    {
                        if (Constant.SUSPENDED_STATUS.Contains(item.pipeline_status))
                        {
                            suspendList.Add(item.pipeline_code);
                        }
                    }
                }
                else
                {
                    _logger.LogInformation("--SuspendedProjectFunciton--projectList is empty--URL-{0}", gateway + GetPipelineListFromWCGT);
                }
                return suspendList;
            }
        }

        //Move this logic to sync event suspend logic 
        public static async Task<string> SuspendProjects(List<string> projectCodes, string currentToken, ILogger _logger)
        {
            using (var client = AzureHttpClient.GetAzureHttpClient(true))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentToken);
                client.DefaultRequestHeaders.Add("Accept", "Application/JSON");
                var environmentVaribles = Environment.GetEnvironmentVariables();
                var UpdateProjectSuspensionStatus = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.UPDATE_PROJECT_SUSPENSION_STATUS]);
                var gateway = Convert.ToString(environmentVaribles[Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);

                SuspendedProjectRequest request = new SuspendedProjectRequest();
                List<KeyValuePair<string, string>> projectCodeKey = new List<KeyValuePair<string, string>>();
                foreach (var item in projectCodes)
                {
                    projectCodeKey.Add(new KeyValuePair<string, string>(item, null));
                }

                request.projectCodes = projectCodeKey;
                request.isSuspended = true;

                string json = JsonConvert.SerializeObject(request);
                var requestData = new StringContent(json, Encoding.UTF8, "application/json");

                _logger.LogInformation($"--SuspendedProjectFunciton--SuspendProjects--ProjectCodes-{json}");

                //todo change path from appsettings
                _logger.LogInformation($"--SuspendedProjectFunciton--SuspendProjects--URL-{gateway + UpdateProjectSuspensionStatus}");

                var response = await client.PostAsync(gateway + UpdateProjectSuspensionStatus, requestData);
                string result = string.Empty;
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"SuspendProjects StatusCode : {response.StatusCode}");
                    result = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"SuspendProjects result : {response}");
                    //  return result;
                }
                else
                {
                    result = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"SuspendProjects result Failed: {result}");

                }
                return result;
            }
        }
    }
}
