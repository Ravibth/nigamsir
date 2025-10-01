using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Services.DTO;
using WCGT.Application.Services.IHttpServices;

namespace WCGT.Application.Services.HttpServices
{
    public class AllocationHttpService: IAllocationHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public AllocationHttpService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<List<PublishedResourceAllocationDayResponse>> PublishedResourceAllocationDays(List<string> empEmail , DateTime startDate , DateTime endDate)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("allocationBaseUrl").Value;
            string path = _config.GetSection("MicroserviceApiSettings").GetSection("publishedResourceAllocationDays").Value;
            PublishedResourceAllocationDaysRequestDto req = new()
            {
                StartDate = startDate,
                EndDate = endDate,
                EmpEmail = empEmail
            };
            var content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + path, content);
            var resp = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                var finalResponse = JsonConvert.DeserializeObject<List<PublishedResourceAllocationDayResponse>>(resp);
                return finalResponse;
            }
            else
            {
                throw new Exception($"Error fetching GetProjectDetailsByCode:- {resp}");
            }
        }
         
    }
}
