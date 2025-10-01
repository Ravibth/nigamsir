//using MediatR;
//using RMT.Allocation.Application.Mappers;
//using RMT.Allocation.Application.Responses;
//using RMT.Allocation.Domain.Entities;
//using RMT.Allocation.Domain.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RMT.Allocation.Application.Handlers.QueryHandlers
//{
//    public class GetAllocationByIDQery : IRequest<ResourceAllocationResponse>
//    {
//        public int AllocationId { get; set; }
//    }

//    public class GetAllocationByIDQueryHandler : IRequestHandler<GetAllocationByIDQery, ResourceAllocationResponse>
//    {
//        private readonly IResourceAllocationRepository _resourceAllocationRepository;
//        public GetAllocationByIDQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
//        {
//            _resourceAllocationRepository = resourceAllocationRepository;
//        }

//        public async Task<ResourceAllocationResponse> Handle(GetAllocationByIDQery request, CancellationToken cancellationToken)
//        {
//            ResourceAllocation a = await _resourceAllocationRepository.GetAllocationById(request.AllocationId);
//            ResourceAllocationResponse response = AllocationMapper.Mapper.Map<ResourceAllocationResponse>(a);
//            //Console.WriteLine(a);
//            return response;
//        }

//    }
//}
