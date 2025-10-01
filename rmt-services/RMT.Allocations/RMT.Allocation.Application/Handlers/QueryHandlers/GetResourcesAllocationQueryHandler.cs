using MediatR;
using RMT.Allocation.Application.Mappers;
//using RMT.Allocation.Application.Responses;
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
    public class GetResourceAllocationByEmailQuery : IRequest<List<ResourceAllocationResponse>>
    {
        public string EmpEmail { get; set; }
    }

    public class GetResourceAllocationByEmailQueryHandler : IRequestHandler<GetResourceAllocationByEmailQuery, List<ResourceAllocationResponse>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public GetResourceAllocationByEmailQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }

        public async Task<List<ResourceAllocationResponse>> Handle(GetResourceAllocationByEmailQuery request, CancellationToken cancellationToken)
        {
            var a = await _resourceAllocationRepository.GetResourceAllocationByEmail(request.EmpEmail);
            List<ResourceAllocationResponse> response = AllocationMapper.Mapper.Map<List<ResourceAllocationResponse>>(a);
            //Console.WriteLine(a);
            return response;
        }
    }
}
