using MediatR;
using RMT.Reports.Infrastructure.Repositories;

namespace RMT.Reports.Application.Handlers.CommandHandlers
{
    public class RefreshEmployeeAllocationViewCommand : IRequest<bool>
    {

    }
    public class RefreshEmployeeAllocationViewCommandHandlers : IRequestHandler<RefreshEmployeeAllocationViewCommand, bool>
    {
        private readonly IReportRepository _repository;
        public RefreshEmployeeAllocationViewCommandHandlers(IReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(RefreshEmployeeAllocationViewCommand request, CancellationToken cancellationToken)
        {

            var result = await _repository.RefreshEmployeeAllocationView();

            return result;
        }
    }
}
