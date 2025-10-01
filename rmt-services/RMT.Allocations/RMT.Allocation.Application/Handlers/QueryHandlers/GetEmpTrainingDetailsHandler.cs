//using MediatR;
//using RMT.Allocation.Domain.DTO;
//using RMT.Allocation.Domain.DTO.Request;
//using RMT.Allocation.Domain.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RMT.Allocation.Application.Handlers.QueryHandlers
//{
//    public class GetEmpTrainingDetailsHandlerQuery : IRequest<Dictionary<string, List<TrainingDetailsDTO>>>
//    {
//        public List<string> EmailId { get; set; }
//    }

//    public class GetEmpTrainingDetailsHandler : IRequestHandler<GetEmpTrainingDetailsHandlerQuery, Dictionary<string, List<TrainingDetailsDTO>>>
//    {
//        private readonly IResourceAllocationRepository _resourceAllocationRepository;

//        public GetEmpTrainingDetailsHandler(IResourceAllocationRepository resourceAllocationRepository)
//        {
//            _resourceAllocationRepository = resourceAllocationRepository;
//        }

//        public async Task<Dictionary<string, List<TrainingDetailsDTO>>> Handle(GetEmpTrainingDetailsHandlerQuery request, CancellationToken cancellationToken)
//        {
//            Dictionary<string, List<TrainingDetailsDTO>> result = await _resourceAllocationRepository.GetEmpTrainingDetails(request.EmailId);
//            return await Task.FromResult(result);
//        }

//    }
//}
