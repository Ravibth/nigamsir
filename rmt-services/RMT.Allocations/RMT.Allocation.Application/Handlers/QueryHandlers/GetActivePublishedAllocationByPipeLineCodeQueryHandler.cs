using MediatR;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetActivePublishedAllocationByPipeLineCodeQuery : IRequest<List<ResourceAllocationDetailsResponse>>
    {
        public List<KeyValuePair<string,string?>> res { get; set; }
    }
    public class GetActivePublishedAllocationByPipeLineCodeQueryHandler : IRequestHandler<GetActivePublishedAllocationByPipeLineCodeQuery, List<ResourceAllocationDetailsResponse>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public GetActivePublishedAllocationByPipeLineCodeQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }

        public async Task<List<ResourceAllocationDetailsResponse>> Handle(GetActivePublishedAllocationByPipeLineCodeQuery request, CancellationToken cancellationToken)
        {
            var response = await _resourceAllocationRepository.GetActivePublishedAllocationByPipeLineCode(request.res);
            return response;
        }
    }
}
