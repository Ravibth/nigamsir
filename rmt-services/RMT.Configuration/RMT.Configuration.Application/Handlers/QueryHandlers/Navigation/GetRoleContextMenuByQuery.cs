using MediatR;
using RMT.Configuration.Application.DTOs.NavigationDTOs;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;
using System.Collections.Generic;

namespace RMT.Configuration.Application.Handlers.QueryHandlers
{
    public class GetRoleContextMenuByQuery : IRequest<List<RoleContextMenuDTO>>
    {
        public string _role { get; set; }

        public Int64 _menuId { get; set; }

    }
    public class GetRoleContextMenuByQueryHandler : IRequestHandler<GetRoleContextMenuByQuery, List<RoleContextMenuDTO>>
    {
        private readonly INavigationRepository _repository;
        public GetRoleContextMenuByQueryHandler(INavigationRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<RoleContextMenuDTO>> Handle(GetRoleContextMenuByQuery request, CancellationToken cancellationToken)
        {
            List<RoleContextMenu> result = await _repository.GetRoleContextMenuByFilter(request._role, request._menuId);

            List<RoleContextMenuDTO> response = ConfigurationMapper.Mapper.Map<List<RoleContextMenuDTO>>(result);

            return response;
        }
    }
}
