using MediatR;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetDraftTimesheetQuery : IRequest<List<DraftTimesheetResponse>>
    {
        public List<DateTime> dates { get; set; }
    }

    public class GetDraftTimesheetQueryHandler : IRequestHandler<GetDraftTimesheetQuery, List<DraftTimesheetResponse>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;

        public GetDraftTimesheetQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }

        public async Task<List<DraftTimesheetResponse>> Handle(GetDraftTimesheetQuery request, CancellationToken cancellationToken)
        {
            return await _resourceAllocationRepository.GetDraftTimesheet(request.dates);
        }
    }
}
