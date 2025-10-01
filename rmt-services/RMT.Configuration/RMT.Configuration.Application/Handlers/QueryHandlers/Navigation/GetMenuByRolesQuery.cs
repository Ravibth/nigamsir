using MediatR;
using RMT.Configuration.Application.DTOs.NavigationDTOs;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;
using System.Collections.Generic;

namespace RMT.Configuration.Application.Handlers.QueryHandlers
{
    public class GetMenuByRolesQuery : IRequest<List<MenuMasterDTO>>
    {
        public List<string> _roles { get; set; }
    }
    public class GetMenuByRolesQueryHandler : IRequestHandler<GetMenuByRolesQuery, List<MenuMasterDTO>>
    {
        private readonly INavigationRepository _repository;
        public GetMenuByRolesQueryHandler(INavigationRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<MenuMasterDTO>> Handle(GetMenuByRolesQuery request, CancellationToken cancellationToken)
        {
            List<MenuMaster> result = await _repository.GetMenuByRoles(request._roles);

            List<MenuMasterDTO> response = ConfigurationMapper.Mapper.Map<List<MenuMasterDTO>>(result);

            return response;
        }
    }
}
