using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.DTOs;
using RMT.Allocation.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetProjectsByEmployeeEmailQuery : IRequest<List<EmployeeProject>>
    {
        public string Email { get; set; }

    }

    //To be moved to Allocation Service

    public class GetProjectsByEmployeeEmailQueryHandler : IRequestHandler<GetProjectsByEmployeeEmailQuery, List<EmployeeProject>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public GetProjectsByEmployeeEmailQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }

        public async Task<List<EmployeeProject>> Handle(GetProjectsByEmployeeEmailQuery request, CancellationToken cancellationToken)
        {
            List<ResourceAllocationResponse> result = await _resourceAllocationRepository.GetResourceAllocationByEmail(request.Email);

            List<EmployeeProject> entitiy = AllocationMapper.Mapper.Map<List<EmployeeProject>>(result);
            if (entitiy is null)
            {
                throw new ApplicationException("Issue with mapper");
            }

            return await Task.FromResult(entitiy);
        }
    }
}
