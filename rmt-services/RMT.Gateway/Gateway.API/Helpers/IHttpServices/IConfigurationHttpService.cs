using Gateway.API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.API.Helpers.IHttpServices
{
    public interface IConfigurationHttpService
    {
        Task<List<ConfigInfoDTO>> GetConfigurationByExpertiesNameAndGroupName(string expertiesName, string groupName);
        Task<List<ConfigurationGroup>> GetConfigurationGroupByGroupNameAndConfigType(string groupName, string configType);
        Task<List<ConfigurationGroup>> GetConfigurationByConfigGroupConfigKeyAndConfigType(string groupName, string configKey, string configType);
    }
}
