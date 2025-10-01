using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Notification.Application.HttpServices.DTO;
using RMT.Notification.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static RMT.Notification.Application.Constants.Constants;

namespace RMT.Notification.Application.HttpServices.MarketPlaceService
{
    public class MarketPlaceHttpService : IMarketPlaceHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public MarketPlaceHttpService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<MarketPlaceProjectDetaillsIntrestDTO>> GetMarketPlaceDetailsIntrest()
        {

            var baseUrl = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.MARKETPLACE_BASE_URI).Value);
            var path = Convert.ToString(_config.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.MarketplaceDetailsIntrestURI).Value);

            var url = baseUrl + path;
            var marketPlace = await _httpClient.GetAsync(url);
            if (marketPlace.IsSuccessStatusCode)
            {
                var response = await marketPlace.Content.ReadAsStringAsync();
                var requstionList = JsonConvert.DeserializeObject<List<MarketPlaceProjectDetaillsIntrestDTO>>(response);
                return requstionList;
            }
            else
            {
                throw new Exception("Unable to fetch Requistion Content");
            }
        }
    }
}
