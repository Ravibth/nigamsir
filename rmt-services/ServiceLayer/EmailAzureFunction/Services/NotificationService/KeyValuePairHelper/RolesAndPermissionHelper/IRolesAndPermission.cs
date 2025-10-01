using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.NotificationService.KeyValuePairHelper.RolesAndPermissionHelper.RolesAndPermissionHelper
{
    public interface IRolesAndPermission
    {
        Task<List<Dictionary<string, string>>> USER_DISABLED_FROM_RMS_NOTIFICATION_TO_PROJECT_REQUESTOR(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
    }
}
