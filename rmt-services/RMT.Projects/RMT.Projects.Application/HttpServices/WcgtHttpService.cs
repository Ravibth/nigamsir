using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Projects.Application.HttpServices.DTOs;
using RMT.Projects.Application.IHttpServices;
using RMT.Projects.Domain.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RMT.Projects.Application.HttpServices
{
    public class WcgtHttpService : IWcgtHttpService
    {

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IWcgtHttpService _wcgtHttpService;

        public WcgtHttpService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<GetBuExpertiesDTO> GetBUExpertiesByMID(string mid)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WCGTMicroserviceBaseApiUrl").Value;
            string getBuExpertiesPath = _config.GetSection("MicroserviceApiSettings").GetSection("GetBuExpertiesByMid").Value;
            var query = new Dictionary<string, dynamic>();
            query.Add("mid", mid);
            var url = Helper.UrlBuilderByQuery(baseurl + getBuExpertiesPath, query);
            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                GetBuExpertiesDTO finalResponse = JsonConvert.DeserializeObject<GetBuExpertiesDTO>(resp);
                return finalResponse;
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching the response of api - " + getBuExpertiesPath + "-" + resp);
            }
        }
        public async Task<List<CompetencyMasterDTO>> GetCompetencyMasterByMid(string mid)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WCGTMicroserviceBaseApiUrl").Value;
            string getBuExpertiesPath = _config.GetSection("MicroserviceApiSettings").GetSection("GetCompetencyList").Value;
            var query = new Dictionary<string, dynamic>();
            query.Add("competency_leader_mid", mid);
            var url = Helper.UrlBuilderByQuery(baseurl + getBuExpertiesPath, query);
            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                List<CompetencyMasterDTO> finalResponse = JsonConvert.DeserializeObject<List<CompetencyMasterDTO>>(resp);
                return finalResponse;
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching the response of api - " + getBuExpertiesPath + "-" + resp);
            }
        }
    }
}
