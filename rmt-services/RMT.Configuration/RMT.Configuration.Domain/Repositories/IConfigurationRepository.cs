using RMT.Configuration.Domain.DTO;
using RMT.Configuration.Domain.Entities;

namespace RMT.Configuration.Domain.Repositories
{
    public interface IConfigurationRepository
    {
        //Task<ConfigurationGroup> CreateConfigurationGroup(ConfigurationGroup configurationGroup);

        //Task<ProjectConfiguration> CreateProjectConfiguration(ProjectConfiguration projectConfiguration);

        //Task<List<ConfigurationGroup>> GetAllConfigurationGroup();

        Task<List<ConfigurationGroupMaster>> GetAllConfigurationGroupMaster(string? configGroup, string? configKey, string? configType);

        //Task<List<ProjectConfiguration>> GetAllProjectConfigurations();

        Task<List<ProjectConfiguration>> GetProjectConfigurationsByConfigGroupAndConfigType(string ConfigGroup, string ConfigType, List<WCGTBUTreeMappingDTO> buTreeMapping, string attributeName);

        Task<List<ProjectConfiguration>> UpdateProjectConfiguration(List<ConfigurationGroup> projectConfigurations, string configurationType, List<WCGTBUTreeMappingDTO> buTreeMapping);

        //Task<List<ConfigurationGroup>> GetConfigurationGroupsByGroupNameAndConfigType(string configGroup, string configType, List<WCGTBUTreeMappingDTO> buTreeMapping, string attributeName);

        //Task<List<ProjectConfiguration>> GetProjectConfigurationByExpertiesNameAndConfigGroup(string expertiesName, string configurationGroupName);

        //Task<Dictionary<string, string>> GetAllBuExpertiesDict();

        Task<Dictionary<string, string>> GetApplicationLevelSettingsDict(List<string>? keys);

        //Task<List<ConfigurationGroup>> GetConfigurationGroupsByConfigNameConfigKeyAndConfigType(string configGroup, string configKey, string configType, List<WCGTBUTreeMappingDTO> buTreeMapping, string attributeName);
        Task<List<ConfigurationMaster>> GetConfigurationMasterByConfigGroupAndConfigTypeAsync(string? configGroup, string? configType);
        Task<ConfigurationMainBreakup> UpdateConfigurationBreakup(List<UpdateConfigurationBreakupRequestDTO> request, string email);
        Task<ConfigurationMaster> GetConfigurationMasterByConfigGroupAndConfigKey(string configGroup, string configKey, string attributeName, string selectorConfigType, List<WCGTBUTreeMappingDTO> buTreeMapping);
        Task<ConfigurationMainBreakup> GetProjectConfigurationByExpertiesNameAndConfigGroup(string buOfferingName, string configurationGroupName);
    }

}