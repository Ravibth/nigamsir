using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.DTOs.MarketPlace
{
    public class UpdateExpiredMarketPlaceProjectsDto
    {
        public DateTime ExpiryDate { get; set; }
        public int DaysAdjustment { get; set; }
    }
}
