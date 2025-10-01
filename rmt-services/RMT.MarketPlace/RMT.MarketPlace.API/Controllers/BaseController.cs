namespace RMT.MarketPlace.API.Controllers
{
    public class BaseController
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
    }
}
