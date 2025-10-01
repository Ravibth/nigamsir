using RMT.Reports.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Application.IHttpServices
{
    public interface IEmployeeHttpService
    {
        Task<List<EmpProjectMappingResponse>?> GetEmpByProjectMapping(List<string>? offerings, List<string>? solutions);

    }
}
