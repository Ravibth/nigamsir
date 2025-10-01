using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Domain.DTO;

namespace RMT.Allocation.Application.IHttpServices
{
    public interface IEmployeeMasterHttpApi
    {
        Task<List<EmployeeMasterDTO>> GetEmployeeMasterDataHttpApiQuery(GetEmployeeMasterDetailsDTO request, string token);
        Task<List<EmployeeOfferingSolutionResp>> GetEmpByProjectMapping(List<string>? offerings, List<string>? solutions);

    }
}
