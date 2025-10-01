using RMT.Reports.Application.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Application.IHttpServices
{
    public interface IWcgtHttpService
    {
        Task<List<GTBUTreeMappingDTO>> GetBUTreeMappingList();
        
        Task<List<GTCompetencyDTO>> GetBUCompetencyListByMID(string mid);

        Task<GetBuExpertiesDTO> GetBUTreeMappingListByMID(string mid);
        Task<List<GTEmployeeBaseDTO>> GetEmployeeBySuperCoachOrCSCByMID(string mid);
    }
}
