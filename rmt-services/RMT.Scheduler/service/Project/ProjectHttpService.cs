using Azure;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using Newtonsoft.Json;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static RMT.Scheduler.Constants.Constant;

namespace RMT.Scheduler.service.Project
{
    public class ProjectHttpService : IProjectHttpService
    {
        private readonly ITokenService _tokenService;
        public ProjectHttpService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<List<GetOfferingSolutionsByJobCodeResponseDTO>> GetOfferingSolutionsByJobCode(List<string> requestJobCodes, ILogger _logger)
        {
            var environmentVaribles = Environment.GetEnvironmentVariables();
            string GetGatewayBaseUrl = Convert.ToString(environmentVaribles[EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
            string GetOfferingSolutionsByJobCodeUrl = Convert.ToString(environmentVaribles[EnvAppSettingConstants.GetOfferingSolutionsByJobCode]);

            var content = new StringContent(JsonConvert.SerializeObject(requestJobCodes), Encoding.UTF8, "application/json");

            var finalUrl = GetGatewayBaseUrl + GetOfferingSolutionsByJobCodeUrl;
            var _httpClient = await _tokenService.GetCustomHttpClient();

            try
            {
                var apiResponse = await _httpClient.PostAsync(finalUrl, content);
                string response = await apiResponse.Content.ReadAsStringAsync();

                if (apiResponse.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<GetOfferingSolutionsByJobCodeResponseDTO>>(response);
                }
                else
                {
                    throw new Exception($"Exception occurred while GetOfferingSolutionsByJobCode :- {response}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while GetOfferingSolutionsByJobCode :- {ex.Message}");
                throw new Exception($"Exception occurred while GetOfferingSolutionsByJobCode :- {ex.Message}");
            }
        }
    }
}
