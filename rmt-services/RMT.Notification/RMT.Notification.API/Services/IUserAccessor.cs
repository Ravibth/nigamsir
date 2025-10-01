using static RMT.Notification.API.Constants;

namespace RMT.Notification.API.Services
{
    public interface IUserAccessor
    {
        UserDecorator GetUser();
        string GetToken();
    }
}
