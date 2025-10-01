using MediatR;
using RMT.Configuration.Application.DTOs.NavigationDTOs;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;

namespace RMT.Configuration.Application.Handlers.CommandHandlers
{
    public class UpdateRoleContextMenuCommand : IRequest<RoleContextMenuDTO>
    {
        public string Role { get; set; }

        public Int64 ContextMenuId { get; set; }

        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }

    public class UpdateRoleContextMenuCommandHandler : IRequestHandler<UpdateRoleContextMenuCommand, RoleContextMenuDTO>
    {
        private readonly INavigationRepository _repository;
        public UpdateRoleContextMenuCommandHandler(INavigationRepository repository)
        {
            _repository = repository;
        }

        public async Task<RoleContextMenuDTO> Handle(UpdateRoleContextMenuCommand request, CancellationToken cancellationToken)
        {
            RoleContextMenu _entity = ConfigurationMapper.Mapper.Map<RoleContextMenu>(request);

            if (_entity is null)
            {
                throw new ApplicationException("Issue with mapper");
            }

            RoleContextMenu result = await _repository.UpdateRoleContextMenuItem(_entity);

            RoleContextMenuDTO _response = ConfigurationMapper.Mapper.Map<RoleContextMenuDTO>(result);

            return _response;
        }

    }
}
