using RMT.Configuration.Domain;

namespace RMT.Configuration.API.Service
{
    public interface IUserAccessor
    {
        UserDecorator GetUser();
        string GetToken();
    }
}
