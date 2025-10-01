using Gateway.API.Dtos;
using Gateway.API.Helpers.IHttpServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.API.Helpers.HttpServices
{
    /// <summary>
    /// ProjectHttpService
    /// </summary>
    public class ProjectHttpService : IProjectHttpService
    {
        private readonly IConfiguration _config;
        //private readonly HttpClient _httpClient;
        private readonly ILogger<ProjectHttpService> _logger;

        /// <summary>
        /// ProjectHttpService
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="httpClient"></param>
        /// <param name="logger"></param>
        public ProjectHttpService(IConfiguration configuration, HttpClient httpClient, ILogger<ProjectHttpService> logger)
        {
            _config = configuration;
            //_httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// AddUpdateProjectRequisitionAllocation
        /// </summary>
        /// <param name="pipelineCode"></param>
        /// <param name="jobCode"></param>
        /// <param name="requisitionCountAdded"></param>
        /// <param name="allocationCountAdded"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

            _logger.LogInformation($"AddUpdateProjectRequisitionAllocation--{url}");
            HttpClient _httpClient = new();

            var apiResponse = await _httpClient.PostAsync(url, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                HttpStatusCode statusCode = apiResponse.StatusCode;
                string reasonPhrase = apiResponse.ReasonPhrase;
                var errorMessage = apiResponse.Content.ReadAsStringAsync().Result;
                throw new Exception($"Error In AddUpdateProjectRequisitionAllocation status code :- {statusCode} reason phrase :- {reasonPhrase} error message :- {errorMessage}");
            }
        }

        /// <summary>
        /// GetProjectDetailsByPipelineCode
        /// </summary>
        /// <param name="pipelineCode"></param>
        /// <param name="jobCode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ProjectInfoDTO> GetProjectDetailsByPipelineCode(string pipelineCode, string? jobCode)
        {
            string baseUrl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectService").Value;
            string GetProjectDetailByPipelineCode = _config.GetSection("MicroserviceApiSettings").GetSection("GetProjectDetailsByPipelineCode").Value;

            var url = baseUrl + GetProjectDetailByPipelineCode + $"?pipelineCode={Uri.EscapeDataString(pipelineCode)}&jobCode={Uri.EscapeDataString(jobCode)}";

            _logger.LogInformation($"GetProjectDetailsByPipelineCode--{url}");

            HttpClient _httpClient = new();

            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                var finalResp = JsonConvert.DeserializeObject<ProjectInfoDTO>(resp);
                return finalResp;
            }
            else
            {
                HttpStatusCode statusCode = apiResponse.StatusCode;
                string reasonPhrase = apiResponse.ReasonPhrase;
                var errorMessage = apiResponse.Content.ReadAsStringAsync().Result;
                throw new Exception($"Error In Fetching Project Information status code :- {statusCode} reason phrase :- {reasonPhrase} error message :- {errorMessage}");
            }
        }

        /// <summary>
        /// GetRolesEmailByPipelineCodesAndRoles
        /// </summary>
        /// <param name="pipelineCodeAndRolesDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<RoleEmailsByPipelineCodeResponse>> GetRolesEmailByPipelineCodesAndRoles(List<PipelineCodeAndRolesDto> pipelineCodeAndRolesDto)
        {
            string baseUrl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectService").Value;
            string path = _config.GetSection("MicroserviceApiSettings").GetSection("GetAppRolesEmailByPipelineCodes").Value;

            var url = baseUrl + path;
            _logger.LogInformation($"GetRolesEmailByPipelineCodesAndRoles--{url}");
            var content = new StringContent(JsonConvert.SerializeObject(pipelineCodeAndRolesDto), Encoding.UTF8, "application/json");

            HttpClient _httpClient = new();
            var apiResponse = await _httpClient.PostAsync(url, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RoleEmailsByPipelineCodeResponse>>(resp);
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetRolesEmailByPipelineCodesAndRoles " + resp);
            }
        }

        /// <summary>
        /// GetAllProjectRolesByCodes
        /// </summary>
        /// <param name="pipelineCodeAndRolesDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<ProjectRolesResponseDTO>> GetAllProjectRolesByCodes(PipelineCodeAndRolesDto pipelineCodeAndRolesDto)
        {
            string baseUrl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectService").Value;
            string path = _config.GetSection("MicroserviceApiSettings").GetSection("GetAllProjectRolesByCodes").Value;

            var url = baseUrl + path;
            _logger.LogInformation($"GetAllProjectRolesByCodes--{url}");
            var content = new StringContent(JsonConvert.SerializeObject(pipelineCodeAndRolesDto), Encoding.UTF8, "application/json");

            HttpClient _httpClient = new();
            var apiResponse = await _httpClient.PostAsync(url, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProjectRolesResponseDTO>>(resp);
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetAllProjectRolesByCodes " + resp);
            }
        }
    }
}
