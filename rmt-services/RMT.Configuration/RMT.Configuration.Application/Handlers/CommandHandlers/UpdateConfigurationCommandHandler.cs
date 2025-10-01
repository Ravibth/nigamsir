using MediatR;
using RMT.Configuration.Application.IHttpServices;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain.DTO;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;
using RMT.Configuration.Infrastructure.Repositories;
using System.Collections.Generic;

namespace RMT.Configuration.Application.Handlers.CommandHandlers
{
    //checked
    public class UpdateConfiguration
    {
        public Int64 Id { get; set; }
        public string ConfigGroup { get; set; }
        public string ConfigGroupDisplay { get; set; }
        public string ConfigKey { get; set; }
        public string CongigDisplayText { get; set; }
        public string ValueType { get; set; } // stri , bool ,
        public string ConfigType { get; set; }
        public bool IsAll { get; set; }
        public string AllValue { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public List<ProjectConfiguration> ProjectConfigurations { get; set; }
        //public Int64 Id { get; set; }
        //public Int64 ConfigId { get; set; }
        //public string AttributeName { get; set; }
        //public string AttributeValue { get; set; }
        //public bool IsActive { get; set; }
        //public string? CreatedBy { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public string ModifiedBy { get; set; }
        //public DateTime ModifiedAt { get; set; }
        //public ConfigurationGroup ConfigurationGroup { get; set; }
    }
    public class UpdateConfigurationCommand : IRequest<List<ProjectConfiguration>>
    {
        public List<UpdateConfiguration> UpdatedConfigurations { get; set; }
        public string ConfigurationType { get; set; }
    }
    public class UpdateConfigurationCommandHandler : IRequestHandler<UpdateConfigurationCommand, List<ProjectConfiguration>>
    {
        private readonly IConfigurationRepository _configuration;
        private readonly IWCGTMasterHttpApi _WCGTMasterHttpApi;

        public UpdateConfigurationCommandHandler(IConfigurationRepository configuration, IWCGTMasterHttpApi wCGTMasterHttpApi)
        {
            _configuration = configuration;
            _WCGTMasterHttpApi = wCGTMasterHttpApi;
        }

        //checked
        public async Task<List<ProjectConfiguration>> Handle(UpdateConfigurationCommand request, CancellationToken cancellationToken)
        {
            List<ProjectConfiguration> result = null;

            if (request.UpdatedConfigurations != null)
            {

                List<WCGTBUTreeMappingDTO> buTreeMapping = await _WCGTMasterHttpApi.GetWCGTBUTreeMappingListApiQuery();

                List<ConfigurationGroupMaster> configGroupMasters = _configuration.GetAllConfigurationGroupMaster(null, null, null).Result;

                List<ConfigurationGroup> updatedProjectConfigurations = new List<ConfigurationGroup>();

                ConfigurationGroupMaster configGroupMaster = null;

                ConfigurationGroup configGroup = null;

                foreach (var item in request.UpdatedConfigurations)
                {
                    configGroup = ConfigurationMapper.Mapper.Map<ConfigurationGroup>(item);

                    var temp = configGroupMasters.Where(a => a.ConfigGroup == item.ConfigGroup
                                                    && a.ConfigKey == item.ConfigKey
                                                    ).First();

                    configGroup.ConfigurationGroupMasterId = temp?.Id;
                    configGroup.ConfigurationGroupMaster = temp;

                    updatedProjectConfigurations.Add(configGroup);
                }

                result = await _configuration.UpdateProjectConfiguration(updatedProjectConfigurations, request.ConfigurationType, buTreeMapping);
            }
            else
            {
                throw new Exception("Input object is null!");
            }
            return result;
        }
    }
}
