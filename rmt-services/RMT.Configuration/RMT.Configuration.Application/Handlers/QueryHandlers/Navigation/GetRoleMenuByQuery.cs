using MediatR;
using RMT.Configuration.Application.DTOs.NavigationDTOs;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;
using System.Collections.Generic;

namespace RMT.Configuration.Application.Handlers.QueryHandlers
{
    public class GetRoleMenuByQuery : IRequest<List<RoleMenuDTO>>
    {
        public string _role { get; set; }

        public Int64 _menuId { get; set; }

    }
    public class GetRoleMenuByQueryHandler : IRequestHandler<GetRoleMenuByQuery, List<RoleMenuDTO>>
    {
        private readonly INavigationRepository _repository;
        public GetRoleMenuByQueryHandler(INavigationRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<RoleMenuDTO>> Handle(GetRoleMenuByQuery request, CancellationToken cancellationToken)
        {
            List<RoleMenu> result = await _repository.GetRoleMenuByFilter(request._role, request._menuId);

            List<RoleMenuDTO> response = ConfigurationMapper.Mapper.Map<List<RoleMenuDTO>>(result);

            return response;
        }
    }
}
