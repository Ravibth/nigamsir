using Azure;
using MediatR;
using Newtonsoft.Json;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class CreateRequisitionCommand : IRequest<List<Requisition>>
    {
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? PipelineName { get; set; }
        public string? JobName { get; set; }
        public string? RequisitionDescription { get; set; }
        public Boolean? IsContinuousAllocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Int64? TotalHours { get; set; }
        public string? RequisitionStatus { get; set; }
        public string? Token { get; set; }
        public string Offerings { get; set; }
        public string Solutions { get; set; }
        public string? BusinessUnit { get; set; }
        public string? Designation { get; set; }
        public string? Grade { get; set; }
        public Boolean? IsAllResourcesSimilar { get; set; }
        public Int64? NumberOfResources { get; set; }
        public string? Description { get; set; }
        public string? SkillName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public bool? IsActive { get; set; } = true;
        public List<ResourceEntities>? ResourceEntities { get; set; }
        public string? ClientName { get; set; }
    }

    public class CreateRequisitionCommandHandler : IRequestHandler<CreateRequisitionCommand, List<Requisition>>
    {
        private readonly IRequisitionRepository _requisitionRepository;
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;

        public CreateRequisitionCommandHandler(IRequisitionRepository requisitionRepository, IResourceAllocationRepository resourceAllocationRepository, IProjectServiceHttpApi projectServiceHttpApi)
        {
            _requisitionRepository = requisitionRepository;
            _resourceAllocationRepository = resourceAllocationRepository;
            _projectServiceHttpApi = projectServiceHttpApi;
        }

        public async Task<List<Requisition>> Handle(CreateRequisitionCommand request, CancellationToken cancellationToken)
        {
            var Requisition = AllocationMapper.Mapper.Map<RequisitionRequest>(request);
            if (Requisition is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            
            var newRequisition = await _requisitionRepository.AddRequisitionAsync(Requisition);

            int allocationcount = await _resourceAllocationRepository.GetAllocationCount(request.PipelineCode, request.JobCode);
            int requicount = await _resourceAllocationRepository.GetRequisitionCount(request.PipelineCode, request.JobCode);
            await _projectServiceHttpApi.AddUpdateProjectRequisitionAllocation(request.PipelineCode, request.JobCode, requicount, allocationcount);

            return newRequisition;
        }
    }


}
