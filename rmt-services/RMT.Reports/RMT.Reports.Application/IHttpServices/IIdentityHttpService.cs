using RMT.Reports.Application.HttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Application.IHttpServices
{
    public interface IIdentityHttpService
    {
        Task<List<SuperCoachModel>> GetSuperCoachMid(string mid);
    }
}
