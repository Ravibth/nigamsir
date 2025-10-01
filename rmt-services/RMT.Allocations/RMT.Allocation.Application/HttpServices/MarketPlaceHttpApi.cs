using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices
{
    public class MarketPlaceHttpApi : IMarketPlaceHttpApi
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        public MarketPlaceHttpApi(HttpClient httpClient, IConfiguration config)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<string[]> GetListOfUsersInterestedByPipelineCode(string pipelineCode, string jobCode)
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "Application/JSON");

            Dictionary<string, dynamic> queries = new()
            {
                { "pipelineCode", pipelineCode },
                { "jobCode", string.IsNullOrEmpty(jobCode)? string.Empty : jobCode  }
            };

            string baseurl = _config.GetSection("MicroserviceApiSettings").GetSection("MarketplaceApiUrl").Value;
            string getListOfUsersInterestedByPipelineCodeUrl = _config.GetSection("MicroserviceApiSettings").GetSection("GetListOfUsersInterestedByPipelineCode").Value;


            var url = Helper.UrlBuilderByQuery(baseurl + getListOfUsersInterestedByPipelineCodeUrl, queries);
            var apiResponse = await _httpClient.GetAsync(url);
            if (apiResponse.IsSuccessStatusCode)
            {
                var resp = await apiResponse.Content.ReadAsStringAsync();
                List<string> response = JsonConvert.DeserializeObject<List<string>>(resp);
                return response.ToArray();
            }
            else
            {
                string response = await apiResponse.Content.ReadAsStringAsync();
                throw new Exception("Error fetching GetListOfUsersInterestedByPipelineCode URL:" + url + ", Response:" + response);
            }
        }
    }
}
