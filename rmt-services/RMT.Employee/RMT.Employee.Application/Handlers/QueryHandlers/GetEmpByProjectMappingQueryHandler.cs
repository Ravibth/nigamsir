using MediatR;
using RMT.Employee.Application.Mappers;
using RMT.Employee.Application.Response;
using RMT.Employee.Domain.DTOs;
using RMT.Employee.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Application.Handlers.QueryHandlers
{
    public class GetEmpByProjectMappingQuery : IRequest<List<EmpProjectMappingResponse>>
    {
        public EmpByProjectMappingRequestDto request { get; set; }
        public string UserEmail { get; set; }

    }

    public class GetEmpByProjectMappingQueryHandler : IRequestHandler<GetEmpByProjectMappingQuery, List<EmpProjectMappingResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmpByProjectMappingQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmpProjectMappingResponse>> Handle(GetEmpByProjectMappingQuery command, CancellationToken cancellationToken)
        {
            var result = await _employeeRepository.GetEmpByProjectMapping(command.request);

            List<EmpProjectMappingResponse> response = EmployeeMapper.Mapper.Map<List<EmpProjectMappingResponse>>(result);
            return response;
        }

    }
}
