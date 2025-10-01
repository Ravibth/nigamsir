using RMT.Employee.Application.DTOs;
using RMT.Employee.Domain.DTOs;
using RMT.Employee.Domain.Entities;

namespace RMT.Employee.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<PreferenceMaster> AddPreferenceMasterAsync(PreferenceMaster preferenceMaster);
        Task<EmployeePreference> AddEmployeePreferenceAsync(EmployeePreference employeePreference);
        Task<List<EmployeePreference>> GetEmployeePreferencesByEmailAsync(string employeeEmail);
        Task<List<PreferenceMaster>> GetPreferenceMastersAsync();
        Task<List<EmployeePreference>> UpdateAsync(List<EmployeePreference> employeePreferences, string userEmail);
        Task<List<EmployeePreference>> GetEmployeePreferencesByEmails(List<string> emails);
        Task<PreferenceMaster> UpdatePreferenceMaster(PreferenceMaster preferenceMaster);

        Task<List<EmployeeProjectMapping>> UpdateEmployeeProjectMapping(List<EmployeeProjectMapping> employeePreferences, string userEmail);

        Task<List<EmployeeProjectMapping>> GetEmpByProjectMapping(EmpByProjectMappingRequestDto request);
        Task<EmployeeProfile> GetEmployeeProfileByEmployeeEmail(string employee_email);
        Task<EmployeeProfile> UpdateEmployeeProfile(UpdateEmployeeProfileRequest req);
    }
}
