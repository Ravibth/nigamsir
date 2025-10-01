using Microsoft.Identity.Client;
using RMT.Scheduler.Constants;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RMT.Scheduler.service
{
    public class TokenService : ITokenService
    {
        public async Task<string> GetToken()
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            var clientId = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.ClientId]);
            var clientSecret = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.ClientSecret]);
            var authority = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.Authority]);
            var audience = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.Audience]);
            var tenantId = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.TenantId]);

            var cca = ConfidentialClientApplicationBuilder
                        .Create(clientId)
                        .WithClientSecret(clientSecret)
                        .WithAuthority(new Uri(authority))
                        .Build();
            var scopes = new string[] { $"{clientId}/.default" };
            try
            {

                var result = await cca.AcquireTokenForClient(scopes).ExecuteAsync();
                return result.AccessToken;
            }
            catch (Exception ex)
            {
                throw new Exception($"Some Went Wrong: {ex.Message}, Not able to get token from toke api");
            }
        }


        public async Task<HttpClient> GetCustomHttpClient()
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            var client = AzureHttpClient.GetAzureHttpClient(true);
            var clientId = Convert.ToString(environmentVariables[Constant.EnvAppSettingConstants.ClientId]);
            string currentToken = await GetToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentToken);
            client.DefaultRequestHeaders.Add("Accept", "Application/JSON");
            //client.DefaultRequestHeaders.Add(Constants.Constant.ICustomHeaderSystem, "system.@gt.com");
            client.DefaultRequestHeaders.Add(Constants.Constant.ICustomHeaderSystem, $"{clientId}@rms.com");
            return client;
        }

    }
}
