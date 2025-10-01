using RMT.Skill.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.IHttpServices
{
    public interface IIdentityUserDetailsHttpApi
    {
        Task<List<IdentityUserResponseDTO>> GetEmployeesDataHttpApiQuery(List<string> request);
    }
}
