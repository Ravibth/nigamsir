using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.DTOs;
using ServiceLayer.Services.ConfigurationService;
using ServiceLayer.Services.MarketPlaceService;
using ServiceLayer.Services.MarketPlaceService.DTOs;
using ServiceLayer.Services.ProjectService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static ServiceLayer.Constants.Constants;

namespace ServiceLayer.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient httpClient;
        private readonly IMarketPlaceService marketplaceService;
        private readonly ILogger<ProjectSubscription> logger;
        public ProjectService(HttpClient httpClient, IMarketPlaceService marketplaceService, ILogger<ProjectSubscription> logger)
        {
            this.httpClient = AzureHttpClient.GetAzureHttpClient(true);
            this.marketplaceService = marketplaceService;
            this.logger = logger;
        }

        public async Task<List<RoleEmailsByPipelineCodeResponse>> GetRolesEmailByPipelineCodesAndRoles(List<PipelineCodeAndRolesDto> pipelineCodeAndRolesDto, string token)
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            var baseurl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.GET_ROLES_EMAIL_BY_PIPELINECODE_AND_ROLES]);
            //var baseurl = Environment.GetEnvironmentVariable("BaseGatewayUrl");
            //var path = Environment.GetEnvironmentVariable("GetRolesEmailByPipelineCodesAndRoles");
            var url = baseurl + path;
            var content = new StringContent(JsonConvert.SerializeObject(pipelineCodeAndRolesDto), Encoding.UTF8, "application/json");

            var tokenSplit = token.Trim().Split(" ");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, tokenSplit[1]);
            //TODO:- change to be made
            var apiResponse = await httpClient.PostAsync(url, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RoleEmailsByPipelineCodeResponse>>(resp);
            }
            else
            {
                throw new Exception("Error fetching GetRolesEmailByPipelineCodesAndRoles");
            }
        }

        public async Task<Project> GetProjectDetailByPipelineCode(string pipelineCodeORJobCode, string token)
        {

            var environmentVariables = Environment.GetEnvironmentVariables();
            var baseurl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.GetMembersOfAllProjectsOfUsers]);
            var tokenSplit = token.Trim().Split(" ");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, tokenSplit[1]);
            var apiResponse = await httpClient.GetAsync($"{baseurl}{path}/{pipelineCodeORJobCode}");
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Project>(resp);
            }
            else
            {
                throw new Exception("Error fetching GetProjectDetailByPipelineCode");
            }
        }

        public async Task<List<GetMembersOfAllProjectsOfUserResponse>> GetListOfAllMembersOfAllProjectsOfUser(List<string> users, string token)
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            var baseurl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.GetMembersOfAllProjectsOfUsers]);
            //var baseurl = Environment.GetEnvironmentVariable("BaseGatewayUrl");
            //todo check this path again for this api
            //var path = Environment.GetEnvironmentVariable("GetRolesEmailByPipelineCodesAndRoles");

            Dictionary<string, dynamic> queries = new()
            {
                { "users", users }
            };
            var url = Helper.UrlBuilderByQuery($"{baseurl}{path}", queries);
            var tokenSplit = token.Trim().Split(" ");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, tokenSplit[1]);

            var apiResponse = await httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<GetMembersOfAllProjectsOfUserResponse>>(resp);
            }
            else
            {
                throw new Exception("Error fetching GetListOfAllMembersOfAllProjectsOfUser");
            }
        }

        public async Task<List<ProjectRolesResponseDTO>> GetResourceRequestorEmailsByPipelineCode(string pipelineCode, string jobCode, string token)
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            var getwayBaseUrl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.GET_RESOURCE_REQUESTOR_EMAILS_BY_PIPELINE_CODE]);
            Dictionary<string, dynamic> queries = new()
            {
                {"pipelineCode", pipelineCode },
                {"jobCode" , jobCode }
            };
            var url = Helper.UrlBuilderByQuery($"{getwayBaseUrl}{path}", queries);
            var tokenSplit = token.Trim().Split(" ");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, tokenSplit[1]);
            var apiResponse = await httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var response = await apiResponse.Content.ReadAsStringAsync();
                var finalResponse = JsonConvert.DeserializeObject<List<ProjectRolesResponseDTO>>(response);
                return finalResponse;
            }
            else
            {
                throw new Exception("error fetching GetResourceRequestorEmailsByPipelineCode resource requestors ");
            }

        }


        public async Task<MarketPlaceProjectDetailResponse> ProcessTopicPayload(NotificationPayloadDTO serviceBusPayload)
        {
            //
            logger.LogInformation("--ServiceBus---ProjectSubscription--- ProcessTopicPayload Áction-{0}", serviceBusPayload.action);
            switch (serviceBusPayload.action)
            {
                case ServiceBusActions.UpdateMarkeplaceProjectDetails:
                    logger.LogInformation("--ServiceBus---ProjectSubscription--- ProcessTopicPayload UpdateMarkeplaceProjectDetails ", serviceBusPayload);
                    await marketplaceService.UpdateMarkeplaceProjectDetails(serviceBusPayload);
                    break;
                case ServiceBusActions.REFRESH_PROJECT_COMPETENCY_EVENT:
                    logger.LogInformation("--ServiceBus---ProjectSubscription--- ProcessTopicPayload UpdateMarkeplaceProjectDetails ", serviceBusPayload);
                    await RefreshProjectCompetency(serviceBusPayload);
                    break;
                case ServiceBusActions.REFRESH_MARKETPLACE_EMPLOYEE_INTREST_SCORE:
                    logger.LogInformation("--ServiceBus---ProjectSubscription--- ProcessTopicPayload UpdateMarkeplaceProjectDetails ", serviceBusPayload);
                    await RefreshMarketPlaceIntrestScore(serviceBusPayload);
                    break;
                case ServiceBusActions.REFRESH_ALLOCATION_STATUS:
                    logger.LogInformation("--ServiceBus---ProjectSubscription--- ProcessTopicPayload REFRESH_ALLOCATION_STATUS ", serviceBusPayload);
                    await UpdateAllocationStatus(serviceBusPayload);
                    break;
                case ServiceBusActions.REFRESH_SKILL_STATUS:
                    logger.LogInformation("--ServiceBus---ProjectSubscription--- ProcessTopicPayload REFRESH_SKILL_STATUS ", serviceBusPayload);
                    await UpdateSkillStatus(serviceBusPayload);
                    break;
                case ServiceBusActions.ADD_UPDATE_SUPERCOACH_DELEGATE:
                    logger.LogInformation("--ServiceBus-- - ProjectSubscription-- - ProcessTopicPayload ADD_UPDATE_SUPERCOACH_DELEGATE ", serviceBusPayload);
                    await UpdateWorkflowTaskSuperCoach(serviceBusPayload);
                    logger.LogInformation("--ServiceBus-- - ProjectSubscription-- - ProcessTopicPayload  UpdateWorkflowTaskSuperCoach Completed");
                    await UpdateProjectRolesForSupercoachDelegate(serviceBusPayload);
                    break;
                default:
                    break;
            }

            return null;
        }
        public async Task<string> UpdateProjectRolesForSupercoachDelegate(NotificationPayloadDTO notificationPayloadDTO)
        {
            string token = notificationPayloadDTO.token;
            if (token.Contains("Bearer"))
            {
                token = token.Split(' ')[1];
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");
            var environmentVaribles = Environment.GetEnvironmentVariables();
            var path = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.UPDATE_SUPERCOACH_AND_DELEGATE_FOR_PROJECT]);
            var gateway = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.BASE_GATEWAY_URL]);

            var requestData = new StringContent(notificationPayloadDTO.payload, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(gateway + path, requestData);
            string result = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation($"UpdateWorkflowTaskSuperCoach StatusCode : {response.StatusCode}");
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"UpdateWorkflowTaskSuperCoach result : {response}");
                //  return result;
            }
            else
            {
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"UpdateSkillStatus result Failed: {result}");
            }
            return result;
        }
        public async Task<string> UpdateWorkflowTaskSuperCoach(NotificationPayloadDTO notificationPayloadDTO)
        {
            string token = notificationPayloadDTO.token;
            if (token.Contains("Bearer"))
            {
                token = token.Split(' ')[1];
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");
            var environmentVaribles = Environment.GetEnvironmentVariables();
            var path = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.UPDATE_SUPERCOACH_AND_DELEGATE]);
            var gateway = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var requestData = new StringContent(notificationPayloadDTO.payload, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(gateway + path, requestData);
            string result = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation($"UpdateWorkflowTaskSuperCoach StatusCode : {response.StatusCode}");
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"UpdateWorkflowTaskSuperCoach result : {response}");
                //  return result;
            }
            else
            {
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"UpdateSkillStatus result Failed: {result}");
            }
            return result;
        }
        public async Task<string> UpdateSkillStatus(NotificationPayloadDTO notificationPayloadDTO)
        {
            string token = notificationPayloadDTO.token;
            if (token.Contains("Bearer"))
            {
                token = token.Split(' ')[1];
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");
            var environmentVaribles = Environment.GetEnvironmentVariables();
            var RefreshProjectCompetency = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.UPDATE_PROJECT_SKILL_STATUS]);
            var gateway = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var requestData = new StringContent(notificationPayloadDTO.payload, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(gateway + RefreshProjectCompetency, requestData);
            string result = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation($"UpdateSkillStatus StatusCode : {response.StatusCode}");
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"UpdateSkillStatus result : {response}");
                //  return result;
            }
            else
            {
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"UpdateSkillStatus result Failed: {result}");
            }
            return result;
        }
        public async Task<string> UpdateAllocationStatus(NotificationPayloadDTO notificationPayloadDTO)
        {
            string token = notificationPayloadDTO.token;
            if (token.Contains("Bearer"))
            {
                token = token.Split(' ')[1];
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");
            var environmentVaribles = Environment.GetEnvironmentVariables();
            var RefreshProjectCompetency = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.UPDATE_PROJECT_ALLOCATION_STATUS]);
            var gateway = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var requestData = new StringContent(notificationPayloadDTO.payload, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(gateway + RefreshProjectCompetency, requestData);
            string result = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation($"UpdateAllocationStatus StatusCode : {response.StatusCode}");
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"UpdateAllocationStatus result : {response}");
                //  return result;
            }
            else
            {
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"UpdateAllocationStatus result Failed: {result}");
            }
            return result;
        }
        public async Task<string> RefreshProjectCompetency(NotificationPayloadDTO notificationPayloadDTO)
        {
            string token = notificationPayloadDTO.token;
            if (token.Contains("Bearer"))
            {
                token = token.Split(' ')[1];
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");
            var environmentVaribles = Environment.GetEnvironmentVariables();
            var RefreshProjectCompetency = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.REFRESH_PROJECT_COMPETENCYAPI]);
            var gateway = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var requestData = new StringContent(notificationPayloadDTO.payload, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(gateway + RefreshProjectCompetency, requestData);
            string result = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation($"RefreshProjectCompetency StatusCode : {response.StatusCode}");
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"RefreshProjectCompetency result : {response}");
                //  return result;
            }
            else
            {
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"RefreshProjectCompetency result Failed: {result}");
            }
            return result;
        }
        public async Task<string> RefreshMarketPlaceIntrestScore(NotificationPayloadDTO notificationPayloadDTO)
        {
            string token = notificationPayloadDTO.token;
            if (token.Contains("Bearer"))
            {
                token = token.Split(' ')[1];
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");
            var environmentVaribles = Environment.GetEnvironmentVariables();
            var employeeProjectIntrestScore = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.RefreshEmpProjectInterestScore]);
            var gateway = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var requestData = new StringContent(notificationPayloadDTO.payload, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(gateway + employeeProjectIntrestScore, requestData);
            string result = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation($"RefreshMarketPlaceIntrestScore StatusCode : {response.StatusCode}");
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"RefreshMarketPlaceIntrestScore result : {response}");
                //  return result;
            }
            else
            {
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"RefreshMarketPlaceIntrestScore result Failed: {result}");
            }
            return result;
        }
        public async Task<string> ProjectActivationStatusChange(string PipelineCode, string? JobCode, string token , bool IsJobClosed)
        {
            ProjectActivationStatusChangeDTO request = new ProjectActivationStatusChangeDTO()
            {
                PipelineCode = PipelineCode,
                JobCode = string.IsNullOrEmpty(JobCode) ? null : JobCode,
                IsJobClosed = IsJobClosed
            };
            if (token.Contains("Bearer"))
            {
                token = token.Split(' ')[1];
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");
            var environmentVaribles = Environment.GetEnvironmentVariables();
            var gateway = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var employeeProjectIntrestScore = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.ProjectActivationStatusChange]);
            var requestData = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(gateway + employeeProjectIntrestScore, requestData);
            string result = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation($"ProjectActivationStatusChange StatusCode : {response.StatusCode}");
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"ProjectActivationStatusChange result : {response}");
                //  return result;
            }
            else
            {
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"ProjectActivationStatusChange result Failed: {result}");
            }
            return result;
        }
        public async Task<string> SuspendProjects(List<string> projectCodes, string currentToken)
        {
            string token = currentToken;
            if (currentToken.Contains("Bearer"))
            {
                token = currentToken.Split(' ')[1];
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");
            var environmentVaribles = Environment.GetEnvironmentVariables();
            var UpdateProjectSuspensionStatus = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.UPDATE_PROJECT_SUSPENSION_STATUS]);
            var gateway = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.BASE_GATEWAY_URL]);

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


            logger.LogInformation($"--SuspendedProjectFunciton--SuspendProjects--ProjectCodes-{json}");

            //todo change path from appsettings
            logger.LogInformation($"--SuspendedProjectFunciton--SuspendProjects--URL-{gateway + UpdateProjectSuspensionStatus}");

            var response = await httpClient.PostAsync(gateway + UpdateProjectSuspensionStatus, requestData);
            string result = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation($"SuspendProjects StatusCode : {response.StatusCode}");
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"SuspendProjects result : {response}");
                //  return result;
            }
            else
            {
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"SuspendProjects result Failed: {result}");

            }
            return result;
        }
        public async Task ReplaceProjectsSuperCoachRole(SuperCoachProjectRole request, string currentToken)
        {
            string token = currentToken;
            if (currentToken.Contains("Bearer"))
            {
                token = currentToken.Split(' ')[1];
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");
            var environmentVaribles = Environment.GetEnvironmentVariables();
            var UpdateProjectSuspensionStatus = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.REPLACE_PROJECT_SUPERCOACH_ROLE]);
            var gateway = Convert.ToString(environmentVaribles[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
           
            string json = JsonConvert.SerializeObject(request);
            var requestData = new StringContent(json, Encoding.UTF8, "application/json");

            logger.LogInformation($"--ReplaceProjectsSuperCoachRoleFunciton--ReplaceProjectsSuperCoachRole--ProjectCodes-{json}");
            //todo change path from appsettings
            logger.LogInformation($"--ReplaceProjectsSuperCoachRoleFunciton--ReplaceProjectsSuperCoachRole--URL-{gateway + UpdateProjectSuspensionStatus}");
            var response = await httpClient.PostAsync(gateway + UpdateProjectSuspensionStatus, requestData);
            string result = string.Empty;
            if (response.IsSuccessStatusCode)
            {
                logger.LogInformation($"ReplaceProjectsSuperCoachRole StatusCode : {response.StatusCode}");
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"ReplaceProjectsSuperCoachRole result : {response}");
                //  return result;
            }
            else
            {
                result = await response.Content.ReadAsStringAsync();
                logger.LogInformation($"ReplaceProjectsSuperCoachRole result Failed: {result}");

            }          
        }

    }
}
