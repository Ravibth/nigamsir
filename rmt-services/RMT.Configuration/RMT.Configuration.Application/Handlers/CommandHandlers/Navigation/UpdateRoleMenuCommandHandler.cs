using MediatR;
using RMT.Configuration.Application.DTOs.NavigationDTOs;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;

namespace RMT.Configuration.Application.Handlers.CommandHandlers
{
    public class UpdateRoleMenuCommand : IRequest<RoleMenuDTO>
    {
        public string Role { get; set; }

        public Int64 MenuId { get; set; }

        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
    }

    public class UpdateRoleMenuCommandHandler : IRequestHandler<UpdateRoleMenuCommand, RoleMenuDTO>
    {
        private readonly INavigationRepository _repository;
        public UpdateRoleMenuCommandHandler(INavigationRepository repository)
        {
            _repository = repository;
        }

        public async Task<RoleMenuDTO> Handle(UpdateRoleMenuCommand request, CancellationToken cancellationToken)
        {
            RoleMenu _entity = ConfigurationMapper.Mapper.Map<RoleMenu>(request);

            if (_entity is null)
            {
                throw new ApplicationException("Issue with mapper");
            }

            RoleMenu result = await _repository.UpdateRoleMenuItem(_entity);

            RoleMenuDTO _response = ConfigurationMapper.Mapper.Map<RoleMenuDTO>(result);

            return _response;
        }

    }
}
