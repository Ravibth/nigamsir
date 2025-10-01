using RMT.Notification.Application.HttpServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.HttpServices.AllocationService
{
    public interface IAllocationHttpService
    {
        Task<List<RequisitionResponse>> GetRequistionByDate(DateTime CreatedAt, DateTime ModifiedAt);
        Task<List<PublishedAllocationResponse>> GetAllocationByDate(DateTime CreatedAt, DateTime ModifiedAt);
    }
}
