using MediatR;
using RMT.Configuration.Application.IHttpServices;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain.DTO;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;

namespace RMT.Configuration.Application.Handlers.QueryHandlers
{
    public class ProjectConfigurationByConfigGroupAndConfigTypeQuery : IRequest<List<ProjectConfiguration>>
    {
        public string ConfigGroup { get; set; }
        public string ConfigType { get; set; }
        public string AttributeName { get; set; }
    }
    public class ProjectConfigurationByConfigGroupAndConfigTypeQueryHandler : IRequestHandler<ProjectConfigurationByConfigGroupAndConfigTypeQuery, List<ProjectConfiguration>>
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IWCGTMasterHttpApi _WCGTMasterHttpApi;

        public ProjectConfigurationByConfigGroupAndConfigTypeQueryHandler(IConfigurationRepository configurationRepository, IWCGTMasterHttpApi wCGTMasterHttpApi)
        {
            _configurationRepository = configurationRepository;
            _WCGTMasterHttpApi = wCGTMasterHttpApi;
        }

        public async Task<List<ProjectConfiguration>> Handle(ProjectConfigurationByConfigGroupAndConfigTypeQuery request, CancellationToken cancellationToken)
        {
            List<WCGTBUTreeMappingDTO> buTreeMapping = await _WCGTMasterHttpApi.GetWCGTBUTreeMappingListApiQuery();

            var result = await _configurationRepository.GetProjectConfigurationsByConfigGroupAndConfigType(request.ConfigGroup, request.ConfigType, buTreeMapping, request.AttributeName);

            if (result != null && result.Count > 0)
            {
                foreach (var item in result)
                {
                    var response = ConfigurationMapper.Mapper.Map<ConfigurationGroup>(item.ConfigurationGroup);

                    item.ConfigurationGroup = response;
                }
            }

            return result;
        }
    }
}
