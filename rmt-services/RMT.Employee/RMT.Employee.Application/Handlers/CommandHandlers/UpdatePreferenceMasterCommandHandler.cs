using MediatR;
using RMT.Employee.Application.Mappers;
using RMT.Employee.Application.Response;
using RMT.Employee.Domain.Entities;
using RMT.Employee.Domain.Repositories;

namespace RMT.Employee.Application.Handlers.CommandHandlers
{
    public class UpdatePreferenceMasterCommand : IRequest<PreferenceMasterResponse>
    {
        public Int64 Id { get; set; }
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
    public class UpdatePreferenceMasterCommandHandler : IRequestHandler<UpdatePreferenceMasterCommand, PreferenceMasterResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdatePreferenceMasterCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<PreferenceMasterResponse> Handle(UpdatePreferenceMasterCommand request, CancellationToken cancellationToken)
        {
            var preferenceMaster = EmployeeMapper.Mapper.Map<PreferenceMaster>(request);
            if (preferenceMaster == null)
            {
                throw new ApplicationException("Issue With the mapper");
            }
            var prefMaster = await _employeeRepository.UpdatePreferenceMaster(preferenceMaster);
            var masterResponse = EmployeeMapper.Mapper.Map<PreferenceMasterResponse>(prefMaster);
            return masterResponse;
        }
    }
}
