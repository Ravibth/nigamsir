//using MediatR;
//using RMT.Allocation.Application.Mappers;
//using RMT.Allocation.Application.Responses;
//using RMT.Allocation.Domain.Entities;
//using RMT.Allocation.Domain.Repositories;
//using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;
//using RMT.Allocation.Application.IHttpServices;
//using RMT.Allocation.Application.HttpServices;
//using RMT.Allocation.Application.HttpServices.DTOs;
//using RMT.Allocation.Infrastructure.Migrations;
//using RMT.Allocation.Infrastructure;
//using RMT.Allocation.Domain;
//using RMT.Allocation.Domain.DTO;

//namespace RMT.Allocation.Application.Handlers.CommandHandlers
//{
//    //public class CreateResourceAllocationDTO
//    //{
//    //    //public Int64 Id { get; set; }
//    //    public string PipelineCode { get; set; }
//    //    public string JobCode { get; set; }
//    //    public string ProjectCode { get; set; }
//    //    public string JobName { get; set; }
//    //    public string EmpEmail { get; set; }
//    //    public string EmpName { get; set; }
//    //    //public DateTime ConfirmedAllocationStartDate { get; set; }
//    //    //public DateTime ConfirmedAllocationEndDate { get; set; }
//    //    //public Int64 ConfirmedPerDayHours { get; set; }
//    //    public Int64 RequisitionId { get; set; }
//    //    public string AllocationStatus { get; set; }
//    //    public string RecordType { get; set; }
//    //    public DateTime CreatedDate { get; set; }
//    //    public DateTime ModifiedDate { get; set; }
//    //    public string CreatedBy { get; set; }
//    //    public string ModifiedBy { get; set; }
//    //    public bool IsActive { get; set; } = true;
//    //    public AllocationObj[] allocation { get; set; }
//    //}
//    public class CreateResourceAllocationCommand : IRequest<ResourceAllocationResponse[]>
//    {
//        public CreateResourceAllocationDTO[] allocationDTOs { get; set; }
//    }
//    public class CreateResourceAllocationCommandHandler : IRequestHandler<CreateResourceAllocationCommand, ResourceAllocationResponse[]>
//    {
//        private readonly IResourceAllocationRepository _resourceAllocationRepository;
//        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
//        public CreateResourceAllocationCommandHandler(IResourceAllocationRepository resourceAllocationRepository, IProjectServiceHttpApi projectServiceHttpApi)
//        {
//            _resourceAllocationRepository = resourceAllocationRepository;
//            _projectServiceHttpApi = projectServiceHttpApi;
//        }

//        public async Task<ResourceAllocationResponse[]> Handle(CreateResourceAllocationCommand request, CancellationToken cancellationToken)
//        {
//            List<ResourceAllocationDetails> inputData = new List<ResourceAllocationDetails>();
//            List<AddProjectUserRole> projectRoles = new List<AddProjectUserRole>();

//            //Todo Get rate from wcgt
//            Dictionary<string, double> resourceRateDict = new Dictionary<string, double>();



//            var pipelineCode = "0";
//            var jobCode = "0";
//            foreach (var item in request.allocationDTOs)
//            {
//                pipelineCode = item.PipelineCode;
//                jobCode = item.JobCode;
//                var data = new ResourceAllocationDetails()
//                {
//                    //ProjectCode = item.ProjectCode,
//                    PipelineCode = item.PipelineCode,
//                    JobCode = item.JobCode,
//                    JobName = item.JobName,
//                    PipelineName = item.PipelineName,
//                    EmpEmail = item.EmpEmail,
//                    EmpName = item.EmpName,
//                    IsContinuousAllocation = item.allocation.Length == 1 ? true : false,
//                    TotalEffort = item.TotalEffort,
//                    RequisitionId = item.RequisitionId,
//                    AllocationStatus = "PENDING",
//                    RecordType = item.RecordType,
//                    CreatedDate = DateTime.UtcNow,
//                    ModifiedDate = DateTime.UtcNow,
//                    CreatedBy = "", // Todo:
//                    ModifiedBy = "",
//                    Description = "",
//                    IsActive = true,
//                };
//                inputData.Add(data);
//                projectRoles.Add(new AddProjectUserRole
//                {
//                    User = item.EmpEmail,
//                    UserName = item.EmpName,
//                    Role = Constants.UserRoles.Employee
//                });
//            }
//            var allocationObj = request.allocationDTOs;
//            var newResourceAllocation = await _resourceAllocationRepository.AddAsync(inputData, request.allocationDTOs[0].allocation, resourceRateDict, new List<AddResourceAllocationSkillRequestDTO>());
//            ResourceAllocationResponse[] resourceAllocationResponse = AllocationMapper.Mapper.Map<ResourceAllocationResponse[]>(newResourceAllocation);
//            await RMT.Allocation.Application.Utils.Helper.AddProjectRoles(projectRoles, 0, pipelineCode, jobCode, _projectServiceHttpApi);
//            return resourceAllocationResponse;
//        }
//    }
//}
