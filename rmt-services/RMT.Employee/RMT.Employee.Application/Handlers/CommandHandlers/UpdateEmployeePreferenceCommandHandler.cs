using MediatR;
using Newtonsoft.Json;
using RMT.Employee.Application.DTOs.EmployeePreferenceDTOs;
using RMT.Employee.Application.Mappers;
using RMT.Employee.Domain.DTOs;
using RMT.Employee.Domain.Entities;
using RMT.Employee.Domain.Repositories;

namespace RMT.Employee.Application.Handlers.CommandHandlers
{
    public class UpdateEmployeePreferenceCommand : IRequest<List<EmployeePreference>>
    {
        public List<UpdateEmployeePreferenceDTO> EmployeePreferences { get; set; }
        public string UserEmail { get; set; }
        //public Int64 Id { get; set; }
        //public string? EmployeeEmail { get; set; }
        //public Int64 PreferedValue { get; set; }
        //public string? Category { get; set; }
        //public DateTime? ModifiedAt { get; set; }
        //public string? ModifiedBy { get; set; }
        //public bool? IsActive { get; set; }
    }
    public class UpdateEmployeePreferenceCommandHandler : IRequestHandler<UpdateEmployeePreferenceCommand, List<EmployeePreference>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateEmployeePreferenceCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<List<EmployeePreference>> Handle(UpdateEmployeePreferenceCommand request, CancellationToken cancellationToken)
        {
            List<EmployeePreference> entity = new();
            foreach (var item in request.EmployeePreferences)
            {
                var prefItem = new EmployeePreference
                {
                    Id = (long)(item.Id == null ? -1 : item.Id),
                    Category = item.Category,
                    CreatedAt = (DateTime)(item.CreatedAt != null ? item.CreatedAt : DateTime.UtcNow),
                    CreatedBy = string.IsNullOrEmpty(item.CreatedBy) ? request.UserEmail : item.CreatedBy ,
                    EmployeeEmail = request.UserEmail,
                    IsActive = true,
                    ModifiedAt = DateTime.UtcNow,
                    ModifiedBy = request.UserEmail,
                    PreferenceOrder = item.PreferenceOrder != null ? 1 :2 ,
                    PreferenceDetails = new PreferenceDetails
                    {
                        businessUnit = item.businessUnit,
                        offering = item.offering,
                        solution = item.solution,
                        location = item.location,
                        industry = item.industry,
                        subIndustry= item.subIndustry,
                    }
                };
                entity.Add(prefItem);
            }
            var updatedData = await _employeeRepository.UpdateAsync(entity , request.UserEmail);
            return updatedData;
        }
    }
}
