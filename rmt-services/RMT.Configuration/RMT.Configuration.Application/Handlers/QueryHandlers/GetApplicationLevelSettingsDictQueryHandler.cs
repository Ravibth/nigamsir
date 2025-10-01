using MediatR;
using RMT.Configuration.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Application.Handlers.QueryHandlers
{
    public class GetApplicationLevelSettingsDictQuery : IRequest<Dictionary<string, string>>
    {
        public List<string> keys { get; set; }
    }
    public class GetApplicationLevelSettingsDictQueryHandler : IRequestHandler<GetApplicationLevelSettingsDictQuery, Dictionary<string, string>>
    {
        private readonly IConfigurationRepository _configurationRepository;
        public GetApplicationLevelSettingsDictQueryHandler(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public async Task<Dictionary<string, string>> Handle(GetApplicationLevelSettingsDictQuery request, CancellationToken cancellationToken)
        {
            return await _configurationRepository.GetApplicationLevelSettingsDict(request.keys);
        }
    }
}
