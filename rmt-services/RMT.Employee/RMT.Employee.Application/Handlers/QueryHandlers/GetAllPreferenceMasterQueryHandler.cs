using MediatR;
using RMT.Employee.Domain.Entities;
using RMT.Employee.Domain.Repositories;

namespace RMT.Employee.Application.Handlers.QueryHandlers
{
    public class GetAllPreferenceMasterQuery : IRequest<List<PreferenceMaster>>
    {
    }
    public class GetAllPreferenceMasterQueryHandler : IRequestHandler<GetAllPreferenceMasterQuery, List<PreferenceMaster>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetAllPreferenceMasterQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<PreferenceMaster>> Handle(GetAllPreferenceMasterQuery request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetPreferenceMastersAsync();
        }
    }
}
