using RMT.Allocation.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.IHttpServices
{
    public interface IConfigurationHttpService
    {
        Task<List<ConfigInfoDTO>> GetConfigurationByExpertiesNameAndGroupName(string keySelector, string groupName);
        Task<List<ConfigurationGroup>> GetProjectConfigurationByConfigGroupAndConfigType(string groupName, string configType);
    }
}
