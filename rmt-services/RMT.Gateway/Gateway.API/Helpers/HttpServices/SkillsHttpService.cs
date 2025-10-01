using Gateway.API.Dtos;
using Gateway.API.Helpers.IHttpServices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.API.Helpers.HttpServices
{
    /// <summary>
    /// Skill Microservice Api
    /// </summary>
    public class SkillsHttpService : ISkillsHttpService
    {
        //private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="config"></param>
        public SkillsHttpService(HttpClient httpClient, IConfiguration config)
        {
            //_httpClient = httpClient;
            _config = config;
        }

        /// <summary>
        /// Update User Skill Status After Supercoach/Co-supercoach accepts/rejects the skill
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> UpdateUserSkillStatusAfterWorkflow(List<UpdateUserSkillStatusAfterWorkflowRequestDTO> requests)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("SkillMicroserviceBaseUrl").Value;
            string UpdateUserSkillStatusAfterWorkflowUrl = _config.GetSection("MicroserviceApiSettings").GetSection("UpdateUserSkillStatusAfterWorkflow").Value;
            var content = new StringContent(JsonConvert.SerializeObject(requests), Encoding.UTF8, "application/json");
            HttpClient _httpClient = new();
            var apiResponse = await _httpClient.PutAsync(baseurl + UpdateUserSkillStatusAfterWorkflowUrl, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                return true;
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception($"Error UpdateUserSkillStatusAfterWorkflow {resp}");
            }
        }
    }
}
