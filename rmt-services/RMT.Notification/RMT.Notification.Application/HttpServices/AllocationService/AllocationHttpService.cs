using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Notification.Application.Helpers;
using RMT.Notification.Application.HttpServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Notification.Application.Constants.Constants;

namespace RMT.Notification.Application.HttpServices.AllocationService
{
    public class AllocationHttpService : IAllocationHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AllocationHttpService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<List<RequisitionResponse>> GetRequistionByDate(DateTime CreatedAt, DateTime ModifiedAt)
        {

            var baseUrl = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.RESOURCE_ALLOCATION_BASE_URI).Value);
            var path = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.GET_REQUISTION_BY_DATE).Value);

            string queries = "?ModifiedAt=" + ModifiedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + "&CreatedAt=" + CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            var url = baseUrl + path + queries;
            var allRequistion = await _httpClient.GetAsync(url);
            if (allRequistion.IsSuccessStatusCode)
            {
                var response = await allRequistion.Content.ReadAsStringAsync();
                var requstionList = JsonConvert.DeserializeObject<List<RequisitionResponse>>(response);
                return requstionList;
            }
            else
            {
                throw new Exception("Unable to fetch Requistion Content");
            }
        }
        public async Task<List<PublishedAllocationResponse>> GetAllocationByDate(DateTime CreatedAt, DateTime ModifiedAt)
        {

            var baseUrl = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.RESOURCE_ALLOCATION_BASE_URI).Value);
            var path = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.GET_ALLOCATION_BY_DATE).Value);

            //Dictionary<string, dynamic> queries = new()
            //{
            //    {"ModifiedAt", ModifiedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") },
            //    {"CreatedAt" , CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") }
            //};
            string queries = "?ModifiedAt=" + ModifiedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") + "&CreatedAt=" + CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            var url = baseUrl + path + queries; // Helper.UrlBuilderByQuery($"{baseUrl}{path}", queries);
            var allocations = await _httpClient.GetAsync(url);
            if (allocations.IsSuccessStatusCode)
            {
                var response = await allocations.Content.ReadAsStringAsync();
                var requstionList = JsonConvert.DeserializeObject<List<PublishedAllocationResponse>>(response);
                return requstionList;
            }
            else
            {
                throw new Exception("Unable to fetch Allocation Content");
            }
        }
    }
}
