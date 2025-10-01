using MediatR;
using RMT.Allocation.Application.DTOs.RequisitionDTOs;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class RemoveUsersAllDraftAllocationsCommand : IRequest<bool>
    {
        public List<string> emails { get; set; }
    }
    public class RemoveUsersAllDraftAllocationsCommandHandler : IRequestHandler<RemoveUsersAllDraftAllocationsCommand, bool>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IMediator _mediator;
        public RemoveUsersAllDraftAllocationsCommandHandler(IResourceAllocationRepository resourceAllocationRepository, IMediator mediator)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(RemoveUsersAllDraftAllocationsCommand request, CancellationToken cancellationToken)
        {
            List<Guid> fetchAllocationGuidsToRemove = await _resourceAllocationRepository.GetAllDraftAllocationForEmployeeForRemoval(request.emails);
            if (fetchAllocationGuidsToRemove != null && fetchAllocationGuidsToRemove.Count > 0)
            {
                foreach (var item in fetchAllocationGuidsToRemove)
                {
                    await _mediator.Send(new ReleaseResourceCommand
                    {
                        guid = item

                    });
                }
            }
            return true;
        }

    }
}
