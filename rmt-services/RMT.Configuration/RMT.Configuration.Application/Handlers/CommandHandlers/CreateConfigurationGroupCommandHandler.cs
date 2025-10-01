using MediatR;
using RMT.Configuration.Application.DTOs.ApplicationConfigurationDTOs;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;
using RMT.Configuration.Infrastructure.Repositories;

namespace RMT.Configuration.Application.Handlers.CommandHandlers
{
    ////checked
    //public class CreateConfigurationGroupCommand : IRequest<ConfigurationGroup>
    //{
    //    public string ConfigGroup { get; set; }
    //    public string ConfigGroupDisplay { get; set; }
    //    public string ConfigKey { get; set; }
    //    public string CongigDisplayText { get; set; }
    //    public string ValueType { get; set; }
    //    public string ConfigType { get; set; }
    //    public bool IsAll { get; set; }
    //    public string AllValue { get; set; }
    //    public bool IsActive { get; set; }
    //    public DateTime CreatedAt { get; set; }
    //    public DateTime ModifiedAt { get; set; }
    //    public string CreatedBy { get; set; }
    //    public string ModifiedBy { get; set; }

    //}
    //public class CreateConfigurationGroupCommandHandler : IRequestHandler<CreateConfigurationGroupCommand, ConfigurationGroup>
    //{
    //    private readonly IConfigurationRepository _configurationRepository;
    //    public CreateConfigurationGroupCommandHandler(IConfigurationRepository configurationRepository)
    //    {
    //        _configurationRepository = configurationRepository;
    //    }
    //    public async Task<ConfigurationGroup> Handle(CreateConfigurationGroupCommand request, CancellationToken cancellationToken)
    //    {
    //        var configGroup = ConfigurationMapper.Mapper.Map<ConfigurationGroup>(request);

    //        if (configGroup == null)
    //        {
    //            throw new ApplicationException("Issue With the mapper");
    //        }

    //        ConfigurationGroupMaster configGroupMaster = _configurationRepository.GetAllConfigurationGroupMaster(request.ConfigGroup, request.ConfigKey, request.ConfigType).Result.First();

    //        configGroup.ConfigurationGroupMaster = configGroupMaster;

    //        var result = await _configurationRepository.CreateConfigurationGroup(configGroup);

    //        ConfigurationGroup response = ConfigurationMapper.Mapper.Map<ConfigurationGroup>(result);

    //        return response;
    //    }
    //}
}
