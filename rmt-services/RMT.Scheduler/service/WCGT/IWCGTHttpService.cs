using Microsoft.Extensions.Logging;
using RMT.Scheduler.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.service.WCGT
{
    public interface IWCGTHttpService
    {
        Task<List<GTJobDTO>> GetJobsListFromWCGT();

        Task<List<string>> GetProjectBudgetByModifiedDateRange(string currentToken, DateTime startDate, DateTime endDate, ILogger _logger);
    }
}
