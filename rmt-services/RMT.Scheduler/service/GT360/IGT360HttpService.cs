using RMT.Scheduler.DTOs.GT360;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Scheduler.service.GT360
{
    public interface IGT360HttpService
    {
        Task<GT360TimesheetResponseDto> PostTimeSheetData(GT360TimesheetRequestDto requestData);

    }
}
