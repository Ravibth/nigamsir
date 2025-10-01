using MediatR;
using RMT.Projects.Domain.DTOs.Request;
using RMT.Projects.Domain.Repositories;

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class AddUpdateProjectRequisitionAllocationCommand : IRequest<bool>
    {
        public List<ProjectRequisitionAllocationRequestDTO> projectRequisitionAllocationRequestDTOs { get; set; }
        public string updatedBy { get; set; }
    }
    public class AddUpdateProjectRequisitionAllocationCommandHandler : IRequestHandler<AddUpdateProjectRequisitionAllocationCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;

        public AddUpdateProjectRequisitionAllocationCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<bool> Handle(AddUpdateProjectRequisitionAllocationCommand request, CancellationToken cancellationToken)
        {
            return await _projectRepository.AddUpdateProjectRequisitionAllocation(request.projectRequisitionAllocationRequestDTOs, request.updatedBy);
        }
    }
}