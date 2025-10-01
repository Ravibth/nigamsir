using RMT.Allocation.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.DTOs
{
    public class HolidayResponseDTO
    {
      public  List<GTHolidayDTO> HolidayList { get; set; }
      public Dictionary<string, string> EmailLocationCollection { get; set; }

    }
}
