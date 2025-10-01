using Microsoft.Extensions.Logging;
using RMT.Configuration.Domain.DTO;
using RMT.Configuration.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Infrastructure.Repositories
{
    public class LoggerRepository : ILoggerRepository
    {
        private readonly ILogger<LoggerRepository> logger;

        public LoggerRepository(ILogger<LoggerRepository> logger)
        {
            this.logger = logger;
        }

        public Task<bool> LogObject(LoggerDTO logObj)
        {
            bool flag = false;
            try
            {
                string logMsg = string.Format("RMT-UI-LogContent-LogLevel->{0};Category->{1};Function->{2};Message->{3};StackTrace->{4}", logObj.LogLevel, logObj.Category, logObj.Function, logObj.Message, logObj.StackTrace);
                logger.LogInformation(logMsg, logObj.LogObjects);

                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return Task.FromResult(flag);
        }
    }
}
