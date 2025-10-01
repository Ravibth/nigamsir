using MediatR;
using RMT.Configuration.Application.DTOs.NavigationDTOs;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;

namespace RMT.Configuration.Application.Handlers.CommandHandlers
{
    public class UpdateContextMenuMasterCommand : IRequest<ContextMenuMasterDTO>
    {
        public string InternalName { get; set; }
        public string DisplayName { get; set; }
        public Int32 Order { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

    }

    public class UpdateContextMenuMasterCommandHandler : IRequestHandler<UpdateContextMenuMasterCommand, ContextMenuMasterDTO>
    {
        private readonly INavigationRepository _repository;
        public UpdateContextMenuMasterCommandHandler(INavigationRepository repository)
        {
            _repository = repository;
        }
        public async Task<ContextMenuMasterDTO> Handle(UpdateContextMenuMasterCommand request, CancellationToken cancellationToken)
        {
            ContextMenuMaster _entity = ConfigurationMapper.Mapper.Map<ContextMenuMaster>(request);

            if (_entity is null)
            {
                throw new ApplicationException("Issue with mapper");
            }

            ContextMenuMaster result = await _repository.UpdateContextMenuMasterItem(_entity);

            ContextMenuMasterDTO _response = ConfigurationMapper.Mapper.Map<ContextMenuMasterDTO>(result);

            return _response;
        }
    }
}
