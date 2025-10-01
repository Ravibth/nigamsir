using RMT.Allocation.Application.HttpServices.DTOs;

namespace RMT.Allocation.Application.IHttpServices
{
    public interface IGetEmployeeLeavesHttpApi
    {
        Task<List<EmployeeLeavesDTO>> GetEmployeeLeavesByEmails(GetEmployeeLeaves request);
        Task<Int64> CalculateTotalUserLeavesInHours(EmployeeLeavesDTO employeeLeaves, Int64 weekends, Int64 working_hours);
    }
}
