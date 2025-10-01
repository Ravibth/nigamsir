using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Abstractions;
using System.Threading.Tasks;

namespace Gateway.API.Helpers
{
    public class AppLogger
    {
        private readonly TelemetryClient _telemetryClient;
        private readonly RequestDelegate _next;

        public AppLogger(RequestDelegate next, TelemetryClient telemetryClient)
        {
            _next = next;
            _telemetryClient = telemetryClient;
        }

        public async Task Invoke(HttpContext context)
        {
            // Log a custom event
            _telemetryClient.TrackEvent("CustomEventName");
            _telemetryClient.TrackTrace("Hello Event111111111");

            await _next(context);
        }

    }
}
