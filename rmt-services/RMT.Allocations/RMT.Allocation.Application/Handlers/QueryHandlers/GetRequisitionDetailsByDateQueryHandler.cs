using MediatR;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetRequisitionDetailsByDateQuery : IRequest<List<RequisitionResponse>>
    {
        public DateTime CreatedAt {  get; set; }
        public DateTime ModifiedAt { get; set; }
    }
    public class GetRequisitionDetailsByDateQueryHandler : IRequestHandler<GetRequisitionDetailsByDateQuery, List<RequisitionResponse>>
    {

        private readonly IRequisitionRepository _requisitionRepository;
 

        public GetRequisitionDetailsByDateQueryHandler(IRequisitionRepository requisitionRepository)
        {
            _requisitionRepository = requisitionRepository;
        }

        public async Task<List<RequisitionResponse>> Handle(GetRequisitionDetailsByDateQuery request, CancellationToken cancellationToken)
        {
            //Fetch Requisition Details
            var requisitionDetails = await _requisitionRepository.GetRequistionByDate(request.CreatedAt,request.ModifiedAt);
            //Helper.GetRequsitionListByCurrentUserRole(List);
            List<RequisitionResponse> requisitionResponse = AllocationMapper.Mapper.Map<List<RequisitionResponse>>(requisitionDetails);
            return requisitionResponse;
        }
    }
}
