//using MediatR;
//using RMT.Allocation.Application.DTOs;
//using RMT.Allocation.Application.HttpServices.DTOs;
//using RMT.Allocation.Application.Mappers;
//using RMT.Allocation.Application.Responses;
//using RMT.Allocation.Domain.DTO;
//using RMT.Allocation.Domain.Entities;
//using RMT.Allocation.Domain.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RMT.Allocation.Application.Handlers.QueryHandlers
//{

//    public class GetRequisitionDetailsByEmployeeQuery : IRequest<List<RequisitionResponse>>
//    {
//        public string? pipelineCode { get; set; }
//        public string? jobcode { get; set; }
//        public string emailId { get; set; }
//        //public string projectCode { get; set; }
//    }

//    public class GetRequisitionDetailsByEmployeeQueryHandler : IRequestHandler<GetRequisitionDetailsByEmployeeQuery, List<RequisitionResponse>>
//    {
//        private readonly IRequisitionRepository _requisitionRepository;
//        public GetRequisitionDetailsByEmployeeQueryHandler(IRequisitionRepository requisitionRepository)
//        {
//            _requisitionRepository = requisitionRepository;
//        }

//        public async Task<List<RequisitionResponse>> Handle(GetRequisitionDetailsByEmployeeQuery request, CancellationToken cancellationToken)
//        {
//            RequisitionDTO query = new RequisitionDTO()
//            {
//                emailId = request.emailId,
//                //projectCode = request.projectCode,
//                pipelineCode = request.pipelineCode,
//                jobcode = request.jobcode
//            };
//            //Fetch Requisition Details
//            List<Requisition> requisitionDetails = await _requisitionRepository.GetAllRequisitionByPipeEmailId(query);
//            List<RequisitionResponse> requisitionResponse = AllocationMapper.Mapper.Map<List<RequisitionResponse>>(requisitionDetails);
//            return requisitionResponse;
//        }
//    }
//}
