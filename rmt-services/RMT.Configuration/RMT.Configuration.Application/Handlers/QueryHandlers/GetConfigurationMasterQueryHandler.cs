using MediatR;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Application.Handlers.QueryHandlers
{
    public class GetConfigurationMasterQuery : IRequest<List<ConfigurationMaster>>
    {
        public string? ConfigGroup { get; set; }
        public string? ConfigType { get; set; }
    }
    public class GetConfigurationMasterQueryHandler : IRequestHandler<GetConfigurationMasterQuery, List<ConfigurationMaster>>
    {
        private readonly IConfigurationRepository _configurationRepository;

        public GetConfigurationMasterQueryHandler(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public async Task<List<ConfigurationMaster>> Handle(GetConfigurationMasterQuery request, CancellationToken cancellationToken)
        {
            List<ConfigurationMaster> result =  await _configurationRepository.GetConfigurationMasterByConfigGroupAndConfigTypeAsync(request.ConfigGroup, request.ConfigType);
            return result;
        }
    }
}
