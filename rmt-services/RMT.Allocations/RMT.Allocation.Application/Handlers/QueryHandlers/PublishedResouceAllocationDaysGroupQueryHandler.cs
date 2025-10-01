using MediatR;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class PublishedResouceAllocationDaysGroupQuery:IRequest<List<AllocationDayResourceGroup>>
    {

    }
    public class PublishedResouceAllocationDaysGroupQueryHandler : IRequestHandler<PublishedResouceAllocationDaysGroupQuery, List<AllocationDayResourceGroup>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public PublishedResouceAllocationDaysGroupQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }
        public async Task<List<AllocationDayResourceGroup>> Handle(PublishedResouceAllocationDaysGroupQuery request, CancellationToken cancellationToken)
        {
            var result = await _resourceAllocationRepository.PublishedResouceAllocationDaysGroup();
            return result;
        }
    }
}
