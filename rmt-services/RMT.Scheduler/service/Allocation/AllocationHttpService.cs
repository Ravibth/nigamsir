using Azure;
using Azure.Core;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using Newtonsoft.Json;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.DTOs.Response;
using RMT.Scheduler.service.Allocation.DTOs;
using RMT.Scheduler.service.Configurations.DTO;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static RMT.Scheduler.Constants.Constant;

namespace RMT.Scheduler.service
{
    public class AllocationHttpService : IAllocationHttpService
    {
        private readonly ITokenService _tokenService;
        public AllocationHttpService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<bool> UpdateActualAllocationTime(List<OracleTimesheetResponseDto> oracleTimesheetResponse, ILogger _logger)
        {
            var environmentVaribles = Environment.GetEnvironmentVariables();
            string GetGatewayBaseUrl = Convert.ToString(environmentVaribles[EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
            string UpdatePublishAllocationActualEffortsUrl = Convert.ToString(environmentVaribles[EnvAppSettingConstants.UpdatePublishAllocationActualEfforts]);

            var content = new StringContent(JsonConvert.SerializeObject(oracleTimesheetResponse), Encoding.UTF8, "application/json");

            var finalUrl = GetGatewayBaseUrl + UpdatePublishAllocationActualEffortsUrl;
            var _httpClient = await _tokenService.GetCustomHttpClient();
            try
            {
                var apiResponse = await _httpClient.PostAsync(finalUrl, content);

                string response = await apiResponse.Content.ReadAsStringAsync();

                if (apiResponse.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Exception occurred while UpdateActualAllocationTime with status code:- {apiResponse.StatusCode} and reason :- {response}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while UpdateActualAllocationTime :- {ex.Message}");
                throw new Exception($"Exception occurred while UpdateActualAllocationTime :- {ex.Message}");
            }
        }

        //public async Task<List<GetProjectResponse>> GetAllProjects()
        //{

        //    var EnvVarDictionary = Environment.GetEnvironmentVariables();
        //    var GetGatewayBaseUrl = Convert.ToString(EnvVarDictionary[Constants.Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
        //    var GetProjectstUrl = Convert.ToString(EnvVarDictionary[Constants.Constant.EnvAppSettingConstants.GET_ALL_PROJECTS]);

        //    var _httpClient = await _tokenService.GetCustomHttpClient();
        //    var allProjectsResult = await _httpClient.GetAsync(GetGatewayBaseUrl + GetProjectstUrl);
        //    if (allProjectsResult.IsSuccessStatusCode)
        //    {
        //        var projectsResponse = await allProjectsResult.Content.ReadAsStringAsync();
        //        var projectsList = JsonConvert.DeserializeObject<List<GetProjectResponse>>(projectsResponse);
        //        return projectsList;
        //    }
        //    else
        //    {
        //        throw new Exception("Unable to fetch ProjectsList Content");
        //    }
        //}

        //public async Task<List<RequisitionResponse>> GetRequistionByDate(DateTime CreatedAt, DateTime ModifiedAt)
        //{

        //    var EnvVarDictionary = Environment.GetEnvironmentVariables();
        //    var GetGatewayBaseUrl = Convert.ToString(EnvVarDictionary[Constants.Constant.EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
        //    var GetRequstionUrl = Convert.ToString(EnvVarDictionary[Constants.Constant.EnvAppSettingConstants.GET_REQUISTION_BY_DATE]);
        //    Dictionary<string, dynamic> queries = new()
        //    {
        //        {"ModifiedAt", ModifiedAt },
        //        {"CreatedAt" , CreatedAt }
        //    };
        //    var url = Helper.UrlBuilderByQuery($"{GetGatewayBaseUrl}{GetRequstionUrl}", queries);
        //    var _httpClient = await _tokenService.GetCustomHttpClient();
        //    var allRequistion = await _httpClient.GetAsync(url);
        //    if (allRequistion.IsSuccessStatusCode)
        //    {
        //        var response = await allRequistion.Content.ReadAsStringAsync();
        //        var requstionList = JsonConvert.DeserializeObject<List<RequisitionResponse>>(response);
        //        return requstionList;
        //    }
        //    else
        //    {
        //        throw new Exception("Unable to fetch ProjectsList Content");
        //    }
        //}
    }
}
