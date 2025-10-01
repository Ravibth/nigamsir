using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCGT.Application.Services.IHttpServices
{
    public interface IConfigurationHttpService
    {
        Task<Dictionary<string, string>> GetApplicationLevelSettings(List<string>? keys);
    }
}
