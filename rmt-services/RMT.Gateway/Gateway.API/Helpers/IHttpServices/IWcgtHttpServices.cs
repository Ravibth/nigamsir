using Gateway.API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.API.Helpers.IHttpServices
{
    public interface IWcgtHttpServices
    {
        Task<GTBUExpertiesGroupDTO> GetBUTreeMappingListByMID(string mid);
        Task<List<CompetencyMasterDTO>> GetCompetencyMasterByMid(string mid);
    }

}
