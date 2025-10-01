using RMT.Reports.Domain;

namespace RMT.Report.Api.Services
{
    public interface IUserAccessor
    {
        UserDecorator GetUser();
        string GetToken();
    }
}
