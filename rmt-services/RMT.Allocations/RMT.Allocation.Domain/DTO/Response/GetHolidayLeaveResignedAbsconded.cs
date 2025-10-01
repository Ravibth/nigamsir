using RMT.Allocation.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Domain.DTO.Response
{
    public class GetHolidayLeaveResignedAbsconded
    {
        public List<GTHolidayDTO> HolidayResponseTask { get; set; }
        public List<GTLeaveBaseDTO> LeavesResponseTask { get; set; }
        public List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto> ResignedAbscondedResponse { get; set; }
    }
}
