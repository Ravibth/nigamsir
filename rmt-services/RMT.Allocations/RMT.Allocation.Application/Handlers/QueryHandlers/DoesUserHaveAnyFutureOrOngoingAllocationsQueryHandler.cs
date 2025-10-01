using MediatR;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class DoesUserHaveAnyFutureOrOngoingAllocationsQuery : IRequest<List<string>>
    {
        public List<string> emails { get; set; }
    }
    public class DoesUserHaveAnyFutureOrOngoingAllocationsQueryHandler : IRequestHandler<DoesUserHaveAnyFutureOrOngoingAllocationsQuery, List<string>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public DoesUserHaveAnyFutureOrOngoingAllocationsQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }
        public async Task<List<string>> Handle(DoesUserHaveAnyFutureOrOngoingAllocationsQuery request, CancellationToken cancellationToken)
        {
            return await _resourceAllocationRepository.DoesUserHaveAnyFutureOrOngoingAllocations(request.emails);
        }
    }   
}
