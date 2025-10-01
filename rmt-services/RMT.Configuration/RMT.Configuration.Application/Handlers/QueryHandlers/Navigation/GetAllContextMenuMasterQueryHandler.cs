using MediatR;
using RMT.Configuration.Application.DTOs.NavigationDTOs;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;

namespace RMT.Configuration.Application.Handlers.QueryHandlers
{
    public class GetAllContextMenuMasterQuery : IRequest<List<ContextMenuMasterDTO>>
    {

    }
    public class GetAllContextMenuMasterQueryHandler : IRequestHandler<GetAllContextMenuMasterQuery, List<ContextMenuMasterDTO>>
    {
        private readonly INavigationRepository _repository;
        public GetAllContextMenuMasterQueryHandler(INavigationRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<ContextMenuMasterDTO>> Handle(GetAllContextMenuMasterQuery request, CancellationToken cancellationToken)
        {
            List<ContextMenuMaster> result = await _repository.GetAllContextMenuMaster();

            List<ContextMenuMasterDTO> response = ConfigurationMapper.Mapper.Map<List<ContextMenuMasterDTO>>(result);

            return response;
        }
    }
}
