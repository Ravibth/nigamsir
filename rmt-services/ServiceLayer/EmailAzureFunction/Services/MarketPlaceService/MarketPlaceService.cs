using Newtonsoft.Json;
using ServiceLayer.DTOs;
using ServiceLayer.Services.MarketPlaceService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static ServiceLayer.Constants.Constants;
using Microsoft.Extensions.Logging;

namespace ServiceLayer.Services.MarketPlaceService
{
    public class MarketPlaceService : IMarketPlaceService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<ProjectSubscription> logger;
        public MarketPlaceService(HttpClient httpClient, ILogger<ProjectSubscription> logger)
        {
            this.httpClient = AzureHttpClient.GetAzureHttpClient(true);
            this.logger = logger;
        }

        public async Task<MarketPlaceProjectDetailResponse> UpdateMarkeplaceProjectDetails(NotificationPayloadDTO serviceBusPayload)
        {
            //process Markeplace project details updated object and process the same

            var environmentVariables = Environment.GetEnvironmentVariables();
            var getwayBaseUrl = Convert.ToString(environmentVariables[EnvVariblesAppsetting.BASE_GATEWAY_URL]);
            var path = Convert.ToString(environmentVariables[EnvVariblesAppsetting.UPDATE_PROJECT_DETAILS_TO_MARKETPLACE]);

            UpdateMarketPlaceProjectDTO mpRequestDto = JsonConvert.DeserializeObject<UpdateMarketPlaceProjectDTO>(serviceBusPayload.payload);

            //todo check payload and serialize accordingly> convert to mp api expect format and then pass it on 
            var content = new StringContent(JsonConvert.SerializeObject(mpRequestDto), Encoding.UTF8, "application/json");
            var url = getwayBaseUrl + path;

            var tokenSplit = serviceBusPayload.token.Trim().Split(" ");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.Constants.Bearer, tokenSplit[1]);
            logger.LogInformation("--ServiceBus---MarketPlace---UpdateMarkeplaceProjectDetails->url-{0}-{1}", url, content);
            var apiResponse = await httpClient.PostAsync(url, content);
            if (apiResponse.IsSuccessStatusCode)
            {
                var responseStr = await apiResponse.Content.ReadAsStringAsync();
                logger.LogInformation("--ServiceBus---MarketPlace---UpdateMarkeplaceProjectDetails--- Success in service execution, StatusCode-{0}, Response-{1}", apiResponse.StatusCode, responseStr);
                MarketPlaceProjectDetailResponse finalResponse = JsonConvert.DeserializeObject<MarketPlaceProjectDetailResponse>(responseStr);
                return finalResponse;
            }
            else
            {
                var responseStr = await apiResponse.Content.ReadAsStringAsync();
                logger.LogInformation("--ServiceBus---MarketPlace---UpdateMarkeplaceProjectDetails--- Error in service execution, StatusCode{0}, Response-{1}", apiResponse.StatusCode, responseStr);
                throw new Exception("error in updating MarkeplaceProjectDetails ");
            }
        }
    }
}
