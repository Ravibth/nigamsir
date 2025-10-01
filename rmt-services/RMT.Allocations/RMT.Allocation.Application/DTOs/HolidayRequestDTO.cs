using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.DTOs
{
    public class HolidayRequestDTO
    {
       public List<HolidayRequest> holidayParamsDTOs {  get; set; }
    }
    public class HolidayRequest
    {
        public string LocationName { get; set; }
        public DateTime? HolidayStartDate { get; set; }
    }
}
