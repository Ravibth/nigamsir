using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Employee.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Application.Services
{
    public class SkillService: ISkillService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public SkillService(HttpClient httpClient , IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            
        }
        public async Task<List<UserSkillsResponseWithSkillDTO>> GetUserApprovedSkillByEmail(List<string> email)
        {
            var baseUrl = Convert.ToString(_config.GetSection("MicroserviceApiSettings").GetSection("skillapiUrl").Value);
            var path = Convert.ToString(_config.GetSection("MicroserviceApiSettings").GetSection("GetUserApprovedSkillByEmail").Value);
            var content = new StringContent(JsonConvert.SerializeObject(email), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(baseUrl + path , content);
            if (response.IsSuccessStatusCode)
            {
                string resp =  await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UserSkillsResponseWithSkillDTO>>(resp);
            }
            else
            {
                throw new Exception("Error fetching GetWorkflowDetailsByItemId" + response);
            }
        }
    }
}
