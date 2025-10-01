using RMT.Notification.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.HttpServices.ProjectConfigurationService
{
    public interface IProjectConfigurationService
    {
        Task<List<ProjectConfigurationResponse>> GetProjectConfiguration(string expertiesName, string configurationGroup);
    }
}
