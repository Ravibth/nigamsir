using Microsoft.Extensions.Logging;
using RMT.Scheduler.service.Configurations.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.service.Configurations
{
    public interface IConfigurationService
    {
        Task<List<ProjectConfiguration>> GetConfigurationByConfigGroupType(string expertiesName, string configurationGroup, string token, ILogger _logger);

    }
}
