using RMT.Allocation.Application.HttpServices.DTOs;

namespace RMT.Allocation.Application.IHttpServices
{
    public interface IEmployeePreferencesHttpApi
    {
        Task<List<EmployeePreferencesByEmailDTO>> GetEmployeePreferenceDetailsByEmails(GetEmployeePreferenceDetailsDTO request);
    }
}
