using Microsoft.Extensions.Logging;
using RMT.Scheduler.DTOs;
using RMT.Scheduler.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.service.Project
{
    public interface IProjectHttpService
    {
        Task<List<GetOfferingSolutionsByJobCodeResponseDTO>> GetOfferingSolutionsByJobCode(List<string> requestJobCodes, ILogger _logger);
    }
}
