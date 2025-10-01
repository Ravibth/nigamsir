using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.HttpServices.HolidayService
{
    public interface IHolidayHttpService
    {
        Task<HolidayResponseDTO> GetLocationSpecificHolidays(List<string> emailIds, List<EmployeeMasterDTO>? employeeMaster, DateTime? startDate);
    }
}
