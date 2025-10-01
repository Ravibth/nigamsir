using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class TokenService : ITokenService
    {
        public async Task<string> GetToken()
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            var clientId = Convert.ToString(environmentVariables[Constants.Constants.EnvAppSettingConstants.ClientId]);
            var clientSecret = Convert.ToString(environmentVariables[Constants.Constants.EnvAppSettingConstants.ClientSecret]);
            var authority = Convert.ToString(environmentVariables[Constants.Constants.EnvAppSettingConstants.Authority]);
            var audience = Convert.ToString(environmentVariables[Constants.Constants.EnvAppSettingConstants.Audience]);
            var tenantId = Convert.ToString(environmentVariables[Constants.Constants.EnvAppSettingConstants.TenantId]);

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

        //public async Task<HttpClient> GetCustomHttpClient()
        //{
        //    var environmentVariables = Environment.GetEnvironmentVariables();
        //    var client = AzureHttpClient.GetAzureHttpClient(true);
        //    var clientId = Convert.ToString(environmentVariables[Constants.Constants.EnvAppSettingConstants.ClientId]);
        //    string currentToken = await GetToken();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", currentToken);
        //    client.DefaultRequestHeaders.Add("Accept", "Application/JSON");
        //    //client.DefaultRequestHeaders.Add(Constants.Constant.ICustomHeaderSystem, "system.@gt.com");
        //    client.DefaultRequestHeaders.Add(Constants.Constants.ICustomHeaderSystem, $"{clientId}@rms.com");
        //    return client;
        //}

        public async Task<HttpClient> GetCustomHttpClient(string token)
        {
            var environmentVariables = Environment.GetEnvironmentVariables();
            var client = AzureHttpClient.GetAzureHttpClient(true);
            var clientId = Convert.ToString(environmentVariables["clientId"]);

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            client.DefaultRequestHeaders.Add("Accept", "Application/JSON");
            client.DefaultRequestHeaders.Add("custom_system_header", $"{clientId}@rms.com");
            return client;
        }
    }
}
