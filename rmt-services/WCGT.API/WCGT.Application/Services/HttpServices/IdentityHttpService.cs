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
    public class SuperCoachModel
    {
        public string? supercoach_mid { get; set; }
    }
    public class SuperCoachModelDto
    {
        [JsonProperty("allocdelegate_mid")]
        public string[] AllocDelegateMid { get; set; }
    }
    public class IdentityHttpService:IIdentityHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public IdentityHttpService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<SuperCoachModel>> GetSuperCoachMid(string mid)
        {
            //return new List<string>{"EMP002"};
            string? baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("IdentityApiUrl").Value;
            string? path = _config.GetSection("MicroserviceApiSettings").GetSection("SupercoachDelegatesList").Value;
            var req = new SuperCoachModelDto
            {
                AllocDelegateMid = new[] { mid }
            };
            var content = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
            var apiResponse = await _httpClient.PostAsync(baseurl + path, content);
            var resp = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                List<SuperCoachModel>? finalResponse = JsonConvert.DeserializeObject<List<SuperCoachModel>>(resp);
                return finalResponse;
            }
            else
            {
                throw new Exception($"Error fetching GetSuperCoachMid:- {resp}");
            }
        }
    }
}
