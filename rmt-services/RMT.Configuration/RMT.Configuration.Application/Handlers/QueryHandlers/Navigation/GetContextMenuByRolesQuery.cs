using MediatR;
using RMT.Configuration.Application.DTOs.NavigationDTOs;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;
using System.Collections.Generic;

namespace RMT.Configuration.Application.Handlers.QueryHandlers
{
    public class GetContextMenuByRolesQuery : IRequest<List<ContextMenuMasterDTO>>
    {
        public List<string> _roles { get; set; }
        public UserDecorator userDecorator { get; set; }
    }
    public class GetContextMenuByRolesQueryHandler : IRequestHandler<GetContextMenuByRolesQuery, List<ContextMenuMasterDTO>>
    {
        private readonly INavigationRepository _repository;
        public GetContextMenuByRolesQueryHandler(INavigationRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<ContextMenuMasterDTO>> Handle(GetContextMenuByRolesQuery request, CancellationToken cancellationToken)
        {
            List<ContextMenuMaster> result = await _repository.GetContextMenuByRoles(request._roles, request.userDecorator);

            List<ContextMenuMasterDTO> response = ConfigurationMapper.Mapper.Map<List<ContextMenuMasterDTO>>(result);

            return response;
        }
    }
}
