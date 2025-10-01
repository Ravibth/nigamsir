using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.IHttpServices
{
    public interface IIdentityUserDetailsHttpApi
    {
        Task<List<IdentityUserResponseDTO>> GetEmployeesDataHttpApiQuery(List<string> request);

        Task<UserInfoDTO> GetUserInfo(string email);

        Task<List<UserDTO>> GetUsersByEmailDataHttpApiQuery(List<string> emails);

        Task<List<UserDTO>> GetEmailDataHttpApiQuery();
        Task<List<UserDTO>> GetUsersBySuperCoachEmailDataHttpApiQuery(List<string> emails);
        Task<List<UserDTO>> GetSupercoachUserListByAllocationSupercoachDelegate(string email);

    }
}
