//using MediatR;
//using RMT.Allocation.Application.DTOs.RequisitionDTOs;
//using RMT.Allocation.Domain.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//not in use
//namespace RMT.Allocation.Application.Handlers.CommandHandlers
//{
//    public class ChangeRequisitionEndDateCommand : IRequest<ChangeRequisitionEndDateDTO>
//    {

//    }
//    public class ChangeRequisitionEndDateCommandHandler : IRequestHandler<ChangeRequisitionEndDateCommand, ChangeRequisitionEndDateDTO>
//    {
//        private readonly IRequisitionRepository _requisitionRepository;
//        public ChangeRequisitionEndDateCommandHandler(IRequisitionRepository requisitionRepository)
//        {
//            _requisitionRepository = requisitionRepository;

//        }
//        public Task<ChangeRequisitionEndDateDTO> Handle(ChangeRequisitionEndDateCommand request, CancellationToken cancellationToken)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
