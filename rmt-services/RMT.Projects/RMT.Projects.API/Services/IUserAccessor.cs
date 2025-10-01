using RMT.Projects.Domain;

namespace RMT.Projects.API.Services
{
    public interface IUserAccessor
    {
        UserDecorator GetUser();
        string GetToken();
    }
}
