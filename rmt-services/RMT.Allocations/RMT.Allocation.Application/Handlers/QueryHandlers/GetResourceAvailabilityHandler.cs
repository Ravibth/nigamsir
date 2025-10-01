//using MediatR;
//using RMT.Allocation.Domain.DTO;
//using RMT.Allocation.Domain.Repositories;
//using RMT.Allocation.Infrastructure;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RMT.Allocation.Application.Handlers.QueryHandlers
//{
//    public class GetResourceAvailabilityHandler
//    {
//        public class ResourceAvailabilityRequestDTO : IRequest<List<ResourceAvailabilityDTO>>
//        {
//            public List<string> email { get; set; }
//            public int leaves { get; set; }
//            public DateTime start_date { get; set; }
//            public DateTime end_date { get; set; }
//            public int total_required_hours { get; set; }

//            public int perday_max_effort { get; set; }
//        }

//        public class ResourceAvailabilityQueryHandler : IRequestHandler<ResourceAvailabilityRequestDTO, List<ResourceAvailabilityDTO>>
//        {
//            private readonly IResourceAllocationRepository _resourceAllocationRepository;
//            public ResourceAvailabilityQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
//            {
//                _resourceAllocationRepository = resourceAllocationRepository;
//            }

//            public async Task<List<ResourceAvailabilityDTO>> Handle(ResourceAvailabilityRequestDTO request, CancellationToken cancellationToken)
//            {
//                ResourceAvailabilityQueryDTO query = new ResourceAvailabilityQueryDTO()
//                {
//                    email = request.email,
//                    end_date = request.end_date,
//                    start_date = request.start_date,
//                    leaves = request.leaves,
//                    total_required_hours = request.total_required_hours,
//                    perday_max_effort = Constants.WorkingHourPerDay// 8
//                };

//                List<ResourceAvailabilityDTO> response = await _resourceAllocationRepository.getResourceAvailability(query);
//                return response;
//            }
//        }
//    }
//}
