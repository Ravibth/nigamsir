using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using MediatR;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Application.Utils;
using RMT.Allocation.Application.DTOs;
using System.Net.Http.Json;

namespace RMT.Allocation.Application.HttpServices
{
    public class ProjectServiceHttpApi : IProjectServiceHttpApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ProjectServiceHttpApi(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<ProjectDTO>> GetProjectDetailsByProjectCode(List<KeyValuePair<string, string>> projectCode, string token)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectService").Value;
            string getProjectDetailsByProjectCodePath = _config.GetSection("MicroserviceApiSettings").GetSection("GetProjectDetailsByProjectCodePath").Value;
            var urlBuilder = new UriBuilder(baseurl + getProjectDetailsByProjectCodePath);
            //var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            //foreach (var item in projectCode)
            //{
            //    query["pipelineCode"] = item.Key;
            //    query["jobCode"] = item.Value;
            //}
            //urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token.Split(" ")[1]);
            var content = new StringContent(JsonConvert.SerializeObject(projectCode), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(url, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                var finalResponse = JsonConvert.DeserializeObject<List<ProjectDTO>>(resp);
                return finalResponse;
            }
            else
            {
                string response = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetProjectDetailsByProjectCode" + response);
            }
        }

        public async Task<List<ResourceRequestorsWithPipelineCodesDTO>> GetResourceRequestorsByPipelineCodes(List<KeyValuePair<string, string?>> pipelineCodes, string token)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectService").Value;
            string getProjectDetailsByProjectCodePath = _config.GetSection("MicroserviceApiSettings").GetSection("GetResourceRequestorEmailsListByPipelineCode").Value;
            var urlBuilder = new UriBuilder(baseurl + getProjectDetailsByProjectCodePath);
            var content = new StringContent(JsonConvert.SerializeObject(pipelineCodes), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token.Split(" ")[1]);
            var apiResponse = await _httpClient.PostAsync(urlBuilder.Uri, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                var finalResponse = JsonConvert.DeserializeObject<List<ResourceRequestorsWithPipelineCodesDTO>>(resp);
                return finalResponse;
            }
            else
            {
                string response = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetResourceRequestorsByPipelineCodes :- " + response);
            }
        }

