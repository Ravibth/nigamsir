using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Services.HttpServices;

namespace WCGT.Application.Services.IHttpServices
{
    public interface IIdentityHttpService
    {
        Task<List<SuperCoachModel>> GetSuperCoachMid(string mid);
    }
}
