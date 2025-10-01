using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.IHttpServices
{
    public interface IMarketPlaceHttpApi
    {
        Task<string[]> GetListOfUsersInterestedByPipelineCode(string pipelineCode, string jobCode);
    }
}
