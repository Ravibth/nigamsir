using MediatR;
using RMT.Employee.Application.DTOs;
using RMT.Employee.Application.Mappers;
using RMT.Employee.Application.Response;
using RMT.Employee.Domain.Entities;
using RMT.Employee.Domain.Repositories;

namespace RMT.Employee.Application.Handlers.CommandHandlers
{
    public class EmpProjectMappingCommand : IRequest<List<EmpProjectMappingResponse>>
    {
        public List<EmployeeProjectMappingDTO> EmployeeProjectMappings { get; set; }
        public string UserEmail { get; set; }

    }

    public class EmpProjectMappingCommandHandler : IRequestHandler<EmpProjectMappingCommand, List<EmpProjectMappingResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmpProjectMappingCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmpProjectMappingResponse>> Handle(EmpProjectMappingCommand request, CancellationToken cancellationToken)
        {
            List<EmployeeProjectMapping> _data = EmployeeMapper.Mapper.Map<List<EmployeeProjectMapping>>(request.EmployeeProjectMappings);
            if (_data is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            var newEmployeeProjectMapping = await _employeeRepository.UpdateEmployeeProjectMapping(_data, request.UserEmail);
            List<EmpProjectMappingResponse> response = EmployeeMapper.Mapper.Map<List<EmpProjectMappingResponse>>(newEmployeeProjectMapping);
            return response;
        }

    }
}
