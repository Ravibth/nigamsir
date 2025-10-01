using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RMT.Configuration.Domain;
using RMT.Configuration.Domain.DTO;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;
using RMT.Configuration.Infrastructure.Data;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace RMT.Configuration.Infrastructure.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        //create group  (done)
        //create config (done)
        //get group (done)
        //get config    (done)
        //get config with condition
        //update configGrp & projectConfig
        private readonly ConfigurationDbContext _configurationDbContext;

        public ConfigurationRepository(ConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext;
        }

        //public async Task<ConfigurationGroup> CreateConfigurationGroup(ConfigurationGroup configurationGroup)
        //{
        //    var response = await _configurationDbContext.Set<ConfigurationGroup>().AddAsync(configurationGroup);
        //    await _configurationDbContext.SaveChangesAsync();
        //    return response.Entity;
        //}

        //public async Task<ProjectConfiguration> CreateProjectConfiguration(ProjectConfiguration projectConfiguration)
        //{
        //    var response = await _configurationDbContext.Set<ProjectConfiguration>().AddAsync(projectConfiguration);
        //    await _configurationDbContext.SaveChangesAsync();
        //    return response.Entity;
        //}

        /// <summary>
        /// Get Project Configuration By Experties Name
        /// </summary>
        /// <param name="expertiesName"></param>
        /// <returns></returns>
        public async Task<ConfigurationMainBreakup> GetProjectConfigurationByExpertiesNameAndConfigGroup(string buOfferingName, string configurationGroupName)
        {
            try
            {
                var result = await _configurationDbContext.ConfigurationMainBreakup
                    .Include(m => m.ConfigurationMaster)
                    .FirstOrDefaultAsync(m =>
                        m.KeySelector.ToLower().Trim() == buOfferingName.ToLower().Trim()
                        && m.ConfigurationMaster.ConfigGroup.ToLower().Trim() == configurationGroupName.ToLower().Trim()
                    );

                if (result == null)
                {
                    return await _configurationDbContext.ConfigurationMainBreakup
                    .Include(m => m.ConfigurationMaster)
                    .FirstOrDefaultAsync(m =>
                        m.KeySelector.ToLower().Trim() == ConfigMasterKeyDisplayLabel.Default_Key_Selector.ToLower().Trim()
                        && m.ConfigurationMaster.ConfigGroup.ToLower().Trim() == configurationGroupName.ToLower().Trim()
                    );
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Some Thing Went Wrong", ex);
            }
        }

        private List<ProjectConfiguration> GetProjectConfigurationsWithDefaultValues(List<ProjectConfiguration> projectConfig, List<ConfigurationGroupMaster> configGroupMaster, string configType, string attributeName)
        {
            List<ProjectConfiguration> result = new List<ProjectConfiguration>(projectConfig.AsEnumerable());

            foreach (ConfigurationGroupMaster cgItem in configGroupMaster)
            {
                ProjectConfiguration? exist = projectConfig.Where(p => p.ConfigurationGroup != null && p.ConfigurationGroup.ConfigurationGroupMaster != null
                                                && p.ConfigurationGroup != null && p.ConfigurationGroup.ConfigurationGroupMaster != null
                                                && p.ConfigurationGroup?.ConfigurationGroupMaster?.ConfigGroup.Trim().ToUpper() == cgItem.ConfigGroup.Trim().ToUpper()
                                                && p.ConfigurationGroup?.ConfigurationGroupMaster?.ConfigKey.Trim().ToUpper() == cgItem.ConfigKey.Trim().ToUpper()
                                                && p.ConfigurationGroup?.ConfigType?.Trim().ToUpper() == configType.Trim().ToUpper()
                                                && p.AttributeName == attributeName
                                                ).OrderBy(a => a.ConfigurationGroup?.SortOrder).FirstOrDefault();

                //if project config not found add default config
                if (exist == null)
                {
                    result.Add(new ProjectConfiguration()
                    {
                        ConfigurationGroup = new ConfigurationGroup()
                        {
                            //ConfigGroup = cgItem.ConfigGroup,
                            //ConfigGroupDisplay = cgItem.ConfigGroupDisplay,
                            //ConfigKey = cgItem.ConfigKey,
                            //CongigDisplayText = cgItem.CongigDisplayText,
                            ConfigType = configType,
                            IsActive = true,
                            AllValue = cgItem.DefaultValue,
                            ConfigurationGroupMasterId = cgItem.Id,
                            ConfigurationGroupMaster = cgItem,
                            IsAll = false,
                            //ValueType = cgItem.ValueType,
                        },

                        AttributeValue = cgItem.DefaultValue,
                        AttributeName = attributeName,
                        IsActive = true,

                    });
                }
            }

            return result;
        }
        public async Task<List<ConfigurationGroupMaster>> GetAllConfigurationGroupMaster(string? configGroup, string? configKey, string? configType)
        {
            return await _configurationDbContext.ConfigurationGroupMasters.Where(d => d.IsActive
                    && (string.IsNullOrEmpty(configGroup) || EF.Functions.ILike(d.ConfigGroup, configGroup))
                    && (string.IsNullOrEmpty(configKey) || EF.Functions.ILike(d.ConfigKey, configKey))
                    //&& (string.IsNullOrEmpty(configType) || d.ConfigType == configType)
                    ).ToListAsync();
        }

        public async Task<List<ProjectConfiguration>> GetProjectConfigurationsByConfigGroupAndConfigType(string configGroup, string configType, List<WCGTBUTreeMappingDTO> buTreeMapping, string attributeName)
        {

            //api/Configuration/GetProjectConfigurationByConfigGroupAndConfigType?ConfigGroup=No_of_days_where_project_is_available_in_Marketplace&ConfigType=Expertise

            var resultProjConfig = await _configurationDbContext.ProjectConfigurations
                                                .Include(e => e.ConfigurationGroup).ThenInclude(t => t.ConfigurationGroupMaster)
                                                .Where(d => d.ConfigurationGroup != null && d.ConfigurationGroup.ConfigurationGroupMaster != null
                                                    && d.ConfigurationGroup.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim() == configGroup.ToUpper().Trim()
                                                    && d.ConfigurationGroup.ConfigType.Trim().ToUpper() == configType.ToUpper().Trim()
                                                    && d.ConfigurationGroup.IsActive == true
                                                    && d.IsActive == true
                                                    )
                                                .OrderBy(a => a.ConfigurationGroup.SortOrder).ToListAsync();

            List<ConfigurationGroupMaster> configGroupMaster = _configurationDbContext.ConfigurationGroupMasters
                                                                            .Where(d => d.IsActive == true
                                                                            && EF.Functions.ILike(d.ConfigGroup, configGroup)).ToList();

            if (configType.ToUpper().Trim() == Constants.ConfigTypeOfferings)
            {
                List<WCGTBUTreeMappingDTO> expertiesMasters = GetDistinctExpertises(buTreeMapping);

                foreach (var item in expertiesMasters)
                {
                    resultProjConfig = this.GetProjectConfigurationsWithDefaultValues(resultProjConfig, configGroupMaster, configType, item.offering);
                }

            }
            else if (configType.ToUpper().Trim() == Constants.ConfigTypeBusinessUnit)
            {
                List<WCGTBUTreeMappingDTO> buMasters = GetDistinctBUs(buTreeMapping);

                foreach (var item in buMasters)
                {
                    resultProjConfig = this.GetProjectConfigurationsWithDefaultValues(resultProjConfig, configGroupMaster, configType, item.bu);
                }
            }

            resultProjConfig = resultProjConfig.Where(d => d.ConfigurationGroup != null && d.ConfigurationGroup.ConfigurationGroupMaster != null
                                                    && d.ConfigurationGroup.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim() == configGroup.ToUpper().Trim()
                                                    && d.ConfigurationGroup.ConfigType.Trim().ToUpper() == configType.ToUpper().Trim()
                                                    && d.ConfigurationGroup.IsActive == true
                                                    && !string.IsNullOrEmpty(d.AttributeName)
                                                ).OrderBy(a => a.ConfigurationGroup?.SortOrder).ToList();
            return resultProjConfig;

            //return await _configurationDbContext.ProjectConfigurations.Include(x => x.ConfigurationGroup)
            //    .Where(d => d.ConfigurationGroup.ConfigGroup.ToString().ToUpper().Trim() == configGroup.ToString().ToUpper().Trim()
            //     && d.ConfigurationGroup.ConfigType.ToString().Trim().ToUpper() == configType.ToString().ToUpper().Trim()
            //&& d.ConfigurationGroup.IsActive == true
            //&& d.IsActive == true).ToListAsync();

            //{
            //valie1: 'true',
            //        value2: '8'
            //}
            //    .Select(s => new ProjectConfiguration
            //{
            //    Id = s.Id,
            //    AttributeName = s.AttributeValue,
            //    AttributeValue = s.AttributeValue,
            //    IsActive = s.IsActive,
            //    CreatedAt = s.CreatedAt,
            //    ModifiedAt = s.ModifiedAt,
            //    CreatedBy = s.CreatedBy,
            //    ModifiedBy = s.ModifiedBy,
            //    ConfigId = s.ConfigId,
            //    ConfigurationGroup = s.ConfigurationGroup
            //})
            //.Where(d => d.ConfigurationGroup.ConfigGroup.ToString().ToUpper().Trim() == configGroup.ToString().ToUpper().Trim()


            //&& d.ConfigurationGroup.ConfigType.ToString().Trim().ToUpper() == configType.ToString().ToUpper().Trim()
            //&& d.ConfigurationGroup.IsActive == true
            //&& d.IsActive == true).ToListAsync();
        }

        public async Task<List<ConfigurationMaster>> GetConfigurationMasterByConfigGroupAndConfigTypeAsync(string? configGroup, string? configType)
        {
            List<ConfigurationMaster> configurationMaster = await _configurationDbContext.ConfigurationMaster
                                                    .Include(e => e.ConfigurationMainBreakups)
                                                    .Where(e => (string.IsNullOrEmpty(configGroup) ? true : e.ConfigGroup.Equals(configGroup, StringComparison.OrdinalIgnoreCase))
                                                                && (string.IsNullOrEmpty(configType) ? true : e.SelectorConfigType.Equals(configType, StringComparison.OrdinalIgnoreCase))
                                                           )
                                                    .ToListAsync();
            return configurationMaster;
        }
        public async Task<ConfigurationMainBreakup> UpdateConfigurationBreakup(List<UpdateConfigurationBreakupRequestDTO> request, string email)
        {
            var deletedConfigurations = request.Where(e => e.IsActive == false);
            foreach (var item in deletedConfigurations)
            {
                var configMainBreakUp = await _configurationDbContext.ConfigurationMainBreakup
                                        .Where(t =>
                                                t.ConfigurationMasterId.ToString() == item.ConfigurationMasterId
                                                && t.KeySelector.ToLower().Trim() == item.KeySelector.ToLower().Trim()
                                        //&& t.ConfigurationMainBreakupMetaValues.Any(m => m.Key == selectedKey)
                                        ).FirstOrDefaultAsync();
                _configurationDbContext.ConfigurationMainBreakup.Remove(configMainBreakUp);
            }
            await _configurationDbContext.SaveChangesAsync();
            var updateOrAddConfigurations = request.Where(e => e.IsActive == true);
            foreach (var item in updateOrAddConfigurations)
            {
                var configurationMaster = await _configurationDbContext.ConfigurationMaster
                                            .Where(e => e.Id.ToString() == item.ConfigurationMasterId)
                                            .FirstOrDefaultAsync();
                string selectedKey = item.KeySelector;
                if (string.IsNullOrEmpty(item.KeySelector))
                {
                    throw new Exception("Key selector must be provided");
                }
                //string[] selectors = item.KeySelector.Split("|");
                //selectedKey = selectors[selectors.Length - 1];
                if (configurationMaster == null)
                {
                    throw new Exception("Configuration master does not exist");
                }
                var configMainBreakUp = await _configurationDbContext.ConfigurationMainBreakup
                                        .Where(t =>
                                                t.ConfigurationMasterId.ToString() == item.ConfigurationMasterId
                                                && t.KeySelector.ToLower().Trim() == selectedKey.ToLower().Trim()
                                        //&& t.ConfigurationMainBreakupMetaValues.Any(m => m.Key == selectedKey)
                                        ).FirstOrDefaultAsync();
                if (configMainBreakUp == null)
                {
                    //add the new value
                    ConfigurationMainBreakup configToAdd = new();
                    configToAdd.ConfigurationMasterId = configurationMaster.Id;
                    configToAdd.KeySelector = selectedKey;
                    configToAdd.CreatedAt = DateTime.UtcNow;
                    configToAdd.ModifiedAt = DateTime.UtcNow;
                    configToAdd.CreatedBy = email;
                    configToAdd.ModifiedBy = email;
                    List<ConfigurationMainBreakupMetaValue> metaData = new();
                    foreach (var configurationMetaValue in item.configurationMetaValues)
                    {
                        ConfigurationMainBreakupMetaValue meta = new ConfigurationMainBreakupMetaValue()
                        {
                            Key = configurationMetaValue.key,
                            Value = configurationMetaValue.value.ToString(),
                            DisplayKey = configurationMaster.schemaValues.Where(t => t.Key.Equals(configurationMetaValue.key, StringComparison.OrdinalIgnoreCase)).FirstOrDefault().KeyDisplay
                        };
                        metaData.Add(meta);
                    }
                    configToAdd.MetaValue = JsonDocument.Parse(JsonConvert.SerializeObject(metaData));
                    await _configurationDbContext.ConfigurationMainBreakup.AddAsync(configToAdd);
                }
                else
                {
                    //update the existing value
                    List<ConfigurationMainBreakupMetaValue> metaData = configMainBreakUp.ConfigurationMainBreakupMetaValues;

                    foreach (var configurationMetaValue in item.configurationMetaValues)
                    {
                        var configKeyInfoIndex = metaData.FindIndex(e => e.Key.ToLower().Trim() == configurationMetaValue.key.ToLower().Trim());
                        ConfigurationMainBreakupMetaValue meta = new ConfigurationMainBreakupMetaValue();
                        if (configKeyInfoIndex != -1)
                        {
                            metaData[configKeyInfoIndex].Value = configurationMetaValue.value.ToString();
                            configMainBreakUp.ModifiedAt = DateTime.UtcNow;
                            configMainBreakUp.ModifiedBy = email;
                        }
                        else
                        {
                            var info = configurationMaster.schemaValues.Where(x => x.Key.ToLower().Trim() == configurationMetaValue.key.ToLower().Trim()).FirstOrDefault();
                            meta.Key = configurationMetaValue.key;
                            meta.DisplayKey = info == null ? configurationMetaValue.key : info.KeyDisplay;
                            meta.Value = configurationMetaValue.value;
                            configMainBreakUp.ModifiedAt = DateTime.UtcNow;
                            configMainBreakUp.ModifiedBy = email;
                            metaData.Add(meta);
                        }
                    }
                    configMainBreakUp.MetaValue = JsonDocument.Parse(JsonConvert.SerializeObject(metaData));
                    _configurationDbContext.ConfigurationMainBreakup.Update(configMainBreakUp);
                }

            }
            await _configurationDbContext.SaveChangesAsync();
            return null;
        }

        private ConfigurationMaster GetDefaultConfiguration(ConfigurationMaster configurationMaster, List<WCGTBUTreeMappingDTO> buTreeMapping)
        {
            if (configurationMaster?.SelectorConfigType.ToLower().Trim() == "offerings" || configurationMaster?.SelectorConfigType.ToLower().Trim() == "bu" && configurationMaster?.ConfigurationMainBreakups.Count > 1)
            {
                List<ConfigurationMainBreakup> finalConfigurationBreakups = new();
                List<WCGTBUTreeMappingDTO> distinctBuTreeMapping = new();

                if (configurationMaster.SelectorConfigType.ToLower().Trim() == "offerings")
                {
                    distinctBuTreeMapping = buTreeMapping.Where(e => !string.IsNullOrEmpty(e.offering)).DistinctBy(e => e.offering).ToList();
                }
                else if (configurationMaster.SelectorConfigType.ToLower().Trim() == "bu")
                {
                    distinctBuTreeMapping = buTreeMapping.Where(e => !string.IsNullOrEmpty(e.bu)).DistinctBy(e => e.bu).ToList();
                }
                var defaultConfigBreakup = configurationMaster.ConfigurationMainBreakups
                    .Where(e => e.KeySelector.ToLower().Trim() == ConfigMasterKeyDisplayLabel.Default_Key_Selector.ToLower().Trim())
                    .FirstOrDefault();
                List<ConfigurationMainBreakup> nonDefaultConfigBreakup = new();
                foreach (var mapping in distinctBuTreeMapping)
                {
                    string filterKey = string.Empty;
                    if (configurationMaster.SelectorConfigType.ToLower().Trim() == "offerings")
                    {
                        filterKey = $"{mapping.bu?.ToLower().Trim()}|{mapping.offering?.ToLower().Trim()}";
                    }
                    else if (configurationMaster.SelectorConfigType.ToLower().Trim() == "bu")
                    {
                        filterKey = $"{mapping.bu?.ToLower().Trim()}";
                    }
                    var offeringConfigBreakup = configurationMaster.ConfigurationMainBreakups
                        .Where(e => e.KeySelector.ToLower().Trim() == filterKey.ToLower().Trim())
                        .FirstOrDefault();

                    if (offeringConfigBreakup == null)
                    {
                        ConfigurationMainBreakup newOfferingConfigMainBreakup = new()
                        {
                            Id = Guid.NewGuid(),
                            KeySelector = $"{mapping.bu}|{mapping.offering}",
                            ConfigurationMasterId = configurationMaster.Id,
                            ConfigurationMainBreakupMetaValues = defaultConfigBreakup.ConfigurationMainBreakupMetaValues,
                            MetaValue = defaultConfigBreakup.MetaValue
                        };
                        nonDefaultConfigBreakup.Add(newOfferingConfigMainBreakup);
                    }
                    else
                    {
                        nonDefaultConfigBreakup.Add(offeringConfigBreakup);
                    }
                }
                finalConfigurationBreakups.Add(defaultConfigBreakup);
                finalConfigurationBreakups.AddRange(nonDefaultConfigBreakup);
                configurationMaster.ConfigurationMainBreakups = finalConfigurationBreakups;
            }

            return configurationMaster;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configGroup">Resource_allocation_review</param>
        /// <param name="configKey"></param>
        /// <param name="attributeName"></param>
        /// <param name="selectorConfigType">SOLLUTION</param>
        /// <returns></returns>
        public async Task<ConfigurationMaster> GetConfigurationMasterByConfigGroupAndConfigKey(string configGroup, string configKey, string attributeName, string selectorConfigType, List<WCGTBUTreeMappingDTO> buTreeMapping)
        {
            var configurationMaster = await _configurationDbContext.ConfigurationMaster
                .Where(e => e.ConfigGroup.ToLower().Trim() == configGroup.ToLower().Trim()
                            && e.SelectorConfigType.ToLower().Trim() == selectorConfigType.ToLower().Trim())
                .Include(t => t.ConfigurationMainBreakups)
                .FirstOrDefaultAsync();

            if (configurationMaster != null)
            {
                //.Where(l => l.KeySelector.ToLower().Trim() == attributeName.ToLower().Trim() || l.KeySelector.ToLower().Trim() == ConfigMasterKeyDisplayLabel.Default_Key_Selector.ToLower().Trim()))
                var finalConfigurationMaster = GetDefaultConfiguration(configurationMaster, buTreeMapping);
                return finalConfigurationMaster;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configGroup">Resource_allocation_review</param>
        /// <param name="configKey">Resource_allocation_review</param>
        /// <param name="configType">OFFERING|SOLUTION</param>
        /// <param name="buTreeMapping"></param>
        /// <param name="attributeName">OFFERING1</param>
        /// <returns></returns>
        //public async Task<List<ConfigurationGroup>> GetConfigurationGroupsByConfigNameConfigKeyAndConfigType(string configGroup, string configKey, string configType, List<WCGTBUTreeMappingDTO> buTreeMapping, string attributeName)
        //{
        //    //var res = await _configurationDbContext.ConfigurationGroups.ToListAsync();
        //    var configurationGroup = await _configurationDbContext.ConfigurationGroups
        //                                    .Where((d) => d.ConfigurationGroupMaster != null &&
        //                                        d.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim() == configGroup.ToUpper().Trim() &&
        //                                        d.ConfigurationGroupMaster.ConfigKey.ToUpper().Trim() == configKey.ToUpper().Trim() &&
        //                                        d.ConfigType.ToUpper().Trim() == configType.ToUpper().Trim() &&
        //                                        //String.Equals((string)(d.ConfigGroup) + "", (string)configGroup + "", StringComparison.CurrentCultureIgnoreCase) &&
        //                                        //String.Equals((string)(d.ConfigKey) + "", (string)configKey + "", StringComparison.CurrentCultureIgnoreCase) &&
        //                                        //String.Equals((string)(d.ConfigType) + "", (string)configType + "", StringComparison.CurrentCultureIgnoreCase) &&
        //                                        d.IsActive == true)
        //                                    .Include((x) => x.ProjectConfigurations
        //                                    .Where((t) => t.IsActive == true)
        //                                    )
        //                                    .OrderBy(a => a.SortOrder).ToListAsync();

        //    foreach (var item in configurationGroup)
        //    {
        //        if (item.ProjectConfigurations != null && item.ProjectConfigurations.Count == 0)
        //        {
        //            //Check if default values are needed to be added in this method
        //            List<ConfigurationGroupMaster> configGroupMaster = _configurationDbContext.ConfigurationGroupMasters
        //                                                                .Where(d => d.IsActive == true
        //                                                                && EF.Functions.ILike(d.ConfigGroup, configGroup)
        //                                                                && EF.Functions.ILike(d.ConfigKey, configKey)
        //                                                                ).ToList();

        //            item.ProjectConfigurations = this.GetProjectConfigurationsWithDefaultValues(item.ProjectConfigurations, configGroupMaster, configType, attributeName);

        //            var temp = item.ProjectConfigurations
        //                        .Where(d => d.ConfigurationGroup != null && d.ConfigurationGroup.ConfigurationGroupMaster != null &&
        //                         d.ConfigurationGroup.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim() == configGroup.ToUpper().Trim() &&
        //                         d.ConfigurationGroup.ConfigurationGroupMaster.ConfigKey.ToUpper().Trim() == configKey.ToUpper().Trim() &&
        //                         d.ConfigurationGroup.ConfigType.ToUpper().Trim() == configType.ToUpper().Trim()
        //                     ).First();
        //            //if no project configuration found pass eth default value in all value property to be used in workflow service 
        //            //do not change
        //            if (temp != null)
        //            {
        //                item.AllValue = temp.AttributeValue;
        //                item.ProjectConfigurations = new List<ProjectConfiguration>();
        //            }
        //            //item.ProjectConfigurations = temp;
        //        }
        //        else
        //        {
        //            //get data based on existing and amster also 
        //            if (configType.ToUpper().Trim() == Constants.ConfigTypeOfferings)
        //            {
        //                List<WCGTBUTreeMappingDTO> expertiesMasters = GetDistinctExpertises(buTreeMapping);

        //                var first = _configurationDbContext.ConfigurationGroupMasters
        //                                                                .Where(d => d.IsActive == true
        //                                                                && EF.Functions.ILike(d.ConfigGroup, configGroup)
        //                                                                && EF.Functions.ILike(d.ConfigKey, configKey)
        //                                                                ).FirstOrDefault();

        //                var projectConfigurationsList = new List<ProjectConfiguration>();
        //                foreach (var experties in expertiesMasters)
        //                {
        //                    var index = item.ProjectConfigurations.FindIndex(d => d.AttributeName.Equals(experties.offering) && d.IsActive);
        //                    if (index == -1)
        //                    {
        //                        var currProjectConfiguration = new ProjectConfiguration()
        //                        {
        //                            Id = 0,
        //                            AttributeName = experties.offering,
        //                            ConfigId = item.Id,
        //                            AttributeValue = first?.DefaultValue,
        //                            CreatedAt = DateTime.Now,
        //                            CreatedBy = string.Empty,
        //                            ModifiedAt = DateTime.Now,
        //                            ModifiedBy = string.Empty,
        //                            IsActive = experties.isactive == true,
        //                        };
        //                        projectConfigurationsList.Add(currProjectConfiguration);
        //                    }
        //                    else
        //                    {
        //                        projectConfigurationsList.Add(item.ProjectConfigurations[index]);
        //                    }

        //                }

        //                item.ProjectConfigurations = projectConfigurationsList;
        //            }
        //            else if (configType.ToUpper().Trim() == Constants.ConfigTypeBusinessUnit)
        //            {
        //                List<WCGTBUTreeMappingDTO> buMasters = GetDistinctBUs(buTreeMapping);

        //                var first = _configurationDbContext.ConfigurationGroupMasters
        //                                                                .Where(d => d.IsActive == true
        //                                                               && EF.Functions.ILike(d.ConfigGroup, configGroup)
        //                                                                && EF.Functions.ILike(d.ConfigKey, configKey)
        //                                                                ).FirstOrDefault();

        //                var projectConfigurationsList = new List<ProjectConfiguration>();
        //                foreach (var bu in buMasters)
        //                {
        //                    var index = item.ProjectConfigurations.FindIndex(d => d.AttributeName.Equals(bu.bu) && d.IsActive);
        //                    if (index == -1)
        //                    {
        //                        var currProjectConfiguration = new ProjectConfiguration()
        //                        {
        //                            Id = 0,
        //                            AttributeName = bu.offering,
        //                            ConfigId = item.Id,
        //                            AttributeValue = first?.DefaultValue,
        //                            CreatedAt = DateTime.Now,
        //                            CreatedBy = string.Empty,
        //                            ModifiedAt = DateTime.Now,
        //                            ModifiedBy = string.Empty,
        //                            IsActive = bu.isactive == true,
        //                        };
        //                        projectConfigurationsList.Add(currProjectConfiguration);
        //                    }
        //                    else
        //                    {
        //                        projectConfigurationsList.Add(item.ProjectConfigurations[index]);
        //                    }

        //                }

        //            }
        //        }
        //    }
        //    return configurationGroup.OrderBy(a => a.SortOrder).ToList();

        //}

        //public async Task<List<ConfigurationGroup>> GetConfigurationGroupsByGroupNameAndConfigType(string configGroup, string configType, List<WCGTBUTreeMappingDTO> buTreeMapping, string attributeName)
        //{
        //    List<ConfigurationGroupMaster> configGroupMaster = _configurationDbContext.ConfigurationGroupMasters
        //                                                .Where(d => d.IsActive == true
        //                                                && EF.Functions.ILike(d.ConfigGroup, configGroup)
        //                                                ).ToList();

        //    List<ProjectConfiguration> projectConfigurationsList = new List<ProjectConfiguration>();

        //    if (configType.ToUpper().Trim() == Constants.ConfigTypeOfferings)
        //    {
        //        //var expertiesMasters = await _configurationDbContext.ExpertiesMasters.Where(d => d.IsActive).ToListAsync();
        //        List<WCGTBUTreeMappingDTO> expertiesMasters = GetDistinctExpertises(buTreeMapping);

        //        var configurationGrp = await _configurationDbContext.ConfigurationGroups
        //                    .Include(d => d.ConfigurationGroupMaster)
        //                    .Where(d => d.ConfigurationGroupMaster != null
        //                        && d.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim() == configGroup.ToUpper()
        //                        && d.ConfigType.ToUpper().Trim() == configType.ToUpper().Trim()
        //                        && d.IsActive == true)
        //                    .Include(d => d.ProjectConfigurations)
        //                    .OrderBy(a => a.SortOrder)
        //                    .ToListAsync();

        //        foreach (var group in configurationGrp)
        //        {
        //            projectConfigurationsList = new List<ProjectConfiguration>();
        //            //foreach (var item in group.ProjectConfigurations)
        //            //{
        //            //    exper
        //            //}
        //            //d.ConfigGroup.ToUpper().Trim() == configGroup.ToUpper().Trim()
        //            var first = configGroupMaster.Where(d => d.IsActive == true
        //                                                && d.ConfigGroup.ToUpper().Trim() == configGroup.ToUpper().Trim()
        //                                                && d.ConfigKey.ToUpper().Trim() == group.ConfigurationGroupMaster?.ConfigKey?.ToUpper().Trim()
        //                                                ).FirstOrDefault();

        //            foreach (var experties in expertiesMasters)
        //            {
        //                var index = group.ProjectConfigurations.FindIndex(d => d.AttributeName.Equals(experties.offering) && d.IsActive);
        //                if (index == -1)
        //                {
        //                    var currProjectConfiguration = new ProjectConfiguration()
        //                    {
        //                        Id = 0,
        //                        AttributeName = experties.offering,
        //                        ConfigId = group.Id,
        //                        AttributeValue = first?.DefaultValue,
        //                        CreatedAt = DateTime.Now,
        //                        CreatedBy = string.Empty,
        //                        ModifiedAt = DateTime.Now,
        //                        ModifiedBy = string.Empty,
        //                        IsActive = experties.isactive == true,
        //                    };
        //                    projectConfigurationsList.Add(currProjectConfiguration);
        //                }
        //                else
        //                {
        //                    projectConfigurationsList.Add(group.ProjectConfigurations[index]);
        //                }

        //                //add default item to projectConfigurationsList collection in below method
        //                //this.GetProjectConfigurationsWithDefaultValues(projectConfigurationsList, configGroupMaster, configType, experties.Experties);

        //            }

        //            //this.GetProjectConfigurationsWithDefaultValues(projectConfigurationsList, configGroupMaster, configType, attributeName);

        //            //projectConfigurationsList = projectConfigurationsList.Where(d => d.ConfigurationGroup != null && d.ConfigurationGroup.ConfigurationGroupMaster != null
        //            //                                                        && d.ConfigurationGroup.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim() == configGroup.ToUpper()
        //            //                                                        ).ToList();

        //            group.ProjectConfigurations = projectConfigurationsList;
        //        }
        //        return configurationGrp.OrderBy(a => a.SortOrder).ToList();
        //    }
        //    else if (configType.ToUpper().Trim() == Constants.ConfigTypeBusinessUnit)
        //    {
        //        //var businessUnitMasters = await _configurationDbContext.BusinessUnitMasters.Where(d => d.IsActive).ToListAsync();

        //        List<WCGTBUTreeMappingDTO> businessUnitMasters = GetDistinctBUs(buTreeMapping);

        //        var configurationGrp = await _configurationDbContext.ConfigurationGroups
        //                    .Where(d => d.ConfigurationGroupMaster != null
        //                        && d.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim() == configGroup.ToUpper()
        //                        && d.ConfigType.ToUpper().Trim() == configType.ToUpper().Trim()
        //                        && d.IsActive == true)
        //                    .Include(d => d.ProjectConfigurations)
        //                    .OrderBy(a => a.SortOrder)
        //                    .ToListAsync();

        //        foreach (var group in configurationGrp)
        //        {
        //            projectConfigurationsList = new List<ProjectConfiguration>();
        //            //foreach (var item in group.ProjectConfigurations)
        //            //{
        //            //    exper
        //            //}

        //            var first = configGroupMaster.Where(d => d.IsActive == true
        //                                                && d.ConfigGroup.ToUpper().Trim() == configGroup.ToUpper().Trim()
        //                                                && d.ConfigKey.ToUpper().Trim() == group.ConfigurationGroupMaster?.ConfigKey?.ToUpper().Trim()
        //                                                ).FirstOrDefault();

        //            foreach (var businessUnit in businessUnitMasters)
        //            {
        //                var index = group.ProjectConfigurations.FindIndex(d => d.AttributeName.Equals(businessUnit.bu) && d.IsActive);
        //                if (index == -1)
        //                {
        //                    var currProjectConfiguration = new ProjectConfiguration()
        //                    {
        //                        Id = 0,
        //                        AttributeName = businessUnit.bu,
        //                        ConfigId = group.Id,
        //                        AttributeValue = first?.DefaultValue,
        //                        CreatedAt = DateTime.Now,
        //                        CreatedBy = string.Empty,
        //                        ModifiedAt = DateTime.Now,
        //                        ModifiedBy = string.Empty,
        //                        IsActive = businessUnit.isactive == true,
        //                    };
        //                    projectConfigurationsList.Add(currProjectConfiguration);
        //                }
        //                else
        //                {
        //                    projectConfigurationsList.Add(group.ProjectConfigurations[index]);
        //                }

        //                //add default item to projectConfigurationsList collection in below method
        //                //this.GetProjectConfigurationsWithDefaultValues(projectConfigurationsList, configGroupMaster, configType, businessUnit.BusinessUnit);

        //            }

        //            //this.GetProjectConfigurationsWithDefaultValues(projectConfigurationsList, configGroupMaster, configType, attributeName);

        //            //projectConfigurationsList = projectConfigurationsList.Where(d => d.ConfigurationGroup != null && d.ConfigurationGroup.ConfigurationGroupMaster != null
        //            //                                                        && d.ConfigurationGroup.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim() == configGroup.ToUpper()
        //            //                                                        ).ToList();

        //            group.ProjectConfigurations = projectConfigurationsList;

        //        }
        //        return configurationGrp.OrderBy(a => a.SortOrder).ToList();
        //    }

        //    var configurationGroup = await _configurationDbContext.ConfigurationGroups
        //                    .Where(d => d.ConfigurationGroupMaster != null
        //                        && d.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim() == configGroup.ToUpper()
        //                        && d.ConfigType.ToUpper().Trim() == configType.ToUpper().Trim()
        //                        && d.IsActive == true)
        //                    .Include(d => d.ProjectConfigurations)
        //                    .OrderBy(a => a.SortOrder)
        //                    .ToListAsync();

        //    foreach (var item in configurationGroup)
        //    {
        //        item.ProjectConfigurations = this.GetProjectConfigurationsWithDefaultValues(item.ProjectConfigurations, configGroupMaster, configType, attributeName);
        //    }

        //    return configurationGroup.OrderBy(a => a.SortOrder).ToList();
        //}

        private static List<WCGTBUTreeMappingDTO> GetDistinctBUs(List<WCGTBUTreeMappingDTO> buTreeMapping)
        {
            return buTreeMapping.Where(d => d.isactive == true)
                .Select(s => new WCGTBUTreeMappingDTO
                {
                    bu = s.bu,
                    bu_id = s.bu_id,
                    isactive = s.isactive,
                })
                .DistinctBy(m => new { m.bu, m.bu_id }).ToList();
        }

        private static List<WCGTBUTreeMappingDTO> GetDistinctExpertises(List<WCGTBUTreeMappingDTO> buTreeMapping)
        {
            return buTreeMapping.Where(d => d.isactive == true)
                .Select(s => new WCGTBUTreeMappingDTO
                {
                    offering = s.offering,
                    offering_id = s.offering_id,
                    isactive = s.isactive,
                })
                .DistinctBy(m => new { m.offering, m.offering_id }).ToList();
        }

        //public async Task<Dictionary<string, string>> GetAllBuExpertiesDict()
        //{
        //    var buExpertiesGrp = await _configurationDbContext.Bu_Experties_Grps.Where(d => d.IsActive && !string.IsNullOrEmpty(d.ExpertiesName)).
        //                        Select((c) => new { c.BusinessUnitName, c.ExpertiesName }).
        //                        ToListAsync();
        //    Dictionary<string, string> buExpertiesDict = buExpertiesGrp.ToDictionary(c => c.ExpertiesName, c => c.BusinessUnitName);
        //    return buExpertiesDict;
        //}

        public async Task<Dictionary<string, string>> GetApplicationLevelSettingsDict(List<string>? keys)
        {
            var _applicationLevelSettings = await _configurationDbContext.ApplicationLevelSettings
                                .Where(d => (keys != null && keys.Count > 0 ? keys.ToList().Any(m => m.Equals(d.Key)) : true) && d.IsActive == true)
                                .ToListAsync();

            Dictionary<string, string> aplicationLevelSettingsDict = _applicationLevelSettings.ToDictionary(c => c.Key, c => c.Value);
            return aplicationLevelSettingsDict;
        }

        //checked
        public async Task<List<ProjectConfiguration>> UpdateProjectConfiguration(List<ConfigurationGroup> configurationGroups, string configurationType, List<WCGTBUTreeMappingDTO> buTreeMapping)
        {
            //BU -> Exp
            if (configurationGroups is null)
            {
                throw new Exception("configurationGroups is null");
            }
            try
            {
                foreach (var configurationGroup in configurationGroups)
                {
                    var dbConfigurationGroup = await _configurationDbContext.ConfigurationGroups
                                                .Where(d => d.ConfigurationGroupMaster != null
                                                    && d.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim() == configurationGroup.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim()
                                                    && d.ConfigurationGroupMaster.ConfigKey.ToUpper().Trim() == configurationGroup.ConfigurationGroupMaster.ConfigKey.ToUpper().Trim()
                                                    && d.ConfigType.ToUpper().Trim() == configurationGroup.ConfigType.ToUpper().Trim()
                                                    && d.IsActive == true)
                                                .FirstOrDefaultAsync();
                    dbConfigurationGroup.IsAll = configurationGroup.IsAll;
                    dbConfigurationGroup.AllValue = configurationGroup.AllValue;
                    _configurationDbContext.ConfigurationGroups.Update(dbConfigurationGroup);

                    var dbProjectConfigurations = await _configurationDbContext.ProjectConfigurations.Where(p => p.ConfigId == configurationGroup.Id && p.IsActive).ToListAsync();

                    foreach (var currentDbProjectConfiguration in dbProjectConfigurations)
                    {
                        var index = configurationGroup.ProjectConfigurations.FindIndex(d =>
                                                                                    d.AttributeName.ToUpper().Trim() == currentDbProjectConfiguration.AttributeName.ToUpper().Trim()
                                                                                    && d.ConfigId == currentDbProjectConfiguration.ConfigId
                                                                                    && d.IsActive == true);
                        if (index == -1)
                        {
                            currentDbProjectConfiguration.IsActive = false;
                            _configurationDbContext.ProjectConfigurations.Update(currentDbProjectConfiguration);
                        }
                    }

                    foreach (var projectConfiguration in configurationGroup.ProjectConfigurations)
                    {
                        var dbProjectConfiguration = await _configurationDbContext.ProjectConfigurations
                            .Where(d =>
                                d.ConfigId == projectConfiguration.ConfigId
                                && d.AttributeName.ToUpper().Trim() == projectConfiguration.AttributeName.ToUpper().Trim()
                                && d.IsActive == true)
                            .FirstOrDefaultAsync();
                        if (dbProjectConfiguration is null)
                        {
                            ProjectConfiguration newProjectConfiguration = new ProjectConfiguration
                            {
                                AttributeName = projectConfiguration.AttributeName,
                                ConfigId = projectConfiguration.ConfigId,
                                AttributeValue = projectConfiguration.AttributeValue,
                                IsActive = true,
                                CreatedAt = configurationGroup.ModifiedAt,
                                CreatedBy = configurationGroup.ModifiedBy,
                                ModifiedAt = configurationGroup.ModifiedAt,
                                ModifiedBy = configurationGroup.ModifiedBy
                            };
                            var addedProjectConfiguration = await _configurationDbContext.ProjectConfigurations.AddAsync(newProjectConfiguration);
                        }
                        else
                        {
                            dbProjectConfiguration.AttributeValue = projectConfiguration.AttributeValue;
                            dbProjectConfiguration.ModifiedAt = configurationGroup.ModifiedAt;
                            dbProjectConfiguration.ModifiedBy = configurationGroup.ModifiedBy;
                            _configurationDbContext.ProjectConfigurations.Update(dbProjectConfiguration);
                        }
                        if (configurationType == Constants.ConfigTypeBusinessUnit)
                        {
                            //if (configurationGroup.IsAll)
                            //{
                            var configGrpExpDb = await _configurationDbContext.ConfigurationGroups.Where(d => d.ConfigurationGroupMaster != null
                                    && d.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim() == configurationGroup.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim()
                                    && d.ConfigurationGroupMaster.ConfigKey.ToUpper().Trim() == configurationGroup.ConfigurationGroupMaster.ConfigKey.ToUpper().Trim()
                                    && d.ConfigType.ToUpper().Trim() == Constants.ConfigTypeOfferings && d.IsActive == true).FirstOrDefaultAsync();
                            if (configGrpExpDb is null)
                            {
                                throw new Exception("Experties configuration group not found");
                            }
                            configGrpExpDb.IsAll = configurationGroup.IsAll;
                            if (configurationGroup.IsAll)
                            {
                                configGrpExpDb.AllValue = configurationGroup.AllValue;
                            }
                            _configurationDbContext.ConfigurationGroups.Update(configGrpExpDb);
                            //}
                            //var expertiseMasterDb = await _configurationDbContext.ExpertiesMasters.Where(d =>
                            //    d.IsActive == true
                            // ).ToListAsync();
                            var expertiseMasterDb = GetDistinctExpertises(buTreeMapping);


                            var currentConfigGroupExp = await _configurationDbContext.ProjectConfigurations
                                .Include(e => e.ConfigurationGroup).ThenInclude(t => t.ConfigurationGroupMaster)
                                .Where(a => a.ConfigurationGroup != null && a.ConfigurationGroup.ConfigurationGroupMaster != null
                                    && a.ConfigurationGroup.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim() == configurationGroup.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim()
                                    && a.ConfigurationGroup.ConfigurationGroupMaster.ConfigKey.ToUpper().Trim() == configurationGroup.ConfigurationGroupMaster.ConfigKey.ToUpper().Trim()
                                    && a.ConfigurationGroup.ConfigType.ToUpper().Trim() == Constants.ConfigTypeOfferings
                                    && a.IsActive == true).ToListAsync();

                            foreach (var item in currentConfigGroupExp)
                            {
                                var index = expertiseMasterDb.FindIndex(d =>
                                d.isactive == true
                                && d.offering?.ToUpper().Trim() == item.AttributeName.ToUpper().Trim());
                                if (index == -1)
                                {
                                    item.IsActive = false;
                                    _configurationDbContext.ProjectConfigurations.Update(item);
                                }
                            }
                            //var businessUnitMasterDb = await _configurationDbContext.BusinessUnitMasters
                            //    .Where(d =>
                            //        d.BusinessUnit.ToUpper().Trim() == projectConfiguration.AttributeName.ToUpper().Trim()
                            //        && d.IsActive == true)
                            //    .Include(p => p.Bu_Experties_Grps).FirstOrDefaultAsync();

                            var businessUnitMasterDb = GetDistinctBUs(buTreeMapping);


                            if (businessUnitMasterDb is null)
                            {
                                throw new Exception("Business unit not found");
                            }
                            var _grp = buTreeMapping.ToArray();

                            for (int i = 0; i < _grp.Length; i++)
                            {
                                //Bu_Experties_Grp item = _grp[i];
                                //}
                                //foreach (var item in _grp)
                                //{
                                if (_grp[i] != null && _grp[i].isactive == true)
                                {
                                    var expertiesMasterDb = expertiseMasterDb.Where(d => d.isactive == true
                                    && d.offering == _grp[i].offering).FirstOrDefault();

                                    //var expertiesMasterDb = await _configurationDbContext.ExpertiesMasters
                                    //.Where(d => d.Id == _grp[i].ExpertiesId
                                    //&& d.IsActive == true)
                                    //.FirstOrDefaultAsync();

                                    var expertiesProjectConfiguration = await _configurationDbContext.ProjectConfigurations
                                        .Include(e => e.ConfigurationGroup).ThenInclude(t => t.ConfigurationGroupMaster)
                                        .Where(d => d.ConfigurationGroup != null && d.ConfigurationGroup.ConfigurationGroupMaster != null
                                        && d.AttributeName.ToUpper().Trim() == expertiesMasterDb.offering.ToUpper().Trim()
                                        && d.ConfigurationGroup.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim() == configurationGroup.ConfigurationGroupMaster.ConfigGroup.ToUpper().Trim()
                                        && d.ConfigurationGroup.ConfigurationGroupMaster.ConfigKey.ToUpper().Trim() == configurationGroup.ConfigurationGroupMaster.ConfigKey.ToUpper().Trim()
                                        && d.ConfigurationGroup.ConfigType.ToUpper().Trim() == Constants.ConfigTypeOfferings
                                        && d.IsActive == true).FirstOrDefaultAsync();
                                    if (expertiesProjectConfiguration is null)
                                    {
                                        var newprojectConfig = new ProjectConfiguration()
                                        {
                                            AttributeName = expertiesMasterDb.offering,
                                            AttributeValue = projectConfiguration.AttributeValue,
                                            ConfigId = configGrpExpDb.Id,
                                            CreatedAt = configurationGroup.ModifiedAt,
                                            CreatedBy = configurationGroup.ModifiedBy,
                                            ModifiedAt = configurationGroup.ModifiedAt,
                                            ModifiedBy = configurationGroup.ModifiedBy,
                                            IsActive = true
                                        };
                                        await _configurationDbContext.ProjectConfigurations.AddAsync(newprojectConfig);
                                    }
                                    else
                                    {
                                        expertiesProjectConfiguration.AttributeValue = projectConfiguration.AttributeValue;
                                        expertiesProjectConfiguration.ModifiedAt = configurationGroup.ModifiedAt;
                                        expertiesProjectConfiguration.ModifiedBy = configurationGroup.ModifiedBy;
                                        _configurationDbContext.ProjectConfigurations.Update(expertiesProjectConfiguration);

                                    }
                                    var resInn = expertiesProjectConfiguration;
                                }
                            }
                        }
                        var result = projectConfiguration;
                    }

                }
                await _configurationDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }

            ////TODO: Enum Creation
            //if (configurationType == "BUSINESS_UNIT")
            //{
            //    foreach (var projectConfiguration in projectConfigurations)
            //    {
            //        //Get ProjectConfiguration for BU
            //        var buProjectConfiguration = await _configurationDbContext.ProjectConfigurations
            //                                    .Where(d => d.Id == projectConfiguration.Id)
            //                                    .FirstOrDefaultAsync();
            //        if (buProjectConfiguration is null)
            //        {
            //            throw new Exception("Business Unit Project Configuration not Found");
            //        }
            //        //Get ConfigurationGroup for BU
            //        var buConfigurationGroup = await _configurationDbContext.ConfigurationGroups
            //            .Where(d => d.Id == buProjectConfiguration.ConfigurationGroup.Id)
            //            .FirstOrDefaultAsync();
            //        if (buConfigurationGroup is null)
            //        {
            //            throw new Exception("Business Unit Configuration Group Not Found");
            //        }
            //        buProjectConfiguration.AttributeValue = projectConfiguration.AttributeValue;
            //        buProjectConfiguration.AttributeName = projectConfiguration.AttributeName;
            //        buProjectConfiguration.ConfigId = projectConfiguration.ConfigId;
            //        //update BU Project Configuration
            //        _configurationDbContext.ProjectConfigurations.Update(buProjectConfiguration);
            //        buConfigurationGroup.IsAll = projectConfiguration.ConfigurationGroup.IsAll;
            //        buConfigurationGroup.AllValue = projectConfiguration.ConfigurationGroup.AllValue;
            //        //update BU Configurations Group
            //        _configurationDbContext.ConfigurationGroups.Update(buConfigurationGroup);

            //        //business unit master data
            //        var businessUnitMasterResult = await _configurationDbContext.BusinessUnitMasters
            //                .Where(d => d.BusinessUnit.ToUpper().Trim() == projectConfiguration.AttributeName.ToUpper().Trim())
            //                .Include(e => e.Bu_Experties_Grps)
            //                .FirstOrDefaultAsync();
            //        if (businessUnitMasterResult is null)
            //        {
            //            throw new Exception("business unit not found");
            //        }

            //        foreach (var item in businessUnitMasterResult.Bu_Experties_Grps)
            //        {
            //            //Iterating Each Experties
            //            //Getting experties master
            //            var expertiesMasterResult = await _configurationDbContext.ExpertiesMasters
            //                .Where(d => d.ExpertiseMasterId == item.ExpertiesId)
            //                .FirstOrDefaultAsync();
            //            //
            //            var expertiesProjectConfiguration = await _configurationDbContext.ProjectConfigurations.Include(e => e.ConfigurationGroup)
            //                .Where(d =>
            //                d.AttributeName.ToUpper().Trim() == expertiesMasterResult.Experties.ToUpper().Trim()
            //                && d.ConfigurationGroup.ConfigGroup.ToUpper().Trim() == projectConfiguration.ConfigurationGroup.ConfigGroup.ToUpper().Trim()
            //                && d.ConfigurationGroup.ConfigType.ToUpper().Trim() == "EXPERTISE"
            //                && d.ConfigurationGroup.ConfigKey.ToUpper().Trim() == projectConfiguration.ConfigurationGroup.ConfigKey.ToUpper().Trim())
            //                .FirstOrDefaultAsync();
            //            var expertiesConfigurationGroup = await _configurationDbContext.ConfigurationGroups.Where(d => d.Id == expertiesProjectConfiguration.ConfigurationGroup.Id).FirstOrDefaultAsync();
            //            if (expertiesProjectConfiguration is null)
            //            {
            //                throw new Exception("expertiesProjectConfiguration Not Found");
            //            }
            //            if (expertiesConfigurationGroup is null)
            //            {
            //                throw new Exception("expertiesConfigurationGroup Not Found");
            //            }
            //            expertiesProjectConfiguration.AttributeValue = projectConfiguration.AttributeValue;
            //            _configurationDbContext.ProjectConfigurations.Update(expertiesProjectConfiguration);
            //            expertiesConfigurationGroup.AllValue = projectConfiguration.AttributeValue;
            //            _configurationDbContext.ConfigurationGroups.Update(expertiesConfigurationGroup);

            //        }
            //    }
            //}
            //List<ProjectConfiguration> result = new List<ProjectConfiguration>();
            //foreach (var projectConfiguration in projectConfigurations)
            //{
            //    var projConfig = await _configurationDbContext.ProjectConfigurations.Where(d => d.Id == projectConfiguration.Id && d.IsActive == true).FirstOrDefaultAsync();
            //    if (projConfig is null)
            //    {
            //        throw new Exception("One of the Project Configuration is null");
            //    }
            //    projConfig.AttributeName = projectConfiguration.AttributeName;
            //    projConfig.AttributeValue = projectConfiguration.AttributeValue;
            //    projConfig.ConfigId = projectConfiguration.ConfigId;
            //    projConfig.ModifiedAt = projectConfiguration.ModifiedAt;
            //    projConfig.ModifiedBy = projectConfiguration.ModifiedBy;
            //    projConfig.IsActive = projectConfiguration.IsActive;
            //    if (projectConfiguration.ConfigurationGroup is null)
            //    {
            //        throw new Exception("Configuration Group of project configuration is null");
            //    }
            //    var configGroup = await _configurationDbContext.ConfigurationGroups.Where(a => a.Id == projectConfiguration.ConfigurationGroup.Id).FirstOrDefaultAsync();
            //    if (configGroup is null)
            //    {
            //        throw new Exception("Configuration Group is null");
            //    }
            //    configGroup.IsAll = projectConfiguration.ConfigurationGroup.IsAll;
            //    configGroup.AllValue = projectConfiguration.ConfigurationGroup.AllValue;
            //    configGroup.ModifiedBy = projectConfiguration.ConfigurationGroup.ModifiedBy;
            //    configGroup.ModifiedAt = projectConfiguration.ConfigurationGroup.ModifiedAt;
            //    configGroup.ConfigGroup = projectConfiguration.ConfigurationGroup.ConfigGroup;
            //    configGroup.ConfigGroupDisplay = projectConfiguration.ConfigurationGroup.ConfigGroupDisplay;
            //    configGroup.CongigDisplayText = projectConfiguration.ConfigurationGroup.CongigDisplayText;
            //    configGroup.IsActive = projectConfiguration.ConfigurationGroup.IsActive;
            //    configGroup.ValueType = projectConfiguration.ConfigurationGroup.ValueType;
            //    configGroup.ConfigKey = projectConfiguration.ConfigurationGroup.ConfigKey;
            //    configGroup.ConfigType = projectConfiguration.ConfigurationGroup.ConfigType;
            //    _configurationDbContext.ProjectConfigurations.Update(projConfig);
            //    //                    [
            //    //                    { { } },
            //    //                    { { } }
            //    //                ]
            //    _configurationDbContext.ConfigurationGroups.Update(configGroup);
            //    await _configurationDbContext.SaveChangesAsync();
            //    var projectConfigurationData = await GetProjectConfigurationById(projConfig.Id);
            //    result.Add(projectConfigurationData);
            ////}
            return null;
        }
    }
}