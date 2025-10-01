using MediatR;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class CreateOrUpdateActiveFieldForMarketPlaceCommand : IRequest<FieldForMarketPlace>
    {
        public string InternalName { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateOrUpdateActiveFieldForMarketPlaceCommandHandler : IRequestHandler<CreateOrUpdateActiveFieldForMarketPlaceCommand, FieldForMarketPlace>
    {
        private readonly IProjectRepository _ProjectRepo;
        public CreateOrUpdateActiveFieldForMarketPlaceCommandHandler(IProjectRepository ProjectRepository)
        {
            _ProjectRepo = ProjectRepository;
        }

        public async Task<FieldForMarketPlace> Handle(CreateOrUpdateActiveFieldForMarketPlaceCommand request, CancellationToken cancellationToken)
        {
            var _fieldForMarketPlace = await _ProjectRepo.CreateOrUpdateActiveFieldForMarketPlace(request.InternalName,request.DisplayName, request.IsActive);
            
            return _fieldForMarketPlace;
        }
    }

}
