using RMT.Scheduler.DTOs.GT360;
using System.Net.Http;
using System.Threading.Tasks;

namespace RMT.Scheduler.service
{
    public interface IGTTokenService
    {
        Task<GT360TokenResponseDto> GetToken();
        HttpClient GetCustomHttpClient();
    }
}
