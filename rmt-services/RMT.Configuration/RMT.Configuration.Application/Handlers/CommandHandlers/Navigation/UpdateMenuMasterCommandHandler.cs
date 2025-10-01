using MediatR;
using RMT.Configuration.Application.DTOs.NavigationDTOs;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;

namespace RMT.Configuration.Application.Handlers.CommandHandlers
{
    public class UpdateMenuMasterCommand : IRequest<MenuMasterDTO>
    {
        public string InternalName { get; set; }
        public string DisplayName { get; set; }
        public string? ParentId { get; set; }
        public Int32 Order { get; set; }
        public string? MenuType { get; set; }
        public string Path { get; set; }
        public string? Description { get; set; }
        public bool Is_Expandable { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }

    }

    public class UpdateMenuMasterCommandHandler : IRequestHandler<UpdateMenuMasterCommand, MenuMasterDTO>
    {
        private readonly INavigationRepository _repository;
        public UpdateMenuMasterCommandHandler(INavigationRepository repository)
        {
            _repository = repository;
        }
        public async Task<MenuMasterDTO> Handle(UpdateMenuMasterCommand request, CancellationToken cancellationToken)
        {
            MenuMaster _entity = ConfigurationMapper.Mapper.Map<MenuMaster>(request);

            if (_entity is null)
            {
                throw new ApplicationException("Issue with mapper");
            }

            MenuMaster result = await _repository.UpdateMenuMasterItem(_entity);

            MenuMasterDTO _response = ConfigurationMapper.Mapper.Map<MenuMasterDTO>(result);

            return _response;
        }
    }
}
