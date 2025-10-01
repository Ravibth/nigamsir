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
    //public class GetApprovedAllocationByConfirmedDateQuery : IRequest<object>
    //{
    //    public List<DateTime> AllocationConfirmedDates { get; set; }
    //}

    //public class GetApprovedAllocationByConfirmedDateQueryHandler : IRequestHandler<GetApprovedAllocationByConfirmedDateQuery, object>
    //{
    //    private readonly IResourceAllocationRepository _resourceAllocationRepository;
    //    private readonly IMediator _mediator;

    //    public GetApprovedAllocationByConfirmedDateQueryHandler(IResourceAllocationRepository resourceAllocationRepository, IMediator mediator)
    //    {
    //        _resourceAllocationRepository = resourceAllocationRepository;
    //        _mediator = mediator;
    //    }

    //    public async Task<object> Handle(GetApprovedAllocationByConfirmedDateQuery request, CancellationToken cancellationToken)
    //    {


    //        return null;
    //    }
    //}
}
