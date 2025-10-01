//using MediatR;
//using RMT.Allocation.Application.Mappers;
//using RMT.Allocation.Application.Responses;
//using RMT.Allocation.Domain.Entities;
//using RMT.Allocation.Domain.Repositories;
//using RMT.Allocation.Infrastructure.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RMT.Allocation.Application.Handlers.CommandHandlers
//{

//    public class DeleteAllocationByIdCommand : IRequest<Boolean>
//    {
//        public IEnumerable<Guid> Ids { get; set; }

//    }

//    public class DeleteAllocationByIdCommandHandler : IRequestHandler<DeleteAllocationByIdCommand, Boolean>
//    {
//        private readonly IResourceAllocationRepository _resourceAllocationRepository;
//        public DeleteAllocationByIdCommandHandler(IResourceAllocationRepository resourceAllocationRepository)
//        {
//            _resourceAllocationRepository = resourceAllocationRepository;
//        }

//        public async Task<Boolean> Handle(DeleteAllocationByIdCommand request, CancellationToken cancellationToken)
//        {
//            var response = await _resourceAllocationRepository.DeleteAllocationByRequisitionId(request.Ids.ToList());
//            return response;
//        }

//    }

//}
