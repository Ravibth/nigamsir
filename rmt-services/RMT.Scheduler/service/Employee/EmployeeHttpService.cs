using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RMT.Scheduler.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static RMT.Scheduler.Constants.Constant;

namespace RMT.Scheduler.service.Employee
{
    public class EmployeeHttpService : IEmployeeHttpService
    {
        private readonly ITokenService _tokenService;
        public EmployeeHttpService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<bool> AddEmployeeProjectMapping(List<AddEmployeeProjectMappingRequestDto> request, ILogger _logger)
        {
            var environmentVaribles = Environment.GetEnvironmentVariables();
            string GetGatewayBaseUrl = Convert.ToString(environmentVaribles[EnvAppSettingConstants.GET_GATEWAY_BASE_URL]);
            string AddEmployeeProjectMappingUrl = Convert.ToString(environmentVaribles[EnvAppSettingConstants.AddEmployeeProjectMapping]);

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var finalUrl = GetGatewayBaseUrl + AddEmployeeProjectMappingUrl;
            var _httpClient = await _tokenService.GetCustomHttpClient();
            var apiResponse = await _httpClient.PostAsync(finalUrl, content);
            string response = await apiResponse.Content.ReadAsStringAsync();

            if (apiResponse.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                _logger.LogError($"Exception occurred while AddEmployeeProjectMapping :- {response}");
                throw new Exception($"Exception occurred while AddEmployeeProjectMapping :- {response}");
            }
        }
    }
}
