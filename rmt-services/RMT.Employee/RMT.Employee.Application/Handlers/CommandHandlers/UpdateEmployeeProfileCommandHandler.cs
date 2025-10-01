using MediatR;
using RMT.Employee.Application.DTOs;
using RMT.Employee.Domain.Entities;
using RMT.Employee.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Application.Handlers.CommandHandlers
{
    public class UpdateEmployeeProfileCommand:IRequest<EmployeeProfile>
    {
        public UpdateEmployeeProfileRequest param { get; set; }
    }
    public class UpdateEmployeeProfileCommandHandler : IRequestHandler<UpdateEmployeeProfileCommand, EmployeeProfile>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateEmployeeProfileCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeProfile> Handle(UpdateEmployeeProfileCommand request, CancellationToken cancellationToken)
        {
            return await _employeeRepository.UpdateEmployeeProfile(request.param);
        }
    }
}
