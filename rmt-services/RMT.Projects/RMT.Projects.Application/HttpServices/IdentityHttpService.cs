using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Projects.Application.HttpServices.DTOs;
using RMT.Projects.Application.IHttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.HttpServices
{
    public class IdentityHttpService : IIdentityHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public IdentityHttpService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<List<EmployeeResponseDTO>> GetEmployeesListByInputEmail(string inputEmail)
        {
            try
            {
                string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityServiceBaseUrl").Value;
                string getIdentityUserListByStringInput = _config.GetSection("MicroserviceApiSettings").GetSection("GetUserByNameEmail").Value;
                var url = baseurl + getIdentityUserListByStringInput + inputEmail;
                var apiResponse = await _httpClient.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    var finalResponse = JsonConvert.DeserializeObject<List<EmployeeResponseDTO>>(response);
                    return finalResponse;
                }
                else
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    throw new Exception("Error Fetching Employee Information From Identity-"+ response);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
