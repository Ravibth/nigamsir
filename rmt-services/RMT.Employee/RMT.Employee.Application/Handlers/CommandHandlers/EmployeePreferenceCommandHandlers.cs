using MediatR;
using Newtonsoft.Json;
using RMT.Employee.Application.Mappers;
using RMT.Employee.Application.Response;
using RMT.Employee.Domain.DTOs;
using RMT.Employee.Domain.Entities;
using RMT.Employee.Domain.Repositories;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RMT.Employee.Application.Handlers.CommandHandlers
{
    public class EmployeePreferenceCommand : IRequest<EmployeePreferenceResponse>
    {
        public Int64 Id { get; set; }

        public string EmployeeEmail { get; set; }

        public string PreferenceName { get; set; }
        public string PreferedValue { get; set; }

        public string PreferenceId { get; set; }

        public string Category { get; set; }
        public string PreferenceInfo { get; set; } = "{}";
        public int PreferenceOrder { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public bool IsActive { get; set; }
        public PreferenceDetails PreferenceDetails { get; set; }
    }
    public class EmployeePreferenceCommandHandlers : IRequestHandler<EmployeePreferenceCommand, EmployeePreferenceResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeePreferenceCommandHandlers(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeePreferenceResponse> Handle(EmployeePreferenceCommand request, CancellationToken cancellationToken)
        {
            var EmployeePreference = EmployeeMapper.Mapper.Map<EmployeePreference>(request);
            if (EmployeePreference is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            var newEmployeePreference = await _employeeRepository.AddEmployeePreferenceAsync(EmployeePreference);
            EmployeePreferenceResponse response = EmployeeMapper.Mapper.Map<EmployeePreferenceResponse>(newEmployeePreference);
            return response;
        }
    }
}
