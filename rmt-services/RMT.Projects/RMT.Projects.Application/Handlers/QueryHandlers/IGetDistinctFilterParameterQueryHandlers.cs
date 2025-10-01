namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public interface IGetDistinctFilterParameterQueryHandlers
    {
        Task<string> Handle(GetDistinctFilterParameterQuery request, CancellationToken cancellationToken);
    }
}