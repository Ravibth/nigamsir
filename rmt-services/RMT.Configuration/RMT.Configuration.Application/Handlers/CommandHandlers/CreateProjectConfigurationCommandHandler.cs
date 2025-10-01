using MediatR;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;

namespace RMT.Configuration.Application.Handlers.CommandHandlers
{
    //public class CreateProjectConfigurationCommand : IRequest<ProjectConfiguration>
    //{
    //    public string AttributeName { get; set; }
    //    public string AttributeValue { get; set; }
    //    public Int64 ConfigId { get; set; }
    //    public bool IsActive { get; set; }
    //    public string CreatedBy { get; set; }
    //    public DateTime CreatedAt { get; set; }
    //    public string ModifiedBy { get; set; }
    //    public DateTime ModifiedAt { get; set; }
    //}
    //public class CreateProjectConfigurationCommandHandler : IRequestHandler<CreateProjectConfigurationCommand, ProjectConfiguration>
    //{
    //    private readonly IConfigurationRepository _configurationRepository;
    //    public CreateProjectConfigurationCommandHandler(IConfigurationRepository configurationRepository)
    //    {
    //        _configurationRepository = configurationRepository;
    //    }
    //    public async Task<ProjectConfiguration> Handle(CreateProjectConfigurationCommand request, CancellationToken cancellationToken)
    //    {
    //        var projectConfig = ConfigurationMapper.Mapper.Map<ProjectConfiguration>(request);
    //        if (projectConfig is null)
    //        {
    //            throw new ApplicationException("Issue with the mapper");
    //        }
    //        var projectConfiguration = await _configurationRepository.CreateProjectConfiguration(projectConfig);

    //        if (projectConfiguration != null && projectConfiguration.ConfigurationGroup != null)
    //        {
    //            var response = ConfigurationMapper.Mapper.Map<ConfigurationGroup>(projectConfiguration.ConfigurationGroup);

    //            projectConfiguration.ConfigurationGroup = response;
    //        }

    //        return projectConfiguration;
    //    }
    //}
}
