using RMT.MarketPlace.Domain;
using RMT.MarketPlace.Infrastructure;

namespace RMT.MarketPlace.API.Services
{
    public interface IUserAccessor
    {
        UserDecorator GetUser();
        string GetToken();
    }
}
