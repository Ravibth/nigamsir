using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.NotificationService.KeyValuePairHelper.MarketPlaceHelper
{
    public interface IMarketPlace
    {
        Task<List<Dictionary<string, string>>> NOTIFICATION_FOR_INTEREST_IN_MARKETPLACE_AGAINST_THEIR_PROJECT(InitNotificationDTO notificationPayloadDTO, List<NotificationPlaceHolderDTO> requiredKeys);
    }
}
