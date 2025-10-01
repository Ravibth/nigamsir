using MediatR;
using RMT.Configuration.Domain.DTO;
using RMT.Configuration.Domain.Repositories;


namespace RMT.Configuration.Application.Handlers.CommandHandlers
{
    public class UpdateConfigurationBreakupCommand:IRequest<string>
    {
        public List<UpdateConfigurationBreakupRequestDTO> UpdateConfigurationBreakupRequests { get; set; }
        public string email { get; set; } = null;
    }
    public class UpdateConfigurationBreakupCommandHandler : IRequestHandler<UpdateConfigurationBreakupCommand, string>
    {
        private readonly IConfigurationRepository _configurationRepository;
        public UpdateConfigurationBreakupCommandHandler(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }
        public async Task<string> Handle(UpdateConfigurationBreakupCommand request, CancellationToken cancellationToken)
        {
            await _configurationRepository.UpdateConfigurationBreakup(request.UpdateConfigurationBreakupRequests , request.email);
            return "success";
        }
    }
}
