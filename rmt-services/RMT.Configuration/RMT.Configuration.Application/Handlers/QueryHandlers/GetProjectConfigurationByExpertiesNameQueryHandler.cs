using MediatR;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;
using System.Collections.Generic;

namespace RMT.Configuration.Application.Handlers.QueryHandlers
{
    public class GetProjectConfigurationByExpertiesNameQuery : IRequest<List<ProjectConfiguration>>
    {
        public string BuOfferingName { get; set; }
        public string ConfigurationGroup { get; set; }
    }
    public class GetProjectConfigurationByExpertiesNameQueryHandler : IRequestHandler<GetProjectConfigurationByExpertiesNameQuery, List<ProjectConfiguration>>
    {
        private readonly IConfigurationRepository _configurationRepository;
        public GetProjectConfigurationByExpertiesNameQueryHandler(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }
        public async Task<List<ProjectConfiguration>> Handle(GetProjectConfigurationByExpertiesNameQuery request, CancellationToken cancellationToken)
        {
            var mainBreakup = await _configurationRepository.GetProjectConfigurationByExpertiesNameAndConfigGroup(request.BuOfferingName, request.ConfigurationGroup);

            List<ProjectConfiguration> response = new();

            if (mainBreakup != null)
            {
                foreach (var item in mainBreakup.ConfigurationMainBreakupMetaValues)
                {
                    ProjectConfiguration projectConfig = new()
                    {
                        AttributeName = request.BuOfferingName,
                        AttributeValue = item.Value,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        ModifiedAt = DateTime.UtcNow,
                        CreatedBy = mainBreakup.CreatedBy,
                        ModifiedBy = mainBreakup.ModifiedBy,
                        ConfigurationGroup = new ConfigurationGroup()
                        {
                            ConfigGroup = mainBreakup.ConfigurationMaster.ConfigGroup,
                            ConfigGroupDisplay = mainBreakup.ConfigurationMaster.ConfigGroupDisplay,
                            ConfigKey = item.Key,
                            ConfigType = mainBreakup.ConfigurationMaster.SelectorConfigType,
                            CongigDisplayText = item.DisplayKey,
                            IsActive = true
                        }
                    };
                    response.Add(projectConfig);
                }
            }

            return response;
        }
    }
}

