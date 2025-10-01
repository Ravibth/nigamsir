using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Ocelot.Middleware;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.API.Middlewares
{
    /// <summary>
    /// TokenAuthenticationMiddleware
    /// </summary>
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        // private ILogger _logger;

        /// <summary>
        /// TokenAuthenticationMiddleware constructore 
        /// </summary>
        /// <param name="next"></param>
        public TokenAuthenticationMiddleware(RequestDelegate next
        // , ILogger logger
        )
        {
            _next = next;
            // _logger = logger;
        }
        /// <summary>
        /// Middleware invike method
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // _logger.LogInformation("--TokenAuthenticationMiddleware------Invoke--Start");
                if (context.Request.HttpContext.WebSockets.IsWebSocketRequest)
                {
                    var requestQuery = context.Request.Query.ToList();
                    var accessToken = requestQuery.Where(m => m.Key.Equals("access_token")).FirstOrDefault().Value;
                    if (accessToken != "")
                    {
                        context.Request.Headers.Authorization = "Bearer " + accessToken;
                    }
                }
                context.Request.Headers.TryGetValue("Authorization", out var token);
                if (string.IsNullOrEmpty(token))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token is required");
                }

                // _logger.LogInformation("--TokenAuthenticationMiddleware------Invoke--End");

                await _next(context);

                // _logger.LogInformation("--TokenAuthenticationMiddleware------next Invoked");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // _logger.LogError(ex, "--TokenAuthenticationMiddleware--Invoke--Failed");
                throw;
            }
        }
    }
}
