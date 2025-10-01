using MediatR;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;

namespace RMT.Configuration.Application.Handlers.QueryHandlers
{
    public class GetAllMenuMasterQuery : IRequest<List<MenuMaster>>
    {

    }
    public class GetAllMenuMasterQueryHandler : IRequestHandler<GetAllMenuMasterQuery, List<MenuMaster>>
    {
        private readonly INavigationRepository _repository;
        public GetAllMenuMasterQueryHandler(INavigationRepository repository)
        {
            _repository = repository;
        }
        public Task<List<MenuMaster>> Handle(GetAllMenuMasterQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetAllMenuMaster();
        }
    }
}
