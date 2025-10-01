using ServiceLayer.DTOs;
using ServiceLayer.Services.MarketPlaceService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.MarketPlaceService
{
    public interface IMarketPlaceService
    {
        Task<MarketPlaceProjectDetailResponse> UpdateMarkeplaceProjectDetails(NotificationPayloadDTO serviceBusPayload);
    }
}
