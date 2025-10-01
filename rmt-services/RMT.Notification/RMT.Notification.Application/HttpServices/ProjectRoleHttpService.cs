using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Notification.Application.HttpServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.HttpServices
{
    public class ProjectRoleHttpService : IProjectRoleHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public ProjectRoleHttpService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<List<RoleEmailsByPipelineCodeResponse>> GetEmailOfProjectRoles(string[] inputEmail,string pipelineCode, string jobCode)
        {
            try
            {
                string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("ProjectServiceBaseUrl").Value;
                string getProjectRoles = _config.GetSection("MicroserviceApiSettings").GetSection("GetEmailOfProjectRoles").Value;
                var url = baseurl + getProjectRoles;
                var content = new StringContent(JsonConvert.SerializeObject(inputEmail), Encoding.UTF8, "application/json");
                var apiResponse = await _httpClient.PostAsync(url,content);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var response = await apiResponse.Content.ReadAsStringAsync();
                    var finalResponse = JsonConvert.DeserializeObject<List<RoleEmailsByPipelineCodeResponse>>(response);
                    return finalResponse;
                }
                else
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    throw new Exception($"Error Fetching Employee Information From Project {resp}");
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
