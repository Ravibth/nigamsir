using Microsoft.Extensions.Logging;
using RMT.Scheduler.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.service.Oracle
{
    public interface IOracleService
    {
        Task<List<OracleTimesheetResponseDto>> GetTimesheetDataFromOracle(string startTime, ILogger _logger);
    }
}
