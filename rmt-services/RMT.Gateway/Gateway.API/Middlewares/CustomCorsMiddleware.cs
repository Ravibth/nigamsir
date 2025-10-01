using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Gateway.API.Middlewares
{
    /// <summary>
    /// CustomCorsMiddleware
    /// </summary>
    public class CustomCorsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _trustedOrigins;

        /// <summary>
        /// CustomCorsMiddleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="trustedOrigins"></param>
        public CustomCorsMiddleware(RequestDelegate next, List<string> trustedOrigins)
        {
            _next = next;
            _trustedOrigins = trustedOrigins;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            string originHeader = "Origin";// "Access-Control-Allow-Origin";
            string responseHeader = "Access-Control-Allow-Origin";
            if (context.Request.Headers.TryGetValue(originHeader, out var origin))
            {
                if (_trustedOrigins.Contains(origin))
                {
                    context.Response.Headers[originHeader] = origin;
                    await _next(context);
                }
                else
                {
                    context.Response.StatusCode = ((int)HttpStatusCode.PreconditionFailed);
                    await context.Response.WriteAsync($"{originHeader} is not valid!");
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}
