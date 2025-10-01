using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Employee.Application.DTOs;
using System.Text;

namespace RMT.Employee.Application.Services
{
    public class AllocationService : IAllocationService
    {                
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public AllocationService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<AllocationByEmailResponse>> GetAllocationsByEmail(string email)
        {
            try
            {
                var baseUrl = Convert.ToString(_config.GetSection("MicroserviceApiSettings").GetSection("allocationapiUrl").Value);
                var path = Convert.ToString(_config.GetSection("MicroserviceApiSettings").GetSection("GetAllocationsByEmail").Value);
                var content = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");
                path = path + "?EmpEmail=" + "EMP002__abhishek.grover01@nagarro.com";
                //path = path + "?EmpEmail=" + email;
                var response = await _httpClient.GetAsync(baseUrl + path);
                if (response.IsSuccessStatusCode)
                {
                    string resp = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<AllocationByEmailResponse>>(resp);
                }
                else
                {
                    throw new Exception("Error fetching GetWorkflowDetailsByItemId" + response);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }    
    }
}
