using Microsoft.Identity.Client;
using Newtonsoft.Json;
using RMT.Scheduler.Constants;
using RMT.Scheduler.DTOs.GT360;
using RMT.Scheduler.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.service
{
    public class GTTokenService : IGTTokenService
    {
        public async Task<GT360TokenResponseDto> GetToken()
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            var gt360TokenApiUrl = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.GT360TokenApiUrl]);
            var gt360UserName = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.GT360UserName]);
            var gt360Password = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.GT360Password]);

            var _httpClient = new HttpClient();
            GT360TokenRequestDto requestObj = new GT360TokenRequestDto()
            {
                USERNAME = gt360UserName,
                PASSWORD = gt360Password,
            };
            var httpRequestContent = new StringContent(JsonConvert.SerializeObject(requestObj), Encoding.UTF8, "application/json");
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var httpResponse = await _httpClient.PostAsync(gt360TokenApiUrl, httpRequestContent);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<GT360TokenResponseDto>(responseString);
                return tokenResponse;
            }
            else
            {
                throw new Exception("Unable to fetch Token");
            }

            return null;

        }

        public HttpClient GetCustomHttpClient()
        {
            var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.DefaultRequestHeaders.Add("Accept", "Application/JSON");
            return client;
        }
    }
}
