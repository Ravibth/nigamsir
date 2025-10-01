using Azure.Core;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Notification.Application.Helpers;
using RMT.Notification.Application.HttpServices.DTO;
using RMT.Notification.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static RMT.Notification.Application.Constants.Constants;

namespace ServiceLayer.Services.ConfigurationService
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ConfigurationService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        /// <summary>
        /// Dynamically replace the payload items in template
        /// </summary>
        /// <param name="template"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public string TransformMessageTemplateAccordingToPayloads(string template, List<NotificationPlaceHolderDTO> payloadKeysRequired, Dictionary<string, string> keyValuePairs)
        {
            var tempTemplate = template;
            foreach (var item in payloadKeysRequired)
            {
                var keyValue = keyValuePairs.Where((m) => m.Key.Trim().ToLower().Equals(item.name.Trim().ToLower())).FirstOrDefault().Value;
                // todo uncomment later
                //if (item.is_required != null && item.is_required == true && String.IsNullOrEmpty(keyValue))
                //{
                //    throw new Exception($"InAdequate Details, {item.name} not found");
                //}
                tempTemplate = tempTemplate.Replace($"<{item.name}>", keyValue != null ? keyValue : "");
            }
            tempTemplate = tempTemplate.Replace("\n", "<br/>");
            return tempTemplate;
        }

        public async Task<List<NotificationTemplateDTO>> GetNotificationTemplate(string[] type, string token)
        {
            //var environmentVariables = Environment.GetEnvironmentVariables();
            //var baseUrl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            //var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.GET_NOTIFICATION_TEMPLATE]);

            var baseUrl = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.BASE_GATEWAY_URL).Value);
            var path = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.GET_NOTIFICATION_TEMPLATE).Value);

            //var objValue = JsonConvert.DeserializeObject<PostNewPushNotificationDTO>(payload);
            var content = new StringContent(JsonConvert.SerializeObject(type), Encoding.UTF8, "application/json");
            string[] tokenSplit = new string[2];
            if (token.Contains("Bearer"))
            {
                tokenSplit = token.Trim().Split(" ");
            }
            else
            {
                tokenSplit[1] = token;
            }

            var urlBuilder = new UriBuilder($"{baseUrl}{path}");
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);

            foreach (var item in type)
            {
                query["type"] = item;
            }
            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();


            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenSplit[1]);

            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<NotificationTemplateDTO>>(resp);
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception($"Error fetching GetNotificationTemplate {resp}");
            }
        }
        public async Task<string> GetResourceAllocationDetailsByGuid(string guid)
        {
            try
            {
                //var environmentVariables = Environment.GetEnvironmentVariables();
                //var baseurl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
                //var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.RESOURCE_ALLOCATION_BASE_URI]);
                string resAllocBase = _config.GetSection("MicroserviceApiSettings").GetSection("ResourceAllocationMicroserviceBaseApiUrl").Value;
                string alloc = _config.GetSection("MicroserviceApiSettings").GetSection("ResourceAllocationDetailsByGuid").Value;
                Dictionary<string, dynamic> queries = new()
                {
                    { "guid", Convert.ToString( guid) }
                };

                var url = Helper.UrlBuilderByQuery($"{resAllocBase}{alloc}", queries);


                var apiResponse = await _httpClient.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    return resp;
                    //return JsonConvert.DeserializeObject<ResourceAllocationDetailsResponse>(resp);
                }
                else
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    throw new Exception($"Error fetching GetResourceAllocationDetailsByGuid Allocation Data {resp}");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> GetRequestorEmailsForAllocationWorkflow(string pipelineCode, string jobCode, string workflowCreatedBy)
        {
            string projectBaseUrl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectServiceBaseUrl").Value;
            string requestorEmailsForAllocationWorkflow = _config.GetSection("MicroserviceApiSettings").GetSection("GetRequestorEmailsForAllocationWorkflow").Value;
            Dictionary<string, dynamic> queries = new()
             {
                 { "pipelineCode", Convert.ToString(pipelineCode) },
                 { "jobCode", Convert.ToString(jobCode) },
                 { "workflowStartedBy", Convert.ToString(workflowCreatedBy)  }
            };
            var url = Helper.UrlBuilderByQuery($"{projectBaseUrl}{requestorEmailsForAllocationWorkflow}", queries);
            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return resp;
                //return JsonConvert.DeserializeObject<ResourceAllocationDetailsResponse>(resp);
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception($"Error fetching GetRequestorEmailsForAllocationWorkflow Allocation Data {resp}");
            }

        }

        public async Task<string> GetResourceReviewerEmailsByPipelineCode(string pipelineCode, string jobCode)
        {
            try
            {

                string baseUrl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectServiceBaseUrl").Value;
                string path = _config.GetSection("MicroserviceApiSettings").GetSection("GetResourceReviewerEmailsByPipelineCode").Value;
                Dictionary<string, dynamic> queries = new()
                {
                     { "pipelineCode", Convert.ToString(pipelineCode) },
                     { "jobCode", string.IsNullOrEmpty(jobCode) ? null : Convert.ToString(jobCode) }
                };
                var url = Helper.UrlBuilderByQuery($"{baseUrl}{path}", queries);
                var apiResponse = await _httpClient.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    return resp;
                    //return JsonConvert.DeserializeObject<ResourceAllocationDetailsResponse>(resp);
                }
                else
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    throw new Exception($"Error fetching GetResourceReviewerEmailsByPipelineCode Allocation Data {resp}");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<string> GetResourceRequestorEmailsByPipelineCode(string pipelineCode, string? jobCode)
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            string baseUrl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectServiceBaseUrl").Value;
            string path = _config.GetSection("MicroserviceApiSettings").GetSection(EnvVariblesAppsetting.GET_RESOURCE_REQUESTOR_EMAILS_BY_PIPELINE_CODE).Value;
            //var getwayBaseUrl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.ProjectServiceBaseUrl]);
            //var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.GET_RESOURCE_REQUESTOR_EMAILS_BY_PIPELINE_CODE]);
            Dictionary<string, dynamic> queries = new()
            {
                {"pipelineCode", pipelineCode },
                {"jobCode" , jobCode }
            };
            var url = Helper.UrlBuilderByQuery($"{baseUrl}{path}", queries);
            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var response = await apiResponse.Content.ReadAsStringAsync();
                return response;
            }
            else
            {
                var response = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetResourceRequestorEmailsByPipelineCode resource requestors " + response);

            }

        }

        public async Task<string> GetProjectDetailsByPipelineCodeAndJobCode(string pipelineCode, string? jobCode)
        {
            try
            {
                string baseUrl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectServiceBaseUrl").Value;
                string path = _config.GetSection("MicroserviceApiSettings").GetSection("GetProjectDetailByPipelineCodeAndJobCode").Value;
                Dictionary<string, dynamic> queries = new()
                {
                     { "pipelineCode", Convert.ToString(Uri.EscapeDataString( pipelineCode)) },
                     { "jobCode", string.IsNullOrEmpty(jobCode) ? null : Convert.ToString(Uri.EscapeDataString( jobCode)) }
                };
                var url = Helper.UrlBuilderByQuery($"{baseUrl}{path}", queries);
                var apiResponse = await _httpClient.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    return resp;
                    //return JsonConvert.DeserializeObject<ResourceAllocationDetailsResponse>(resp);
                }
                else
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    throw new Exception($"Error fetching GetProjectDetailsByPipelineCodeAndJobCode Allocation Data {resp}");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<RoleEmailsByPipelineCodeResponse>> GetRolesEmailByPipelineCodesAndRoles(List<PipelineCodeAndRolesDto> pipelineCodeAndRolesDto)
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            //var baseurl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            //var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.GET_ROLES_EMAIL_BY_PIPELINECODE_AND_ROLES]);
            var baseUrl = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.ProjectServiceBaseUrl).Value);
            var path = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.GET_ROLES_EMAIL_BY_PIPELINECODE_AND_ROLES).Value);
            //var baseurl = Environment.GetEnvironmentVariable("BaseGatewayUrl");
            //var path = Environment.GetEnvironmentVariable("GetRolesEmailByPipelineCodesAndRoles");
            var url = baseUrl + path;
            var content = new StringContent(JsonConvert.SerializeObject(pipelineCodeAndRolesDto), Encoding.UTF8, "application/json");

            //TODO:- change to be made
            var apiResponse = await _httpClient.PostAsync(url, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RoleEmailsByPipelineCodeResponse>>(resp);
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetRolesEmailByPipelineCodesAndRoles-" + resp);
            }
        }

        public async Task<List<WorkflowDTO>> GetWorkflowPendingTasks()
        {
            string taskstatus = "pending";
            string outcome = "inprogress";
            var baseUrl = _config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.WORKFLOW_BASE_URL).Value;
            var path = _config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.GET_WORKFLOW_TASK).Value;
            Dictionary<string, dynamic> queries = new Dictionary<string, dynamic>()
            {
                {"taskstatus" , taskstatus },
                {"outcome" , outcome }
            };
            string finalUrl = Helper.UrlBuilderByQuery(baseUrl + path, queries);
            var apiResponse = await _httpClient.GetAsync(finalUrl);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<WorkflowDTO>>(resp);
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception($"Error fetching GetWorkflowPendingTasks Api did not fetch response {resp}");
            }
        }
        public async Task<List<WorkflowDTO>> GetWorkflowByModuleOutcomeAndUpdateDate(string module, string outcome, DateTime date)
        {
            var baseUrl = _config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.WORKFLOW_BASE_URL).Value;
            var path = _config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.workflowClosedByUpdateDate).Value;
            Dictionary<string, dynamic> queries = new Dictionary<string, dynamic>()
            {
                {"module" , module },
                {"outcome" , outcome },
                {"updated_at" , date }
            };
            string finalUrl = Helper.UrlBuilderByQuery(baseUrl + path, queries);
            var apiResponse = await _httpClient.GetAsync(finalUrl);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<WorkflowDTO>>(resp);
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception($"Error fetching GetWorkflowPendingTasks Api did not fetch response {resp}");
            }
        }

        public async Task<List<ProjectFullDetailsResponse>> GetAllProjectByCreationDate(DateOnly currentDate)
        {
            try
            {
                var baseUrl = _config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.ProjectServiceBaseUrl).Value;
                var path = _config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.Get_All_Project_By_CreationDate).Value;
                Dictionary<string, dynamic> queries = new Dictionary<string, dynamic>()
                {
                    {"creationDate" , Convert.ToString(currentDate) }
                };
                string finalUrl = $"{baseUrl}{path}?creationDate={currentDate.ToString("yyyy-MM-dd")}";
                //_httpClient.Timeout = TimeSpan.FromMinutes(10) ;
                HttpClient c = new HttpClient();

                var apiResponse = await c.GetAsync(finalUrl);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ProjectFullDetailsResponse>>(resp);
                }
                else
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    throw new Exception($"Error fetching GetAllProjectByCreationDate Api did not fetch response {resp}");
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<string> GetUserInfoByUserEmailId(string emailId)
        {
            try
            {
                //var environmentVariables = Environment.GetEnvironmentVariables();
                //var baseurl = Convert.ToString(environmentVariables["IdentityServiceBaseUrl"]);
                //var getUserInfoFromIdentity = Convert.ToString(environmentVariables["GetUserInfoFromIdentity"]);
                var baseUrl = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.IdentityServiceBaseUrl).Value);
                var path = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.GetUserInfoFromIdentity).Value);
                //_logger.LogInformation("---------------------------------------------------------");
                //_logger.LogInformation(baseurl + getUserInfoFromIdentity + $"?email_id={email}");
                var content = new StringContent(JsonConvert.SerializeObject(emailId), Encoding.UTF8, "application/json");
                //var apiResponse = await _httpClient.PostAsync(baseurl + getEmployeePreferenceDetailsByEmailsPath, content);
                var apiResponse = await _httpClient.GetAsync(baseUrl + path + $"?email_id={emailId}");

                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    return resp;
                }
                else
                {
                    //_logger.LogInformation("Response:-" + await apiResponse.Content.ReadAsStringAsync());
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    throw new Exception($"Error fetching GetUserInfoByUserEmailId {resp} ");
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "");
                throw;
            }
        }
        public async Task<string> GetUsersByUsersEmail(List<string> users)
        {
            try
            {
                var baseUrl = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.IdentityServiceBaseUrl).Value);
                var path = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.GETUSERBYEMAILS).Value);
                var content = new StringContent(JsonConvert.SerializeObject(users), Encoding.UTF8, "application/json");
                var apiResponse = await _httpClient.PostAsync(baseUrl + path, content);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    return resp;
                }
                else
                {
                    //_logger.LogInformation("Response:-" + await apiResponse.Content.ReadAsStringAsync());
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    throw new Exception($"Error fetching GetUserInfoByUserEmailId {resp} ");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<MarketPlaceProjectDetailDTO>> GetMarketPlaceProjectListByPublishDate(DateOnly publishDate)
        {
            var environmentVaribles = Environment.GetEnvironmentVariables();
            string baseUrl = _config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.MarketPlaceBaseUrl).Value;
            string path = _config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.GetProjectListedInMarketplaceByProjectListingDate).Value;

            string dateStr = publishDate.ToDateTime(TimeOnly.MinValue).ToString("dd-MM-yyyy");

            var finalUrl = $"{baseUrl}{path}?MarketPlacePublishDate={dateStr}";
            var response = await _httpClient.GetAsync(finalUrl);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                List<MarketPlaceProjectDetailDTO> finalResp = JsonConvert.DeserializeObject<List<MarketPlaceProjectDetailDTO>>(result);
                return finalResp;
            }
            else
            {
                var result = await response.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetMarketPlaceProjectListByPublishDate Something went wrong in fetching data -" + result);
            }
        }

        public async Task<List<IdentityUserResponseDTO>> GetUserDetailsByUserEmailId(List<string> emailId)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");
                var baseUrl = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.IdentityServiceBaseUrl).Value);
                var path = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.GetUserDetailsFromIdentity).Value);
                //_logger.LogInformation("---------------------------------------------------------");
                //_logger.LogInformation(baseurl + getUserInfoFromIdentity + $"?email_id={email}");
                var finalResponse = new List<IdentityUserResponseDTO>();


                var joinedEmails = string.Join(',', emailId);

                //getUserByNameOrEmailV6Request emails = new()
                //{
                //    name = joinedEmails
                //};
                //    var content = new StringContent(JsonConvert.SerializeObject(emails), Encoding.UTF8, "application/json");
                var apiResponse = await _httpClient.GetAsync(baseUrl + path + joinedEmails);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadFromJsonAsync<List<IdentityUserResponseDTO>>();
                    finalResponse.AddRange(resp);
                }
                else
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    Console.WriteLine("Error fetching GetUserDetailsByUserEmailId URL:" + baseUrl + path + "response -->" + response);
                }
                return finalResponse;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "");
                throw;
            }
        }
    }
}
