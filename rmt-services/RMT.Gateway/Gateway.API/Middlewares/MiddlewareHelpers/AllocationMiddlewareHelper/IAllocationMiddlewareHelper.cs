using Gateway.API.Dtos;
using Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.API.Middlewares.MiddlewareHelpers.AllocationMiddlewareHelper
{
    public interface IAllocationMiddlewareHelper
    {
        Task<bool> AddUpdateProjectRequisitionAllocationForCreateRequisition(InitNotificationDTO notificationParams);
        Task<bool> AddUpdateProjectRequisitionAllocationForDeleteRequisitionById(InitNotificationDTO notificationParams);
        Task<bool> AddUpdateProjectRequisitionAllocationForReleaseResourceByGuid(InitNotificationDTO notificationParams);
        Task<bool> AddUpdateProjectRequisitionAllocationForBulkRequisitionUpload(InitNotificationDTO notificationParams);
        Task<bool> AddUpdateProjectRequisitionAllocationForUpdateListOfAllocationStatusInResourceAllocationDetails(UpdateListOfAllocationStatusInResourceAllocationDetailsResponse response);
        Task<bool> AddUpdateProjectRequisitionAllocationForCommonAllocation(InitNotificationDTO notificationDTOparams);
    }
}
