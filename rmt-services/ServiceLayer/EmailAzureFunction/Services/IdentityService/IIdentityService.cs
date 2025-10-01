using ServiceLayer.DTOs;
using ServiceLayer.Services.AllocationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.IdentityService
{
    public interface IIdentityService
    {
        Task<List<EmployeeInfoDTO>> GetEmployeeDetailsByEmails(List<string> emails, string token);

        Task<UserInfoDTO> GetUserInfo(string email);

    }
}
