using Microsoft.Extensions.Logging;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.service
{
    public interface IAllocationHttpService
    {
        Task<bool> UpdateActualAllocationTime(List<OracleTimesheetResponseDto> oracleTimesheetResponse, ILogger _logger);
        //Task<List<GetProjectResponse>> GetAllProjects();
    }
}
