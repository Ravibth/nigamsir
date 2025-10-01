using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.DTOs;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetProjectsByEmployeeEmailAndPipelineCodeQuery : IRequest<List<EmployeeProject>>
    {
        public string Email { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
    }
    public class GetProjectsByEmployeeEmailAndPipelineCodeQueryHandler : IRequestHandler<GetProjectsByEmployeeEmailAndPipelineCodeQuery, List<EmployeeProject>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public GetProjectsByEmployeeEmailAndPipelineCodeQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }
        public async Task<List<EmployeeProject>> Handle(GetProjectsByEmployeeEmailAndPipelineCodeQuery request, CancellationToken cancellationToken)
        {
            List<ResourceAllocationResponse> result = await _resourceAllocationRepository.GetProjectsByEmployeeEmailAndPipelineCode(request.Email, request.PipelineCode, request.JobCode, null);
            List<EmployeeProject> entitiy = AllocationMapper.Mapper.Map<List<EmployeeProject>>(result);
            if (entitiy is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            return await Task.FromResult(entitiy);
        }
    }
}
