using MediatR;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetPublishAllocationDetailsByDateQuery : IRequest<List<PublishAllocationResponse>>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
    public class GetPublishAllocationDetailsByDateQueryHandler : IRequestHandler<GetPublishAllocationDetailsByDateQuery, List<PublishAllocationResponse>>
    {
        private readonly IRequisitionRepository _requisitionRepository;


        public GetPublishAllocationDetailsByDateQueryHandler(IRequisitionRepository requisitionRepository)
        {
            _requisitionRepository = requisitionRepository;
        }
        public async Task<List<PublishAllocationResponse>> Handle(GetPublishAllocationDetailsByDateQuery request, CancellationToken cancellationToken)
        {
            //Fetch Requisition Details
            var allocationDetails = await _requisitionRepository.GetPublishedAllocationByDate(request.CreatedAt, request.ModifiedAt);
            //Helper.GetRequsitionListByCurrentUserRole(List);
            List<PublishAllocationResponse> response = AllocationMapper.Mapper.Map<List<PublishAllocationResponse>>(allocationDetails);
            return response;
        }
    }
}
