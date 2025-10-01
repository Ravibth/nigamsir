using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace RMT.Scheduler.service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> GetToken()
        {
            var clientId = _config.GetSection("ClientSecretSettings").GetSection("clientId").Value;
            var clientSecret = _config.GetSection("ClientSecretSettings").GetSection("clientSecret").Value;
            var authority = _config.GetSection("ClientSecretSettings").GetSection("authority").Value;
            var augience = _config.GetSection("ClientSecretSettings").GetSection("augience").Value;
            var tenantId = _config.GetSection("ClientSecretSettings").GetSection("tenantId").Value;
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
                throw new Exception($"Some Went Wrong: {ex.Message}", ex);
            }
        }
    }
}
