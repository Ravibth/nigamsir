using MediatR;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetAllocationByRequisitionIDQery : IRequest<ResourceAllocationDetailsWithConsumedHours>
    {
        public Guid RequisitionId { get; set; }

    }

    public class GetAllocationByRequisitionIDQueryHandler : IRequestHandler<GetAllocationByRequisitionIDQery, ResourceAllocationDetailsWithConsumedHours>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public GetAllocationByRequisitionIDQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }

        public async Task<ResourceAllocationDetailsWithConsumedHours> Handle(GetAllocationByRequisitionIDQery request, CancellationToken cancellationToken)
        {
            ResourceAllocationDetailsResponse a = await _resourceAllocationRepository.GetAllocationByRequisitionId(request.RequisitionId);
            if (a is null)
            {
                return new ResourceAllocationDetailsWithConsumedHours();
            }
            else 
            {
                ResourceAllocationDetailsWithConsumedHours response = AllocationMapper.Mapper.Map<ResourceAllocationDetailsWithConsumedHours>(a);
                response.consumedHours = await _resourceAllocationRepository.GetWithConsumedHours(request.RequisitionId);
                return response;
            }
        }
    }

}
