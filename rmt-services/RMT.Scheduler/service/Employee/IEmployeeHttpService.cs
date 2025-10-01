using Microsoft.Extensions.Logging;
using RMT.Scheduler.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.service.Employee
{
    public interface IEmployeeHttpService
    {
        Task<bool> AddEmployeeProjectMapping(List<AddEmployeeProjectMappingRequestDto> request, ILogger _logger);
    }
}
