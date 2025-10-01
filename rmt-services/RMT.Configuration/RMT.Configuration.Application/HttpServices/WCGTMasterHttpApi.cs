using Microsoft.Extensions.Configuration;
using RMT.Configuration.Application.IHttpServices;
using RMT.Configuration.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Application.HttpServices
{
    public class WCGTMasterHttpApi : IWCGTMasterHttpApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public WCGTMasterHttpApi(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<List<WCGTBUTreeMappingDTO>> GetWCGTBUTreeMappingListApiQuery()
        {
            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("WcgtApiUrl").Value;
            string getWcgtMaster = _config.GetSection("MicroserviceApiSettings").GetSection("GetBUTreeMappingList").Value;

            var apiResponse = await _httpClient.GetAsync(baseurl + getWcgtMaster);

            if (apiResponse.IsSuccessStatusCode)
            {
                List<WCGTBUTreeMappingDTO> finalResponse = await apiResponse.Content.ReadFromJsonAsync<List<WCGTBUTreeMappingDTO>>();
                return finalResponse;
            }
            else
            {
                throw new Exception("Error fetching GetWCGTBUTreeMappingListApiQuery");
            }

        }
    }
}
