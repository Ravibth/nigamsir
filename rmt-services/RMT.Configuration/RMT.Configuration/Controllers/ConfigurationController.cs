using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RMT.Configuration.API.Attributes;
using RMT.Configuration.API.Service;
using RMT.Configuration.Application.DTOs.ApplicationConfigurationDTOs;
using RMT.Configuration.Application.DTOs.MasterDataDTOs;
using RMT.Configuration.Application.DTOs.Response;
using RMT.Configuration.Application.Handlers.CommandHandlers;
using RMT.Configuration.Application.Handlers.QueryHandlers;
using RMT.Configuration.Domain;
using RMT.Configuration.Domain.DTO;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Infrastructure.Repositories;

namespace RMT.Configuration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : BaseController
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IMediator _mediator;
        public ConfigurationController(IMediator mediator, IUserAccessor userAccessor, ILogger<BaseController> logger) : base(logger)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        [HttpGet("GetProjectConfigurationByConfigGroupAndConfigType")]
        [SanitizeInput]
        public async Task<List<ProjectConfiguration>> GetProjectConfigurationByConfigGroupAndConfigType(string ConfigGroup, string ConfigType)
        {
            try
            {
                var result = await _mediator.Send(new ProjectConfigurationByConfigGroupAndConfigTypeQuery()
                {
                    ConfigGroup = ConfigGroup,
                    ConfigType = ConfigType,
                    AttributeName = string.Empty,
                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }
        //scheduler allocation
        [HttpGet("GetExpertiesConfigurationByExpertiesNameAndConfigGroup")]
        [CustomHeader]
        [SanitizeInput]
        public async Task<List<ProjectConfiguration>> GetProjectConfigurationByExpertiesNameAndConfigGroup(string expertiesName, string configurationGroup)
        {
            try
            {
                var result = await _mediator.Send(new GetProjectConfigurationByExpertiesNameQuery()
                {
                    BuOfferingName = expertiesName,
                    ConfigurationGroup = configurationGroup

                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateConfiguration")]
        public async Task<List<ProjectConfiguration>> UpdateConfigGroupAndProjectConfiguration([FromBody] UpdateConfigurationDTO updateConfiguration)
        {
            try
            {
                List<UpdateConfiguration> updateConfig = new List<UpdateConfiguration>();
                foreach (var config in updateConfiguration.ConfigrationGroupDtos)
                {
                    List<ProjectConfiguration> projectConfigurations = new List<ProjectConfiguration>();
                    foreach (var projectConfig in config.ProjectConfigurations)
                    {
                        var projectConfiguration = new ProjectConfiguration
                        {
                            AttributeName = projectConfig.AttributeName,
                            AttributeValue = projectConfig.AttributeValue,
                            ConfigId = projectConfig.ConfigId,
                            ModifiedBy = "",
                            ModifiedAt = DateTime.UtcNow,
                            IsActive = true,

                        };
                        projectConfigurations.Add(projectConfiguration);
                    }
                    UpdateConfiguration updateConfigurationDTO = new UpdateConfiguration()
                    {
                        ConfigGroup = config.ConfigGroup,
                        ConfigGroupDisplay = config.ConfigGroupDisplay,
                        ConfigKey = config.ConfigKey,
                        ConfigType = config.ConfigType,
                        CongigDisplayText = config.CongigDisplayText,
                        AllValue = config.AllValue,
                        IsAll = config.IsAll,
                        ValueType = config.ValueType,
                        IsActive = true,
                        Id = config.Id,
                        ModifiedBy = "",
                        ModifiedAt = DateTime.UtcNow,
                        ProjectConfigurations = projectConfigurations,
                    };
                    updateConfig.Add(updateConfigurationDTO);
                }
                var result = await _mediator.Send(new UpdateConfigurationCommand()
                {
                    UpdatedConfigurations = updateConfig,
                    ConfigurationType = updateConfiguration.ConfigurationType
                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateConfigurationBreakup")]
        public async Task<string> UpdateConfigurationBreakup([FromBody] List<UpdateConfigurationBreakupRequestDTO> request)
        {
            try
            {
                UserDecorator userDecorator = _userAccessor.GetUser();
                UpdateConfigurationBreakupCommand req = new()
                {
                    UpdateConfigurationBreakupRequests = request,
                    email = userDecorator != null ? userDecorator.email : "",
                };
                var result = await _mediator.Send(req);
                return result;
            }
            catch (Exception ex)
            {

                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetConfigurationGroupByGroupNameAndConfigType")]
        [SanitizeInput]
        // found 5 instance > gateway and workflow and allocation, scheduler frontend(12 places)
        public async Task<List<ConfigurationGroupResponse>> GetConfigurationGroupByGroupNameAndConfigType(string groupName, string configType)
        {
            try
            {
                var result = await _mediator.Send(new GetConfigurationGroupByGroupNameQuery()
                {
                    GroupName = groupName,
                    ConfigType = configType,
                    AttributeName = string.Empty
                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetConfigurationMaster")]
        public async Task<List<ConfigurationMaster>> GetConfigurationMasterList(string? configGroup, string? configKey)
        {
            try
            {
                GetConfigurationMasterQuery req = new()
                {
                    ConfigGroup = string.IsNullOrEmpty(configGroup) ? null : configGroup,
                    ConfigType = string.IsNullOrEmpty(configKey) ? null : configKey,
                };
                var result = await _mediator.Send(req);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }

        }

        [HttpGet("GetConfigurationByConfigGroupConfigKeyAndConfigType")]
        [SanitizeInput]
        // found 2 instance > gateway and workflow
        public async Task<List<ConfigurationGroupResponse>> GetConfigurationByConfigGroupConfigKeyAndConfigType(string groupName, string configKey, string configType)
        {
            try
            {
                var result = await _mediator.Send(new GetConfigurationGroupConfigKeyAndConfigTypeQuery()
                {
                    ConfigGroup = groupName,
                    ConfigKey = configKey,
                    ConfigType = configType,
                    AttributeName = string.Empty,
                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetConfigurationGroupMaster")]
        [SanitizeInput]
        public async Task<List<ConfigurationGroupMaster>> GetConfigurationGroupMaster(string groupName, string configKey, string configType)
        {
            try
            {
                var result = await _mediator.Send(new GetConfigurationGroupMastersQuery()
                {
                    ConfigGroup = groupName,
                    ConfigKey = configKey,
                    ConfigType = configType,
                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetApplicationLevelSettings")]
        [SanitizeInput]
        public async Task<Dictionary<string, string>> GetApplicationLevelSettings([FromQuery] List<string>? keys)
        {
            try
            {
                var result = await _mediator.Send(new GetApplicationLevelSettingsDictQuery()
                {
                    keys = keys != null ? keys : new List<string>()
                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("LogContent")]
        public async Task<bool> LogContent([FromBody] LoggerCommand loggerDTO)
        {
            try
            {
                var result = await _mediator.Send(new LoggerCommand
                {
                    LogLevel = loggerDTO.LogLevel,
                    Category = loggerDTO.Category,
                    Message = loggerDTO.Message,
                    StackTrace = loggerDTO.StackTrace,
                    LogObjects = loggerDTO.LogObjects
                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return false;
            }
        }

        /// <summary>
        /// HandleException
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        private object HandleException(Exception ex)
        {
            Guid guid = Guid.NewGuid();
            this.LogException(ex, guid);
            throw new BadHttpRequestException($"{ex.Message}-errorid:{guid}", StatusCodes.Status400BadRequest);//, ex);
        }

    }
}
