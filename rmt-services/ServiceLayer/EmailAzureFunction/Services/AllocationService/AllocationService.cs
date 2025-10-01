using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceLayer.DTOs;
using ServiceLayer.Services.AllocationService.DTOs;
using ServiceLayer.Services.SyncEventService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static ServiceLayer.Constants.Constants;

namespace ServiceLayer.Services.AllocationService
{
    public class AllocationService : IAllocationService
    {
        private readonly ILogger<AllocationService> _logger;

        public AllocationService(ILogger<AllocationService> log)
        {
            _logger = log;
        }
        public async Task<ResourceAllocationDetailsResponse> GetResourceAllocationDetailsByGuid(string guid, string token)
        {
            try
            {
                var environmentVariables = Environment.GetEnvironmentVariables();
                var baseurl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
                var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.GET_RESOURCE_ALLOCATION_DETAILS_BY_GUID]);

                Dictionary<string, dynamic> queries = new()
                {
                    { "guid", Convert.ToString( guid) }
                };
                var _httpClient = AzureHttpClient.GetAzureHttpClient(true);

                var url = Helper.UrlBuilderByQuery($"{baseurl}{path}", queries);
                var tokenSplit = token.Trim().Split(" ");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, tokenSplit[1]);

                _logger.LogInformation("--ServiceBus--GetResourceAllocationDetailsByGuid URL--" + url);

