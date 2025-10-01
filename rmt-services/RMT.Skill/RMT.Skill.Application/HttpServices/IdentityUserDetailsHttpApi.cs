using Microsoft.Extensions.Configuration;
using RMT.Skill.Application.DTOs;
using RMT.Skill.Application.IHttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.HttpServices
{
    public class IdentityUserDetailsHttpApi : IIdentityUserDetailsHttpApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public IdentityUserDetailsHttpApi(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<List<IdentityUserResponseDTO>> GetEmployeesDataHttpApiQuery(List<string> request)
        {
            try
            {
                // var content = new StringContent(JsonConvert.SerializeObject(request.email), Encoding.UTF8, "application/json");
                // _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                _httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");

                string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityApiUrl").Value;
                string getUsers = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityGetUsersPath").Value;
                string emailId = string.Join(",", request);
                var apiResponse = await _httpClient.GetAsync(baseurl + getUsers + emailId);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadFromJsonAsync<List<IdentityUserResponseDTO>>();
                    return resp;
                }
                else
                {
                    throw new Exception("Error fetching GetEmployeesDataHttpApiQuery URL:" + baseurl + getUsers);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
