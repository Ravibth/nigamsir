using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RMT.Allocation.API.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Exception logging method
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="args"></param>
        internal void LogException(Exception ex, params object?[] args)
        {
            _logger.LogError(ex, this.GetType().FullName, args);
        }

        internal void LogInformation(string ex, params object?[] args)
        {
            _logger.LogInformation(ex, this.GetType().FullName, args);
        }
    }
}