                var apiResponse = await _httpClient.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation("--ServiceBus--GetResourceAllocationDetailsByGuid Success--");
                    return JsonConvert.DeserializeObject<ResourceAllocationDetailsResponse>(resp);
                }
                else
                {
                    _logger.LogInformation("--ServiceBus--GetResourceAllocationDetailsByGuid Failed--");
                    throw new Exception($"Error fetching  GetResourceAllocationDetailsByGuid Allocation Data {await apiResponse.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<AllocationRolloverResponseDTO> RolloverAllocationByPipelineCode(WcgtJobDTO wcgtJob, Project rmsProjectJob, string token)
        {
            try
            {
                var rolloverAllocationByPipelineCode = Environment.GetEnvironmentVariable("RolloverAllocationByPipelineCode");
                var baseGatewayUrl = Environment.GetEnvironmentVariable("BaseGatewayUrl");
                var url = baseGatewayUrl + rolloverAllocationByPipelineCode;
                ProjectRolloverRequestDTO projectRolloverRequestDTO = new ProjectRolloverRequestDTO()
                {
                    JobCode = wcgtJob.job_code,
                    PipelineCode = wcgtJob.pipeline_code,
                    PipelineStartDate = wcgtJob.job_start_date != null ? Convert.ToDateTime(wcgtJob.job_start_date) : Convert.ToDateTime(wcgtJob.start_date),
                    PipelineOldStartDate = Convert.ToDateTime(rmsProjectJob.StartDate),
                    Message = string.Empty
                };
                var _httpClient = AzureHttpClient.GetAzureHttpClient(true);
                List<ProjectRolloverRequestDTO> listData = new List<ProjectRolloverRequestDTO>();
                listData.Add(projectRolloverRequestDTO);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, token);
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, tokenSplit[0]);
                //_httpClient.DefaultRequestHeaders.Add(Constants.Constants.ICustomHeaderSystem, "system.@gt.com");

                string json = JsonConvert.SerializeObject(listData);
                _logger.LogInformation("--ServiceBus--RolloverAllocationByPipelineCode URL--" + url);
                _logger.LogInformation("--ServiceBus--RolloverAllocationByPipelineCode Request--" + json);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var apiResponse = await _httpClient.PostAsync(url, content);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation("--ServiceBus--RolloverAllocationByPipelineCode Success--");
                    return JsonConvert.DeserializeObject<AllocationRolloverResponseDTO>(resp);
                }
                else
                {
                    _logger.LogInformation("--ServiceBus--RolloverAllocationByPipelineCode Failed--");
                    throw new Exception("Error executing Roll Forward" + apiResponse);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateDesignation(DesignationUpdateDTO request, string token)
        {
            try
            {
                _logger.LogInformation("--ServiceBus--UpdateDesignation--" + ServiceBusActions.DESIGNATION_CHANGE_EVENT, request);

                var updateDesignationPath = Environment.GetEnvironmentVariable("UpdateDesignationAllocationPath");
                var baseGatewayUrl = Environment.GetEnvironmentVariable("BaseGatewayUrl");
                var url = baseGatewayUrl + updateDesignationPath;
                var updateDesignationRequest = new List<DesignationUpdateDTO>();
                updateDesignationRequest.Add(request);

                var designationReq = new DesignationUpdateRequestDTO { UpdateDesignationCostDTO = updateDesignationRequest };
                var _httpClient = AzureHttpClient.GetAzureHttpClient(true);

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, token);
                _httpClient.DefaultRequestHeaders.Add(Constants.Constants.ICustomHeaderSystem, "system.@gt.com");

                string json = JsonConvert.SerializeObject(designationReq);
                _logger.LogInformation("--ServiceBus--UpdateDesignation URL--" + url);
                _logger.LogInformation("--ServiceBus--UpdateDesignation Request--" + json);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var apiResponse = await _httpClient.PostAsync(url, content);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation("--ServiceBus--UpdateDesignation Success--");

                }
                else
                {
                    _logger.LogInformation("--ServiceBus--UpdateDesignation Failed--");
                    throw new Exception("Error executing Update Designation" + apiResponse);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<AllocationWithLeavesAndResourceDTO>> GetAllocationInformationByLeaves(string empEmail, DateTime startDate, DateTime endDate, string token)
        {
            try
            {
                var environmentVariables = Environment.GetEnvironmentVariables();
                var baseurl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
                var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.GET_ALLOCATION_BY_LEAVES]);
                string tokenString = token;
                if (tokenString.Contains("Bearer"))
                {
                    tokenString = tokenString.Split(" ")[1];
                }
                var request = new GetAllocationByEmployeeEmailAndLeaveDTO
                {
                    EmployeeEmail = empEmail,
                    LeaveStartDate = startDate,
                    LeaveEndDate = endDate
                };
                List<GetAllocationByEmployeeEmailAndLeaveDTO> res = new();
                res.Add(request);
                var _httpClient = AzureHttpClient.GetAzureHttpClient(true);

                var url = new UriBuilder(baseurl + path);
                //var tokenSplit = token.Trim().Split(" ");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, tokenString);

                _logger.LogInformation("--ServiceBus--GetAllocationInformationByLeaves URL--" + url);

                var allocationContent = new StringContent(JsonConvert.SerializeObject(res), Encoding.UTF8, "application/json");
                var allocationResult = await _httpClient.PostAsync(url.Uri, allocationContent);
                var allocationResponse = await allocationResult.Content.ReadAsStringAsync();
                if (allocationResult.IsSuccessStatusCode)
                {
                    _logger.LogInformation("--ServiceBus--GetAllocationInformationByLeaves Success--");
                    return JsonConvert.DeserializeObject<List<AllocationWithLeavesAndResourceDTO>>(allocationResponse);
                }
                else
                {
                    _logger.LogInformation("--ServiceBus--GetAllocationInformationByLeaves Failed--");
                    throw new Exception($"Error fetching  GetAllocationInformationByLeaves Allocation Data {await allocationResult.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<ResourceAllocationResponseDTO>> GetActiveAllocationByEmail(string empEmail, string token)
        {
            try
            {
                var environmentVariables = Environment.GetEnvironmentVariables();
                var baseurl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
                var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.GET_ALLOCATION_BY_EMAIL]);
                var finalToken = token;
                if (token.Contains(Constants.Constants.Bearer))
                {
                    finalToken = token.Split(" ")[1];
                }

                var _httpClient = AzureHttpClient.GetAzureHttpClient(true);
                Dictionary<string, dynamic> queries = new()
                {
                    { "EmpEmail", Convert.ToString( empEmail) }
                };
                var url = Helper.UrlBuilderByQuery($"{baseurl}{path}", queries);
                var tokenSplit = token.Trim().Split(" ");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, finalToken);

                _logger.LogInformation("--ServiceBus--GetActiveAllocationByEmail URL--" + url);


                var allocationResult = await _httpClient.GetAsync(url);
                if (allocationResult.IsSuccessStatusCode)
                {
                    var allocationResponse = await allocationResult.Content.ReadAsStringAsync();
                    _logger.LogInformation("--ServiceBus--GetActiveAllocationByEmail Success--");
                    return JsonConvert.DeserializeObject<List<ResourceAllocationResponseDTO>>(allocationResponse);
                }
                else
                {
                    _logger.LogInformation("--ServiceBus--GetActiveAllocationByEmail Failed--");
                    throw new Exception($"Error fetching  GetActiveAllocationByEmail Allocation Data {await allocationResult.Content.ReadAsStringAsync()}");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
