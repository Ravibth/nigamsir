using MediatR;
using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class UpdatePublishAllocationActualEffortsCommand:IRequest<bool>
    {
        public List<UpdatePublishAllocationActualEffortsRequestDTO> AllocationActualEfforts { get; set; }
    }

    public class UpdatePublishAllocationActualEffortsCommandHandler : IRequestHandler<UpdatePublishAllocationActualEffortsCommand, bool>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public UpdatePublishAllocationActualEffortsCommandHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }
        public async Task<bool> Handle(UpdatePublishAllocationActualEffortsCommand request, CancellationToken cancellationToken)
        {
           await _resourceAllocationRepository.UpdatePublishAllocationActualEfforts(request.AllocationActualEfforts);
           return true;
        }
    }
}
