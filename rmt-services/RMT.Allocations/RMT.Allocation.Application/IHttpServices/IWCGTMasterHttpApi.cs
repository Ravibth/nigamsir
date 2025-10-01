using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.IHttpServices
{
    public interface IWCGTMasterHttpApi
    {
        Task<List<WcgtCompetencyMasterDTO>> GetCompetencyMaster();
        Task<List<WCGTJobCodeClientDTO>> GetClientListByJobCodes(List<string> jobcodes);
        Task<List<WCGTDesigantionDTO>> GetDesignationWCGTMAsterHttpApiQuery();

        Task<List<WCGTMasterDataDTO>> GetWCGTMAsterDataHttpApiQuery();

        Task<List<WCGTLocationDTO>> GetLocationWCGTMAsterHttpApiQuery();

        Task<List<WCGTIndustryDTO>> GetIndustryWCGTMAsterHttpApiQuery();

        Task<List<GetRateDesignationDTO>> GetRateByDesignation(List<GetRateDesignationRequestDTO> designations);
        Task<List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto>> GetResignedAndAbscondedUsersByEmails(List<string> emails);
        Task<List<GetUserLeaveHolidayWithUserMasterResponseForSystemSuggestion>> GetUserLeaveHolidayResponseForSystemSuggestion(GetUserLeaveHolidayWithUserMasterRequestDTOForSystemSuggestion request);
    }
}
