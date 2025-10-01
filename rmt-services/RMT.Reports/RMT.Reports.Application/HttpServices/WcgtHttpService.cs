using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Reports.Application.DTO.Response;
using RMT.Reports.Application.IHttpServices;
using RMT.Reports.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Application.HttpServices
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

        public async Task<List<GTBUTreeMappingDTO>> GetBUTreeMappingList()
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WCGTMicroserviceBaseApiUrl").Value;
            string getBuExpertiesPath = _config.GetSection("MicroserviceApiSettings").GetSection("GetBUTreeMappingList").Value;
            var query = new Dictionary<string, dynamic>();
            var url = Helper.UrlBuilderByQuery(baseurl + getBuExpertiesPath, query);
            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                List<GTBUTreeMappingDTO> finalResponse = JsonConvert.DeserializeObject<List<GTBUTreeMappingDTO>>(resp);
                return finalResponse;
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching the response of api - " + getBuExpertiesPath + "-" + resp);
            }
        }

        public async Task<List<GTCompetencyDTO>> GetBUCompetencyListByMID(string mid)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WCGTMicroserviceBaseApiUrl").Value;
            string getBuExpertiesPath = _config.GetSection("MicroserviceApiSettings").GetSection("GetCompetencyListByMID").Value;
            var query = new Dictionary<string, dynamic>();
            if (!string.IsNullOrEmpty(mid))
            {
                query.Add("competency_leader_mid", mid);
            }
            var url = Helper.UrlBuilderByQuery(baseurl + getBuExpertiesPath, query);
            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                List<GTCompetencyDTO> finalResponse = JsonConvert.DeserializeObject<List<GTCompetencyDTO>>(resp);
                return finalResponse;
            }
            else
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching the response of api - " + getBuExpertiesPath + "-" + resp);
            }
        }


        public async Task<GetBuExpertiesDTO> GetBUTreeMappingListByMID(string mid)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WCGTMicroserviceBaseApiUrl").Value;
            string getBuExpertiesPath = _config.GetSection("MicroserviceApiSettings").GetSection("GetBUTreeMappingListByMID").Value;
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

        public async Task<List<GTEmployeeBaseDTO>> GetEmployeeBySuperCoachOrCSCByMID(string mid)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WCGTMicroserviceBaseApiUrl").Value;
            string getBuExpertiesPath = _config.GetSection("MicroserviceApiSettings").GetSection("GetEmployeeBySuperCoachOrCSC").Value;
            var query = new Dictionary<string, dynamic>();
            query.Add("emp_mid", mid);
            var url = Helper.UrlBuilderByQuery(baseurl + getBuExpertiesPath, query);
            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                List<GTEmployeeBaseDTO> finalResponse = JsonConvert.DeserializeObject<List<GTEmployeeBaseDTO>>(resp);
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
