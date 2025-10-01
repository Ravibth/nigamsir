using MediatR;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Application.Handlers.QueryHandlers
{
    public class GetConfigurationGroupMastersQuery : IRequest<List<ConfigurationGroupMaster>>
    {

        public string ConfigGroup { get; set; }

        public string ConfigKey { get; set; }

        public string ConfigType { get; set; }

    }

    public class GetConfigurationGroupMastersQueryHandler : IRequestHandler<GetConfigurationGroupMastersQuery, List<ConfigurationGroupMaster>>
    {
        private readonly IConfigurationRepository _configurationRepository;

        public GetConfigurationGroupMastersQueryHandler(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }
        public async Task<List<ConfigurationGroupMaster>> Handle(GetConfigurationGroupMastersQuery request, CancellationToken cancellationToken)
        {
            return await _configurationRepository.GetAllConfigurationGroupMaster(request.ConfigGroup, request.ConfigKey, request.ConfigType);
        }
    }

}
