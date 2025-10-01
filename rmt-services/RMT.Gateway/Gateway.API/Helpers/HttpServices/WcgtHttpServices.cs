using Gateway.API.Dtos;
using Gateway.API.Helpers.IHttpServices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Ocelot.Requester;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gateway.API.Helpers.HttpServices
{
    public class WcgtHttpServices : IWcgtHttpServices
    {
        //private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public WcgtHttpServices(HttpClient httpClient, IConfiguration config)
        {
            //_httpClient = httpClient;
            _config = config;
        }
        public async Task<GTBUExpertiesGroupDTO> GetBUTreeMappingListByMID(string mid)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WCGTService").Value;
            string GetBUTreeMappingListByMIDUrl = _config.GetSection("MicroserviceApiSettings").GetSection("GetBUTreeMappingListByMID").Value;


            Dictionary<string, dynamic> queries = new()
            {
                { "mid", mid }
            };
            var url = Utility.UrlBuilderByQuery(baseurl + GetBUTreeMappingListByMIDUrl, queries);

            HttpClient _httpClient = new();

            var apiResponse = await _httpClient.GetAsync(url);
            var resp = await apiResponse.Content.ReadAsStringAsync();
            if (apiResponse.IsSuccessStatusCode)
            {
                var response = JsonConvert.DeserializeObject<GTBUExpertiesGroupDTO>(resp);
                return response;
            }
            else
            {
                throw new Exception($"GetBUTreeMappingListByMID->Error Fetching:{resp}");
            }
        }
        public async Task<List<CompetencyMasterDTO>> GetCompetencyMasterByMid(string mid)
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WCGTService").Value;
            string getBuExpertiesPath = _config.GetSection("MicroserviceApiSettings").GetSection("GetCompetencyList").Value;
            var query = new Dictionary<string, dynamic>();
            query.Add("competency_leader_mid", mid);
            HttpClient _httpClient = new();
            var url = Utility.UrlBuilderByQuery(baseurl + getBuExpertiesPath, query);
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
