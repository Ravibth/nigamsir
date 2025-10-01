using Gateway.API.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Text;
using Gateway.API.Helpers.IHttpServices;
using System.Net;
using Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper.DTOs;
using Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper;

namespace Gateway.API.Helpers.HttpServices
{
    public class AllocationHttpService : IAllocationHttpService
    {
        //private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IAllocationMiddlewareHelper _allocationMiddlewareHelper;
        public AllocationHttpService(HttpClient httpClient, IConfiguration config, IAllocationMiddlewareHelper allocationMiddlewareHelper)
        {
            //_httpClient = httpClient;
            _config = config;
            _allocationMiddlewareHelper = allocationMiddlewareHelper;
        }
        public async Task<HttpStatusCode> UpdateAllocationStatus(UpdateAllocationRequestDTO allocationStatus)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("AllocationService").Value;
            string updateAllocationStatus = _config.GetSection("MicroserviceApiSettings").GetSection("UpdateAllocationStatus").Value;
            var content = new StringContent(JsonConvert.SerializeObject(allocationStatus), Encoding.UTF8, "application/json");
            HttpClient _httpClient = new();
            var apiResponse = await _httpClient.PostAsync(baseurl + updateAllocationStatus, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return apiResponse.StatusCode;
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error Fetching UpdateAllocationStatus" + resp);
            }
        }
        public async Task<UpdateListOfAllocationStatusInResourceAllocationDetailsResponse> UpdateListOfAllocationStatus(List<UpdateAllocationRequestDTO> allocationStatus)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("AllocationService").Value;
            string updateAllocationStatus = _config.GetSection("MicroserviceApiSettings").GetSection("UpdateListOfAllocationStatusInResourceAllocationDetails").Value;

            var content = new StringContent(JsonConvert.SerializeObject(allocationStatus), Encoding.UTF8, "application/json");
            HttpClient _httpClient = new();
            var apiResponse = await _httpClient.PostAsync(baseurl + updateAllocationStatus, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<UpdateListOfAllocationStatusInResourceAllocationDetailsResponse>(resp);
                if (response.allocationDeletionCount > 0)
                {
                    await _allocationMiddlewareHelper.AddUpdateProjectRequisitionAllocationForUpdateListOfAllocationStatusInResourceAllocationDetails(response);
                }
                return response;
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception($"Error Fetching UpdateListOfAllocationStatus :{resp}");
            }
        }

        public async Task<bool> RemoveAllDraftAllocationsAfterUserIsDeactivated(List<string> emails)
        {
            string allocationBaseUrl = _config.GetSection("MicroserviceApiSettings").GetSection("AllocationService").Value;
            string removeAllDraftAllocationsAfterUserIsDeactivatedUrl = _config.GetSection("MicroserviceApiSettings").GetSection("RemoveAllDraftAllocationsAfterUserIsDeactivated").Value;

            Dictionary<string, dynamic> queries = new()
            {
                { "emails", emails }
            };
            var url = Utility.UrlBuilderByQuery(allocationBaseUrl + removeAllDraftAllocationsAfterUserIsDeactivatedUrl, queries);
            var content = new StringContent("", Encoding.UTF8, "application/json");

            HttpClient _httpClient = new();
            var apiResponse = await _httpClient.PostAsync(url, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return true;
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception($"RemoveAllDraftAllocationsAfterUserIsDeactivated->Error Fetching:{resp}");
            }
        }
    }
}
