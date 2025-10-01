using MediatR;
using RMT.Allocation.Application.DTOs.RequisitionDTOs;
using RMT.Allocation.Application.DTOs.ResourceAllocationDTOs;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain;
using RMT.Allocation.Domain.DTO;
//using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO.Request;
//using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure;
using RMT.Allocation.Infrastructure.Migrations;
using RMT.Allocation.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class BulkRequisitionCommand : IRequest<BulkRequistionResponse>
    {
        public List<BulkCreateRequisitionDTO> bulkRequisitions { get; set; }
        public UserDecorator userInfo { get; set; }

    }

    public class BulkRequisitionCommandHandler : IRequestHandler<BulkRequisitionCommand, BulkRequistionResponse>
    {
        private readonly IRequisitionRepository _requisitionRepository;
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;

        //private readonly IWCGTMasterHttpApi _designationWCGTHttpApi;

        public BulkRequisitionCommandHandler(IResourceAllocationRepository resourceAllocationRepository, IRequisitionRepository requisitionRepository, IProjectServiceHttpApi projectServiceHttpApi)
        {
            _requisitionRepository = requisitionRepository;
            _resourceAllocationRepository = resourceAllocationRepository;
            _projectServiceHttpApi = projectServiceHttpApi;

        }

        public async Task<BulkRequistionResponse> Handle(BulkRequisitionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var allRequisitionAllocations = AllocationMapper.Mapper.Map<List<BulkRequisition>>(request.bulkRequisitions);
                if (allRequisitionAllocations is null)
                {
                    throw new ApplicationException("Issue with mapper");
                }
                var requisition = allRequisitionAllocations.Where((a) => string.IsNullOrEmpty(a.EmailId)).ToList();
                var newRequisition = new BulkRequistionResponse();
                if (requisition.Count > 0)
                {
                    newRequisition = await _requisitionRepository.AddBulkRequisitions(requisition, request.userInfo);
                }
                var allocations = allRequisitionAllocations.Where((a) => !string.IsNullOrEmpty(a.EmailId)).ToList();
                //if (allocations.Count > 0)
                //{
                //    /*************** Named Allocation*************************/
                //    var namedResourceAllocation = new NamedResourceCommandHandler(_resourceAllocationRepository, _requisitionRepository, _projectServiceHttpApi);
                //    foreach (var allocation in allocations)
                //    {
                //        var namedResourceAllocationDTO = new NamedResourceAllocationDTO()
                //        {
                //            PipelineCode = allocation.PipelineCode,
                //            JobCode = allocation.JobCode,
                //            ProjectCode = allocation.ProjectCode,
                //            ProjectName = allocation.ProjectName,
                //            RequisitionDescription = allocation.RequisitionDescription,
                //            IsContinuousAllocation = false,
                //            StartDate = (DateTime)allocation.StartDate,
                //            EndDate = (DateTime)allocation.EndDate,
                //            TotalHours = (int)allocation.TotalHours,
                //            RequisitionStatus = allocation.RequisitionStatus,
                //            Expertise = allocation.Expertise,
                //            SME = allocation.SME,
                //            Description = allocation.Description,
                //            userDetails = new UserDetailsDTO[] { new UserDetailsDTO { EmpEmail = allocation.EmailId, EmpName = allocation.EmpName, Designation = allocation.Designation } }
                //        };
                //        var command = AllocationMapper.Mapper.Map<NamedResourceAllocationCommand>(namedResourceAllocationDTO);
                //        var response = await namedResourceAllocation.Handle(command, cancellationToken);
                //    }
                //}
                
                foreach (var req in allRequisitionAllocations) 
                {
                    int allocationcount = await _resourceAllocationRepository.GetAllocationCount(req.PipelineCode, req.JobCode);
                    int requicount = await _resourceAllocationRepository.GetRequisitionCount(req.PipelineCode, req.JobCode);
                    await _projectServiceHttpApi.AddUpdateProjectRequisitionAllocation(req.PipelineCode, req.JobCode, requicount, allocationcount);
                }
                
                return newRequisition;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
