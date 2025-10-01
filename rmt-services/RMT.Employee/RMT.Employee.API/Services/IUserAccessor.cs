using RMT.Employee.Domain;

namespace RMT.Employee.API.Services
{
    public interface IUserAccessor
    {
        UserDecorator GetUser();
        string GetToken();
    }
}
