using MediatR;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class PublishedResourceAllocationDaysQuery : IRequest<List<ResourceAllocationDaysResponse>>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string>? EmpEmail { get; set; }
    }
    public class PublishedResourceAllocationDaysQueryHandler : IRequestHandler<PublishedResourceAllocationDaysQuery, List<ResourceAllocationDaysResponse>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public PublishedResourceAllocationDaysQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }
        public async Task<List<ResourceAllocationDaysResponse>> Handle(PublishedResourceAllocationDaysQuery request, CancellationToken cancellationToken)
        {
            var response = await _resourceAllocationRepository.GetPublishedResourceAllocationDays(request.EmpEmail, (DateTime)request.StartDate, (DateTime)request.EndDate);
            return response;
        }
    }
}
