//using MediatR;
//using RMT.Allocation.Application.HttpServices;
//using RMT.Allocation.Application.HttpServices.DTOs;
//using RMT.Allocation.Application.IHttpServices;
//using RMT.Allocation.Application.Mappers;
//using RMT.Allocation.Application.Responses;
//using RMT.Allocation.Domain;
//using RMT.Allocation.Domain.Entities;
//using RMT.Allocation.Domain.Repositories;
//using RMT.Allocation.Infrastructure;
//using RMT.Allocation.Infrastructure.DTOs;

//namespace RMT.Allocation.Application.Handlers.CommandHandlers
//{
//    public class UpdateResourceAllocationCommand : IRequest<ResourceAllocationDetailsResponse>
//    {
//        public string? token { get; set; }
//        public Int64? Id { get; set; }
//        public string? PipelineCode { get; set; }
//        public string? JobCode { get; set; }
//        //public string? ProjectCode { get; set; }
//        public string? JobName { get; set; }
//        public string? EmpEmail { get; set; }
//        public string? EmpName { get; set; }

//        public List<ResourceAllocation>? ResourceAllocation { get; set; }

//        public Int64? RequisitionId { get; set; }
//        public string? AllocationStatus { get; set; }
//        public string? RecordType { get; set; }
//        public DateTime? CreatedDate { get; set; }
//        public DateTime? ModifiedDate { get; set; }
//        public string? CreatedBy { get; set; }
//        public string? ModifiedBy { get; set; }
//        public bool? IsActive { get; set; } = true;
//        public string? PipelineName { get; set; }
//        public bool? IsContinuousAllocation { get; set; }
//        public string? Description { get; set; }
//        public int TotalEffort { get; set; }


//    }
//    public class UpdateResourceAllocationCommandHandler : IRequestHandler<UpdateResourceAllocationCommand, ResourceAllocationDetailsResponse>
//    {
//        private readonly IResourceAllocationRepository _resourceAllocationRepository;
//        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
//        public UpdateResourceAllocationCommandHandler(IResourceAllocationRepository resourceAllocationRepository, IProjectServiceHttpApi projectServiceHttpApi)
//        {
//            _resourceAllocationRepository = resourceAllocationRepository;
//            _projectServiceHttpApi = projectServiceHttpApi;
//        }

//        public async Task<ResourceAllocationDetailsResponse> Handle(UpdateResourceAllocationCommand request, CancellationToken cancellationToken)
//        {
//            var ResourceAllocated = AllocationMapper.Mapper.Map<ResourceAllocationDetails>(request);
//            if (ResourceAllocated is null)
//            {
//                throw new ApplicationException("Issue with mapper");
//            }
//            var newResourceAllocation = await _resourceAllocationRepository.UpdateAsync(ResourceAllocated);
//            ResourceAllocationDetailsResponse resourceAllocationResponse = AllocationMapper.Mapper.Map<ResourceAllocationDetailsResponse>(newResourceAllocation);
//            List<AddProjectUserRole> projectRoles = new List<AddProjectUserRole>();
//            projectRoles.Add(new AddProjectUserRole
//            {
//                User = resourceAllocationResponse.EmpEmail,
//                UserName = resourceAllocationResponse.EmpName,
//                Role = Constants.UserRoles.Employee
//            });
//            await RMT.Allocation.Application.Utils.Helper.AddProjectRoles(projectRoles, 0, resourceAllocationResponse.PipelineCode, resourceAllocationResponse.JobCode, _projectServiceHttpApi);
//            return resourceAllocationResponse;
//        }

//    }

//}
