//using MediatR;
//using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;
//using RMT.Allocation.Domain.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RMT.Allocation.Application.Handlers.QueryHandlers
//{
//    public class GetResourceByNameQuery : IRequest<List<GetResourceNameResponseDTO>>
//    {
//        public List<string> UserName { get; set; }
//    }


//    public class GetResourceByNameQueryHandler : IRequestHandler<GetResourceByNameQuery, List<GetResourceNameResponseDTO>>
//    {
//        private readonly IResourceAllocationRepository _resourceAllocationRepository;
//        public GetResourceByNameQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
//        {
//            _resourceAllocationRepository = resourceAllocationRepository;
//        }

//        public async Task<List<GetResourceNameResponseDTO>> Handle(GetResourceByNameQuery request, CancellationToken cancellationToken)
//        {
//            List<GetResourceNameResponseDTO> result = await _resourceAllocationRepository.GetResourceByName(request.UserName);
//            return await Task.FromResult(result);
//        }
//    }

//}
