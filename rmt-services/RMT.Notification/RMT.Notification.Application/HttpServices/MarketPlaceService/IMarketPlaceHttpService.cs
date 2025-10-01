using RMT.Notification.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.HttpServices.MarketPlaceService
{
    public interface IMarketPlaceHttpService
    {
        Task<List<MarketPlaceProjectDetaillsIntrestDTO>> GetMarketPlaceDetailsIntrest();
    }
}
