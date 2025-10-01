using MediatR;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO.Response;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class SuspendAllocationCommand : IRequest<List<SuspendAllocationResponse>>
    {
        public List<KeyValuePair<string, string>> ProjectCode { get; set; }
    }
    public class SuspendAllocationCommandHandler : IRequestHandler<SuspendAllocationCommand, List<SuspendAllocationResponse>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IRequisitionRepository _requisitionRepository;
        public SuspendAllocationCommandHandler(IResourceAllocationRepository resourceAllocationRepository, IRequisitionRepository requisitionRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _requisitionRepository = requisitionRepository;
        }

        public async Task<List<SuspendAllocationResponse>> Handle(SuspendAllocationCommand request, CancellationToken cancellationToken)
        {
            List<KeyValuePair<string, string>> projectCodes = request.ProjectCode;
            await _resourceAllocationRepository.SuspendAllocations(projectCodes);
            await _resourceAllocationRepository.SuspendAllocationPerDay(projectCodes);
            List<SuspendAllocationResponse> allocationsDetails = await _resourceAllocationRepository.SuspendAllocationsDetails(projectCodes);
            await _requisitionRepository.SuspendAllocationRequisition(projectCodes);
            return allocationsDetails;
        }
    }
}
