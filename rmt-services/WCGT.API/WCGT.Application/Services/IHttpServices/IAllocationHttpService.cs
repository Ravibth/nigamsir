using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Services.DTO;

namespace WCGT.Application.Services.IHttpServices
{
    public interface IAllocationHttpService
    {
        Task<List<PublishedResourceAllocationDayResponse>> PublishedResourceAllocationDays(List<string> empEmail, DateTime startDate, DateTime endDate);
    }
}
