using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.DTOs;
using ServiceLayer.Services.AllocationService.DTOs;
using ServiceLayer.Services.MarketPlaceService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static ServiceLayer.Constants.Constants;

namespace ServiceLayer.Services.WorkflowService
{
    public class WorkflowService : IWorkflowService
    {
        private readonly ILogger<WorkflowSubscription> _logger;
        public WorkflowService(ILogger<WorkflowSubscription> log, HttpClient httpClient)
        {
            _logger = log;
        }

        public async Task initWorkflow(NotificationPayloadDTO payload)
        {
            switch (payload.action)
            {
                case ServiceBusActions.CreateUserAllocationWorkflowAction:
                    _logger.LogInformation("--ServiceBus---Workflow--- initWorkflow create user allocation workflow ", payload);
                    await CreateWorkflow(payload);
                    break;

                case ServiceBusActions.CreateUserSkillAssessmentWorkflowAction:
                    _logger.LogInformation("--ServiceBus---Workflow--- initWorkflow CREATE_USER_SKILL_ASSESSMENT_WORKFLOW_ACTION workflow ", payload);
                    await CreateWorkflow(payload);
                    break;

                default:
                    _logger.LogInformation("--ServiceBus---Workflow--- No Action Match-{0}", payload.action);
                    break;
            }
        }
        public async Task CreateWorkflow(NotificationPayloadDTO payload)
        {
            var environmentVaribles = Environment.GetEnvironmentVariables();
            var baseUrl = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var createWorkflowUrl = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.GREATE_WORKFLOW_SERVICE]);
            var objValue = JsonConvert.DeserializeObject(payload.payload);
            var content = new StringContent(JsonConvert.SerializeObject(objValue), Encoding.UTF8, "application/json");
            //context.Items.DownstreamRequest().Headers.Add("userinfo", JsonConvert.SerializeObject(userInfo).ToString());
            var tokenSplit = payload.token.Trim().Split(" ");

