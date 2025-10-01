//using MediatR;
//using RMT.Allocation.Application.HttpServices;
//using RMT.Allocation.Application.HttpServices.DTOs;
//using RMT.Allocation.Application.IHttpServices;
//using RMT.Allocation.Application.Mappers;
//using RMT.Allocation.Application.Responses;

//using RMT.Allocation.Domain.Entities;
//using RMT.Allocation.Domain.Repositories;
//using RMT.Allocation.Infrastructure;

//namespace RMT.Allocation.Application.Handlers.CommandHandlers
//{
//    public class RejectResourceAllocationCommand : IRequest<Boolean>
//    { 
//        public Int64 Requisition_Id { get; set; }
//    }
//    public class RejectResourceAllocationCommandHandler : IRequestHandler<RejectResourceAllocationCommand, Boolean>
//    {
//        private readonly IResourceAllocationRepository _resourceAllocationRepository;
//        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
//        public RejectResourceAllocationCommandHandler(IResourceAllocationRepository resourceAllocationRepository, IProjectServiceHttpApi projectServiceHttpApi)
//        {
//            _resourceAllocationRepository = resourceAllocationRepository;
//            _projectServiceHttpApi = projectServiceHttpApi;
//        }
//        public async Task<Boolean> Handle(RejectResourceAllocationCommand request, CancellationToken cancellationToken)
//        {
//            var newResourceAllocation = await _resourceAllocationRepository.RejectResourceAllocation(request.Requisition_Id);            
//            return newResourceAllocation;
//        }
//    }       
//}
