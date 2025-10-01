using RMT.Allocation.Domain;
using RMT.Allocation.Infrastructure;

namespace RMT.Allocations.API.Services
{
    public interface IUserAccessor
    {
        UserDecorator GetUser();
        string GetToken();
    }
}
