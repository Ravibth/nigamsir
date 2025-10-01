using MediatR;
using RMT.Employee.Application.Mappers;
using RMT.Employee.Application.Response;
using RMT.Employee.Domain.Entities;
using RMT.Employee.Domain.Repositories;

namespace RMT.Employee.Application.Handlers.CommandHandlers
{
    public class PreferenceMasterCommand : IRequest<PreferenceMasterResponse>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int SortOrder { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }

    }
    public class PreferenceMasterCommandHandler : IRequestHandler<PreferenceMasterCommand, PreferenceMasterResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public PreferenceMasterCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<PreferenceMasterResponse> Handle(PreferenceMasterCommand request, CancellationToken cancellationToken)
        {
            PreferenceMaster preferenceMaster = EmployeeMapper.Mapper.Map<PreferenceMaster>(request);
            if (preferenceMaster is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            var newPreferenceMaster = await _employeeRepository.AddPreferenceMasterAsync(preferenceMaster);
            PreferenceMasterResponse response = EmployeeMapper.Mapper.Map<PreferenceMasterResponse>(newPreferenceMaster);
            return response;
        }
    }
}
