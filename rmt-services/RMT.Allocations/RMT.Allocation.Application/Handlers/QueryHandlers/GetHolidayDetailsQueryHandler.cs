//using MediatR;
//using RMT.Allocation.Domain.DTO;
//using RMT.Allocation.Domain.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RMT.Allocation.Application.Handlers.QueryHandlers
//{
//    public class GetHolidayDetailsQueryHandlerQuery : IRequest<Dictionary<string, List<HolidayDetailsDTO>>>
//    {
//        public List<string> EmailId { get; set; }
//    }
//    public class GetHolidayDetailsQueryHandler : IRequestHandler<GetHolidayDetailsQueryHandlerQuery, Dictionary<string, List<HolidayDetailsDTO>>>
//    {
//        private readonly IResourceAllocationRepository _resourceAllocationRepository;

//        public GetHolidayDetailsQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
//        {
//            _resourceAllocationRepository = resourceAllocationRepository;
//        }

//        public async Task<Dictionary<string, List<HolidayDetailsDTO>>> Handle(GetHolidayDetailsQueryHandlerQuery request, CancellationToken cancellationToken)
//        {
//            Dictionary<string, List<HolidayDetailsDTO>> result = await _resourceAllocationRepository.GetEmpHolidayDetails(request.EmailId);
//            return await Task.FromResult(result);
//        }
//    }
//}
