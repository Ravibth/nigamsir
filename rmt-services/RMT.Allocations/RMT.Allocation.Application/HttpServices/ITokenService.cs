using System.Threading.Tasks;

namespace RMT.Scheduler.service
{
    public interface ITokenService
    {
        Task<string> GetToken();
    }
}
