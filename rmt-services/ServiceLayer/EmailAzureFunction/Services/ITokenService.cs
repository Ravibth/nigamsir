using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface ITokenService
    {
        Task<string> GetToken();

        //Task<HttpClient> GetCustomHttpClient();

        Task<HttpClient> GetCustomHttpClient(string token);
    }
}