        public async Task<ProjectDTO> GetProjectDetailsByCode(string pipelineCode, string? jobCode)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectMicroserviceBaseApiUrl").Value;
            string getProjectDetailsByProjectCodePath = _config.GetSection("MicroserviceApiSettings").GetSection("GetProjectByCodePath").Value;
            var urlBuilder = new UriBuilder(baseurl + getProjectDetailsByProjectCodePath);
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            query["pipelineCode"] = pipelineCode;
            query["jobCode"] = jobCode;
            urlBuilder.Query = query.ToString();
            var apiResponse = await _httpClient.GetAsync(urlBuilder.ToString());
            var resp = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                var finalResponse = JsonConvert.DeserializeObject<ProjectDTO>(resp);
                return finalResponse;
            }
            else
            {
                throw new Exception($"Error fetching GetProjectDetailsByCode:- {resp}");
            }
        }
        public async Task<bool> AddSuperCoachProjectRole(AddSuperCoachProjectRoleRequest req)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectMicroserviceBaseApiUrl").Value;
            string addSupercoachPath = _config.GetSection("MicroserviceApiSettings").GetSection("AddSuperCoachProjectRole").Value;
            var content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + addSupercoachPath , content);
            var resp = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"Error fetching GetProjectDetailsByCode:- {resp}");
            }
        }

        public async Task<List<ProjectBudgetDto>> GetProjectBudget(string pipelineCode, string? jobCode)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectApiUrl").Value;
            string getProjectBudgetPath = _config.GetSection("MicroserviceApiSettings").GetSection("GetDesignationBudgetPath").Value;

            var urlBuilder = new UriBuilder(baseurl + getProjectBudgetPath);
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            query["pipelineCode"] = pipelineCode;
            query["jobCode"] = jobCode; ;

            urlBuilder.Query = query.ToString();
            string url = urlBuilder.ToString();
            //_httpClient.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("Bearer", token.Split(" ")[1]);
            var apiResponse = await _httpClient.GetAsync(url);
            var resp = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {

                var finalResponse = JsonConvert.DeserializeObject<List<ProjectBudgetDto>>(resp);
                return finalResponse;
            }
            else
            {
                throw new Exception($"Error fetching GetProjectBudget :- {resp} ");
            }
        }

        public async Task<List<AddProjectUserRole>> AddProjectUserWithRole(Handlers.CommandHandlers.AddProjectUserCommand projecctRoles)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectMicroserviceBaseApiUrl").Value;
            string getProjectDetailsByProjectCodePath = _config.GetSection("MicroserviceApiSettings").GetSection("AddProjectUserWithRolePath").Value;
            var content = new StringContent(JsonConvert.SerializeObject(projecctRoles), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + getProjectDetailsByProjectCodePath, content);
            var response = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                throw new Exception($"Error AddProjectUserWithRole :- {response}");
            }
        }

        public async Task<List<AddProjectUserRole>> RemoveProjectUserWithRole(Handlers.CommandHandlers.AddProjectUserCommand projecctRoles)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectMicroserviceBaseApiUrl").Value;
            string getProjectDetailsByProjectCodePath = _config.GetSection("MicroserviceApiSettings").GetSection("RemoveProjectUserWithRolePath").Value;
            var content = new StringContent(JsonConvert.SerializeObject(projecctRoles), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + getProjectDetailsByProjectCodePath, content);
            var resp = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                throw new Exception($"Error RemoveProjectUserWithRole :- {resp}");
            }
        }

        public async Task<List<RoleEmailsByPipelineCodeResponse>> GetEmployeeRoleByByPipelineJobCodes(List<PipelineCodeAndRolesDto> pipelineCodeAndRolesDto)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectMicroserviceBaseApiUrl").Value;
            string path = _config.GetSection("MicroserviceApiSettings").GetSection("GetEmployeeRoleByByPipelineJobCodes").Value;

            //var baseurl = Environment.GetEnvironmentVariable("BaseGatewayUrl");
            //var path = Environment.GetEnvironmentVariable("GetRolesEmailByPipelineCodesAndRoles");
            var url = baseurl + path;
            var content = new StringContent(JsonConvert.SerializeObject(pipelineCodeAndRolesDto), Encoding.UTF8, "application/json");

            //var tokenSplit = token.Trim().Split(" ");
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, tokenSplit[1]);

            var apiResponse = await _httpClient.PostAsync(url, content);
            var resp = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<RoleEmailsByPipelineCodeResponse>>(resp);
            }
            else
            {
                throw new Exception($"Error fetching GetEmployeeRoleByByPipelineJobCodes :- {resp} ");
            }
        }

        public async Task<List<ProjectFullDetailsResponse>> GetProjectFullDetailsByUniqueCodes(List<KeyValuePair<string, string?>> projectUniqueCodes)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectMicroserviceBaseApiUrl").Value;
            string path = _config.GetSection("MicroserviceApiSettings").GetSection("GetProjectFullDetailsByUniqueCodes").Value;

            //var baseurl = Environment.GetEnvironmentVariable("BaseGatewayUrl");
            //var path = Environment.GetEnvironmentVariable("GetRolesEmailByPipelineCodesAndRoles");
            var url = baseurl + path;
            var content = new StringContent(JsonConvert.SerializeObject(projectUniqueCodes), Encoding.UTF8, "application/json");

            //var tokenSplit = token.Trim().Split(" ");
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, tokenSplit[1]);

            var apiResponse = await _httpClient.PostAsync(url, content);
            var resp = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<ProjectFullDetailsResponse>>(resp);
            }
            else
            {
                throw new Exception($"Error fetching GetProjectFullDetailsByUniqueCodes:- {resp}");
            }
        }

        public async Task<ProjectFullDetailsResponse> UpdateProjectRollOverStatus(UpdateProjectRolledOverDto updateProjectRollForwardDto)
        {
            //Update the project enddate based on roll forward days
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectMicroserviceBaseApiUrl").Value;
            string path = _config.GetSection("MicroserviceApiSettings").GetSection("UpdateProjectRollOverStatus").Value;

            var url = baseurl + path;
            var content = new StringContent(JsonConvert.SerializeObject(updateProjectRollForwardDto), Encoding.UTF8, "application/json");

            var apiResponse = await _httpClient.PostAsync(url, content);
            var resp = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ProjectFullDetailsResponse>(resp);
            }
            else
            {
                throw new Exception($"Error fetching UpdateProjectRollOverStatus:- {resp}");
            }
        }

        public async Task<List<ProjectFullDetailsResponse>> GetProjectListDataByUser(ProjectListRequestDTO requestDto)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectMicroserviceBaseApiUrl").Value;
            string path = _config.GetSection("MicroserviceApiSettings").GetSection("GetProjectListDataByUser").Value;

            var url = baseurl + path;
            var content = new StringContent(JsonConvert.SerializeObject(requestDto), Encoding.UTF8, "application/json");

            var apiResponse = await _httpClient.PostAsync(url, content);
            var resp = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<ProjectFullDetailsResponse>>(resp);
            }
            else
            {
                throw new Exception($"Error fetching UpdateProjectRollOverStatus:- {resp}");
            }
        }

        public async Task<bool> AddUpdateProjectRequisitionAllocation(string pipelineCode, string? jobCode, int requisitionCountAdded, int allocationCountAdded)
        {
            List<ProjectRequisitionAllocationRequestDTO> request = new()
            {
                new()
                {
                    pipelineCode = pipelineCode,
                    jobCode = jobCode,
                    requisitionCountAdded = requisitionCountAdded,
                    allocationCountAdded = allocationCountAdded
                }
            };

            string baseUrl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectService").Value;
            string AddUpdateProjectRequisitionAllocationCommandUrl = _config.GetSection("MicroserviceApiSettings").GetSection("AddUpdateProjectRequisitionAllocationCommand").Value;
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var url = baseUrl + AddUpdateProjectRequisitionAllocationCommandUrl;

            //_logger.LogInformation($"AddUpdateProjectRequisitionAllocation--{url}");
            HttpClient _httpClient = new();

            var apiResponse = await _httpClient.PostAsync(url, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
                //HttpStatusCode statusCode = apiResponse.StatusCode;
                //string reasonPhrase = apiResponse.ReasonPhrase;
                //var errorMessage = apiResponse.Content.ReadAsStringAsync().Result;
                //throw new Exception($"Error In AddUpdateProjectRequisitionAllocation status code :- {statusCode} reason phrase :- {reasonPhrase} error message :- {errorMessage}");
            }
        }

        //

    }
}
