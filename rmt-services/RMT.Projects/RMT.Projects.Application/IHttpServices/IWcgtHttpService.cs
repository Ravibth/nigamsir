using RMT.Projects.Application.HttpServices.DTOs;
using RMT.Projects.Domain.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.IHttpServices
{
    public interface IWcgtHttpService
    {
        Task<GetBuExpertiesDTO> GetBUExpertiesByMID(string mid);
        Task<List<CompetencyMasterDTO>> GetCompetencyMasterByMid(string mid);
    }
}
