using MediatR;
using RMT.Employee.Domain.Entities;
using RMT.Employee.Domain.Repositories;

namespace RMT.Employee.Application.Handlers.QueryHandlers
{
    public class GetEmployeePreferenceByEmailQuery : IRequest<List<EmployeePreference>>
    {
        public string EmployeeEmail { get; set; }
    }
    public class GetEmployeePreferenceByEmailQueryHandler : IRequestHandler<GetEmployeePreferenceByEmailQuery, List<EmployeePreference>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeePreferenceByEmailQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<List<EmployeePreference>> Handle(GetEmployeePreferenceByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetEmployeePreferencesByEmailAsync(request.EmployeeEmail);
        }
    }
}