            HttpClient _httpClient = AzureHttpClient.GetAzureHttpClient(true);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenSplit[1]);
            //Call worflow service post request to create new workflow
            var url = baseUrl + createWorkflowUrl;
            _logger.LogInformation("--ServiceBus---Workflow---CreateWorkflow->url-{0}-{1}", url, content);
            var apiResponse = await _httpClient.PostAsync(url, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                _logger.LogInformation("--ServiceBus---Workflow--- Success in service execution, StatusCode-{0}, Response-{1}", apiResponse.StatusCode, resp);

            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                _logger.LogInformation("--ServiceBus---Workflow--- Error in service execution, StatusCode-{0}, Response-{1}", apiResponse.StatusCode, resp);
            }
        }


        public async Task TerminateWorkflow(List<TerminateWorkflowDTO> workflowRADGUID, string token)
        {
            //terminate the workflow by guids
            try
            {
                var environmentVaribles = Environment.GetEnvironmentVariables();
                var baseUrl = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
                var terminateWorkflowUrl = Convert.ToString(environmentVaribles["TerminateWorkflowPath"]);


                //var objValue = JsonConvert.DeserializeObject(workflowRADGUID);// payload.payload);
                var content = new StringContent(JsonConvert.SerializeObject(workflowRADGUID), Encoding.UTF8, "application/json");
                //context.Items.DownstreamRequest().Headers.Add("userinfo", JsonConvert.SerializeObject(userInfo).ToString());
                if (token.Contains("Bearer"))
                {
                    token = token.Trim().Split(" ")[1];
                }
                //var tokenSplit = token.Trim().Split(" ");// payload.token.Trim().Split(" ");

                HttpClient _httpClient = AzureHttpClient.GetAzureHttpClient(true);

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //Call worflow service post request to terminate workflows
                var url = baseUrl + terminateWorkflowUrl;

                //if (_httpClient.DefaultRequestHeaders.Contains("userinfo") == false)
                //{
                //    _httpClient.DefaultRequestHeaders.Add("userinfo", JsonConvert.SerializeObject(userInfo).ToString());
                //}

                _logger.LogInformation("TerminateWorkflow-URL" + url);
                _logger.LogInformation("TerminateWorkflow-Content" + content);

                var apiResponse = await _httpClient.PostAsync(url, content);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation("Success in TerminateWorkflow-" + apiResponse.StatusCode + resp);
                }
                else
                {
                    var resp1 = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation("Error in TerminateWorkflow-" + apiResponse.StatusCode + resp1);
                    throw new Exception("Error fetching TerminateWorkflow");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task CreateWorkflowRollForward(NotificationPayloadDTO payload)
        {
            try
            {
                var environmentVaribles = Environment.GetEnvironmentVariables();
                var baseUrl = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
                var createWorkflowUrl = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.GREATE_WORKFLOW_SERVICE]);

                var objValue = JsonConvert.DeserializeObject(payload.payload);
                var content = new StringContent(JsonConvert.SerializeObject(objValue), Encoding.UTF8, "application/json");
                //context.Items.DownstreamRequest().Headers.Add("userinfo", JsonConvert.SerializeObject(userInfo).ToString());
                var tokenSplit = payload.token.Trim().Split(" ");

                HttpClient _httpClient = AzureHttpClient.GetAzureHttpClient(true);

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenSplit[1]);
                //Call worflow service post request to create new workflow
                var url = baseUrl + createWorkflowUrl;

                //if (_httpClient.DefaultRequestHeaders.Contains("userinfo") == false)
                //{
                //    _httpClient.DefaultRequestHeaders.Add("userinfo", JsonConvert.SerializeObject(userInfo).ToString());
                //}

                //_logger.LogInformation($"Sending request to {url}");
                _logger.LogInformation("CreateWorkflow-URL" + url);
                _logger.LogInformation("CreateWorkflow-tokenSplit" + tokenSplit[1]);
                _logger.LogInformation("CreateWorkflow-token" + payload.token);
                _logger.LogInformation("CreateWorkflow-payload" + payload.payload);
                _logger.LogInformation("CreateWorkflow-objValue" + objValue);
                _logger.LogInformation("CreateWorkflow-Content" + content);

                var apiResponse = await _httpClient.PostAsync(url, content);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation("Success in CreateWorkflow-if" + apiResponse.StatusCode + resp);
                }
                else
                {
                    var resp1 = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation("Error in CreateWorkflow-else" + apiResponse.StatusCode + resp1);
                    throw new Exception("Error fetching CreateWorkflow");
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error in Create Workflow", ex.Message, ex.StackTrace, ex.InnerException);
                throw;
            }
        }

        public async Task RefreshAssignedTask(RefreshAssignedWorkflowTaskDTO request , string token)
        {
            var environmentVaribles = Environment.GetEnvironmentVariables();
            var baseUrl = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var refreshUrl = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.REFRESH_WORKFLOW_TASK_ASSIGNMENT]);
            if (token.Contains("Bearer"))
            {
                token = token.Trim().Split(" ")[1];
            }
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpClient _httpClient = AzureHttpClient.GetAzureHttpClient(true);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //Call worflow service post request to create new workflow
            var url = baseUrl + refreshUrl;
            _logger.LogInformation("--ServiceBus---Workflow---CreateWorkflow->url-{0}-{1}", url, content);
            var apiResponse = await _httpClient.PostAsync(url, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                _logger.LogInformation("--ServiceBus---Workflow--- Success in service execution, StatusCode-{0}, Response-{1}", apiResponse.StatusCode, resp);

            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                _logger.LogInformation("--ServiceBus---Workflow--- Error in service execution, StatusCode-{0}, Response-{1}", apiResponse.StatusCode, resp);
            }
        }

        public async Task<List<WorkflowTaskDTO>> GetWorkflowSuperCoachTask(string employee_email, string supercoach_email, string token)
        {
            var environmentVaribles = Environment.GetEnvironmentVariables();
            var baseUrl = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var refreshUrl = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.GetWorkflowSuperCoachTask]);
            if (token.Contains("Bearer"))
            {
                token = token.Trim().Split(" ")[1];
            }
            HttpClient _httpClient = AzureHttpClient.GetAzureHttpClient(true);
            Dictionary<string, dynamic> queries = new()
            {
                { "employeeEmail", employee_email },
                { "supercoachEmail", supercoach_email },
                { "outcome", WF_TASK_OUTCOME },
                { "module", WF_TASK_MODULE },
                { "workflow_task_status", WF_TASK_STATUS },
            };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //Call worflow service post request to create new workflow
            var url = Helper.UrlBuilderByQuery($"{baseUrl}{refreshUrl}", queries);
            _logger.LogInformation("--ServiceBus---Workflow---GetPendingAssignedTask->url-{0}-{1}", url, queries);
            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                _logger.LogInformation("--ServiceBus---Workflow--- Success in service execution, StatusCode-{0}, Response-{1}", apiResponse.StatusCode, resp);
                return JsonConvert.DeserializeObject<List<WorkflowTaskDTO>>(resp);
            }
            else
            {
                throw new Exception("Error fetching GetPendingAssignedTask");
            }
        }
    }
}
