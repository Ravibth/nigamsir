using RMT.Configuration.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Application.IHttpServices
{
    public interface IWCGTMasterHttpApi
    {
        Task<List<WCGTBUTreeMappingDTO>> GetWCGTBUTreeMappingListApiQuery();

    }
}
