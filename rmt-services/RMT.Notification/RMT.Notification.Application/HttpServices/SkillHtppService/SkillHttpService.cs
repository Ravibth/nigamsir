using Microsoft.Extensions.Configuration;
using RMT.Notification.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Notification.Application.Constants.Constants;

namespace RMT.Notification.Application.HttpServices.SkillHtppService
{
    public class SkillHttpService : ISkillHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public SkillHttpService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<string> GetUserSkillById(string guid)
        {
            try
            {
               // var environmentVariables = Environment.GetEnvironmentVariables();
                //var baseurl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.SKILLS_BASE_URI]);
                //var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.GETUSERSKILLBYID]);

                var baseUrl = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.SKILLS_BASE_URI).Value);
                var path = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.GETUSERSKILLBYID).Value);
                // string userSkillPath = _config.GetSection("MicroserviceApiSettings").GetSection(path).Value;
                Dictionary<string, dynamic> queries = new()
                {
                    { "id", Convert.ToString( guid)

                    },
                    {
                      "approvals",true
                    }
                };

                var url = Helper.UrlBuilderByQuery($"{baseUrl}{path}", queries);


                var apiResponse = await _httpClient.GetAsync(url);
                if (apiResponse.IsSuccessStatusCode)
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    return resp;
                    //return JsonConvert.DeserializeObject<ResourceAllocationDetailsResponse>(resp);
                }
                else
                {
                    var resp = await apiResponse.Content.ReadAsStringAsync();
                    throw new Exception($"Error fetching  GetResourceAllocationDetailsByGuid Allocation Data {resp}");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
